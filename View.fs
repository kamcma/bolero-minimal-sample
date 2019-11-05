module View

open Model
open Bolero.Html

let view model dispatch =
    div []
        [ button [ on.click (fun _ -> dispatch Decrement) ] [ text "-" ]
          text (string model.value)
          button [ on.click (fun _ -> dispatch Increment) ] [ text "+" ] ]
