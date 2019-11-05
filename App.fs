namespace BoleroMinimalSample

open Elmish
open Bolero

open Model
open View
open Update

type BoleroMinimalSample() =
    inherit ProgramComponent<Model, Message>()
    override this.Program = Program.mkSimple (fun _ -> { value = 0 }) update view
