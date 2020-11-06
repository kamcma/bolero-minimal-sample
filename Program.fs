module Program

open Elmish

module Counter =
    open Bolero.Html

    type State = { Count: int }

    type Msg =
        | Increment
        | Decrement

    let init () = { Count = 0 }, Cmd.none

    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + 1 }, Cmd.none
        | Decrement -> { model with Count = model.Count - 1 }, Cmd.none

    let view model dispatch =
        div [] [
            button [ on.click (fun _ -> dispatch Decrement) ] [
                text "-"
            ]
            text (string model.Count)
            button [ on.click (fun _ -> dispatch Increment) ] [
                text "+"
            ]
        ]

module MultiCounter =
    open Bolero.Html

    type State =
        { Counter1: Counter.State
          Counter2: Counter.State }

    type Msg =
        | Counter1 of Counter.Msg
        | Counter2 of Counter.Msg

    let init () =
        let counter1, counter1Cmd = Counter.init ()
        let counter2, counter2Cmd = Counter.init ()
        { Counter1 = counter1
          Counter2 = counter2 },
        Cmd.batch [ Cmd.map Counter1 counter1Cmd
                    Cmd.map Counter2 counter2Cmd ]

    let update msg model =
        match msg with
        | Counter1 msg' ->
            let counter1, counter1Cmd = Counter.update msg' model.Counter1
            { model with Counter1 = counter1 }, Cmd.map Counter1 counter1Cmd
        | Counter2 msg' ->
            let counter2, counter2Cmd = Counter.update msg' model.Counter2
            { model with Counter2 = counter2 }, Cmd.map Counter2 counter2Cmd

    let view model (dispatch: Msg -> unit) =
        div [] [
            lazyComp2 Counter.view model.Counter1 (Counter1 >> dispatch)
            lazyComp2 Counter.view model.Counter2 (Counter2 >> dispatch)
        ]

open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Bolero

type BoleroMinimalSample() =
    inherit ProgramComponent<MultiCounter.State, MultiCounter.Msg>()
    override this.Program = Program.mkProgram (fun _ -> MultiCounter.init ()) MultiCounter.update MultiCounter.view

[<EntryPoint>]
let main argv =
    let builder =
        WebAssemblyHostBuilder.CreateDefault(argv)

    builder.RootComponents.Add<BoleroMinimalSample>("#app")
    |> ignore
    builder.Build().RunAsync() |> ignore
    0
