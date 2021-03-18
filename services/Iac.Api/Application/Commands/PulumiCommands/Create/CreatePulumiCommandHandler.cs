using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pulumi;
using Pulumi.Docker;


namespace Iac.Api.Application.Commands.PulumiCommands.Create
{
    public class CreatePulumiCommandHandler:IRequestHandler<CreatePulumiCommand,int>
    {
        public async  Task<int> Handle(CreatePulumiCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var x= await Deployment.RunAsync(() =>
                {


                    var network = new Network(request.StackInfo.Network.Name);
                    var image = new Pulumi.Docker.RemoteImage(request.OsInfo.Title, new Pulumi.Docker.RemoteImageArgs
                    {
                        Name = $"{request.OsInfo.ImageName}:{request.OsInfo.Version}"
                  
                    });

                    var container = new Pulumi.Docker.Container(request.OsInfo.Title, new Pulumi.Docker.ContainerArgs
                    {
                        Image = image.Latest,
                 
                 
                    });
                    foreach (var item in request.StackInfo.Items)
                    {
                        var imageItem = new Pulumi.Docker.RemoteImage(item.Title, new Pulumi.Docker.RemoteImageArgs
                        {
                            Name = $"{item.ImageName}:latest",
                      
                  
                        });
                  
                        var containerItem = new Pulumi.Docker.Container(request.OsInfo.Title, new Pulumi.Docker.ContainerArgs
                        {
                            Image = imageItem.Latest,
                            Envs = item.Variables.Select(x=> new InputList<string>
                            {
                                x.Name,x.Value
                            }).ToString() ?? string.Empty,
                 
                        });
                 
                    }
              
                });
                return x;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}