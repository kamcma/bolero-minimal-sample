namespace BoleroMinimalSample

open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Components.Builder

type Startup() =
    member __.ConfigureServices(services: IServiceCollection) = ()

    member __.Configure(app: IComponentsApplicationBuilder) = app.AddComponent<BoleroMinimalSample>("#app")

open Microsoft.AspNetCore.Blazor.Hosting

module Program =
    [<EntryPoint>]
    let main argv =
        BlazorWebAssemblyHost.CreateDefaultBuilder().UseBlazorStartup<Startup>().Build().Run()
        0
