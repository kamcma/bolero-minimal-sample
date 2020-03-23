module Model =
    type State =
        { count: int }

    type Message =
        | Increment
        | Decrement

module View =
    open Model
    open Bolero.Html

    let view model dispatch =
        div []
            [ button [ on.click (fun _ -> dispatch Decrement) ] [ text "-" ]
              text (string model.count)
              button [ on.click (fun _ -> dispatch Increment) ] [ text "+" ] ]

module Update =
    open Model

    let update message model =
        match message with
        | Increment -> { model with count = model.count + 1 }
        | Decrement -> { model with count = model.count - 1 }


module Program =
    open Microsoft.AspNetCore.Components.WebAssembly.Hosting
    open Elmish
    open Bolero

    open Model
    open View
    open Update

    type BoleroMinimalSample() =
        inherit ProgramComponent<State, Message>()
        override this.Program = Program.mkSimple (fun _ -> { count = 0 }) update view

    [<EntryPoint>]
    let main argv =
        let builder = WebAssemblyHostBuilder.CreateDefault(argv)
        builder.RootComponents.Add<BoleroMinimalSample>("#app") |> ignore
        builder.Build().RunAsync() |> ignore
        0
