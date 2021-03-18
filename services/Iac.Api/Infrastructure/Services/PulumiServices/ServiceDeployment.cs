﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iac.Api.Application.Models;
using Pulumi;
using Pulumi.Kubernetes.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using Pulumi.Kubernetes.Types.Inputs.ApiExtensions.V1Beta1;

namespace Iac.Api.Infrastructure.Services.PulumiServices
{
    public class ServiceDeployment: Pulumi.ComponentResource
    {
         public Pulumi.Kubernetes.Apps.V1.Deployment Deployment;
    public Pulumi.Kubernetes.Core.V1.Service Service;
    public Output<string> IpAddress;

    public ServiceDeployment(string name, ServiceDeploymentArgs args, ComponentResourceOptions? opts = null)
        : base("k8sx:service:ServiceDeployment", name, opts)
    {
        var labels = new InputMap<string>
        {
            { "app", name },
        };

        var deploymentPorts = args.Ports.ToOutput().Apply(ports =>
            from p in ports select new ContainerPortArgs { ContainerPortValue = 6379 }
        );

        var container = new ContainerArgs
        {
            Name = name,
            Image = args.Image,
            Resources = args.Resources ?? new ResourceRequirementsArgs
            {
                Requests =
                {
                    { "cpu", "100m" },
                    { "memory", "100Mi" },
                },
            },
            Env = args.Env,
            Ports = deploymentPorts,
        };

        this.Deployment = new Pulumi.Kubernetes.Apps.V1.Deployment(name, new DeploymentArgs
        {
            Spec = new DeploymentSpecArgs
            {
                Selector = new LabelSelectorArgs
                {
                    MatchLabels = labels,
                },
                Replicas = args.Replicas ?? 1,
                Template = new PodTemplateSpecArgs
                {
                    Metadata = new ObjectMetaArgs
                    {
                        Labels = labels,
                    },
                    Spec = new PodSpecArgs
                    {
                        Containers = { container },
                    },
                },
            },
        }, 
        new CustomResourceOptions { Parent = this });

        var servicePorts = args.Ports.ToOutput().Apply(ports =>
            from p in ports select new ServicePortArgs { Port = p, TargetPort = p }
        );

        this.Service = new Pulumi.Kubernetes.Core.V1.Service(name, new ServiceArgs
        {
            Metadata = new ObjectMetaArgs
            {
                Name = name,
                Labels = this.Deployment.Metadata.Apply(metadata => metadata.Labels),
            },
            Spec = new ServiceSpecArgs
            {
                Type = args.AllocateIPAddress.Apply(hasIp => hasIp ? (args.ServiceType ?? "LoadBalancer") : null),
                Ports = servicePorts,
                Selector = this.Deployment.Spec.Apply(spec => spec.Template.Metadata.Labels),
            },
        }, 
        new CustomResourceOptions { Parent = this });

        this.IpAddress = args.AllocateIPAddress.Apply(hasIp => {
	        if (hasIp)
            {
                return args.ServiceType.Apply(serviceType =>
                    serviceType == "ClusterIP"
                    ? this.Service.Spec.Apply(s => s.ClusterIP)
                    : this.Service.Status.Apply(status =>
                    {
                        var ingress = status.LoadBalancer.Ingress[0];
                        // Return the ip address if populated or else the hostname
                        return ingress.Ip ?? ingress.Hostname;
                    })
                );
            }

	        return null;
        });
    }
    }
}