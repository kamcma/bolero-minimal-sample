namespace BoleroMinimalSample

open Microsoft.AspNetCore.Components.WebAssembly.Hosting

module Program =
    [<EntryPoint>]
    let main argv =
        let builder = WebAssemblyHostBuilder.CreateDefault(argv)
        builder.RootComponents.Add<BoleroMinimalSample>("#app") |> ignore
        builder.Build().RunAsync() |> ignore
        0
