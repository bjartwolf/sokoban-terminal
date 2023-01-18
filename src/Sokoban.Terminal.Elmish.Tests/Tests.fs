module Tests

open sokoban.terminal.elmish
open Xunit

[<Fact>]
let ``Initialized board nr 3`` () =
    let (model, _) = init ()
    Assert.Equal(3, model.BoardNr)
