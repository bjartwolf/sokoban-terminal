module Tests

open System
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.gameControl.getGame().Length > 4)
