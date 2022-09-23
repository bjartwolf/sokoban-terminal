module Tests

open System
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.gameControl.init().Length > 4)
