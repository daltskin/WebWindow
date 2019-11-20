using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WebWindows;

namespace DockerSuperMario
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string dockerImage = "pengbai/docker-supermario";
            string mappedPort = "8600";
            string localPort = "8080";

            // Use local docker daemon
            var client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();

            var progress = new Progress<JSONMessage>(p => Debug.WriteLine(p.Status));

            // This will take a while the first time as it's ~640mb big
            await client.Images.CreateImageAsync(new ImagesCreateParameters() { FromImage = dockerImage }, null, progress);

            var container = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
            {
                Image = dockerImage,
                ExposedPorts = new Dictionary<string, EmptyStruct>() { { localPort, new EmptyStruct() } },
                HostConfig = new HostConfig()
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>> { { localPort, new List<PortBinding> { new PortBinding { HostIP = "", HostPort = mappedPort } } } }
                }
            });

            await client.Containers.StartContainerAsync(container.ID, null);

            await Task.Run(() =>
            {
                var window = new WebWindow("WebWindow with Super Mario in HTML5 running in Docker :)");
                window.NavigateToUrl($"http://localhost:{mappedPort}");
                window.WaitForExit();
            });
            await client.Containers.StopContainerAsync(container.ID, new ContainerStopParameters() { WaitBeforeKillSeconds = 1 });
        }
    }
}

