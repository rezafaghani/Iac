using Pulumi;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;

namespace Iac.Api.Application.Models
{
    public class ServiceDeploymentArgs
    {
        public Input<string> Image { get; set; } = null!;
        public Input<int>? Replicas { get; set; }
        public Input<ResourceRequirementsArgs>? Resources { get; set; }
        public InputList<int> Ports { get; set; } = new InputList<int>();
        public Input<bool>? AllocateIPAddress { get; set; }
        public Input<string>? ServiceType { get; set; }
        public InputList<EnvVarArgs> Env { get; set; } = new InputList<EnvVarArgs>();
    }
}