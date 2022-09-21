module Tests

open System
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.gameControl.getGame().Length > 4)

[<Fact>]
let ``Indented strings are not long as long as one moves them in`` () =
    let smallBoard = game.gameControl.getGameSmall()
    let length = smallBoard.Length
    Assert.True(smallBoard.Length <= 4 + 2*2)
