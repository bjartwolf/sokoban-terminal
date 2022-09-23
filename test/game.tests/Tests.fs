module Tests

open System
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.gameControl.init().Length > 4)

[<Fact>]
let parse_wall_returns_wall () = 
    let board = "#"
    Assert.True([['#']] =  game.gameControl.parseBoard(board))

[<Fact>]
let parse_character_returns_character() = 
    Assert.True([['@']] =  game.gameControl.parseBoard("@"))

[<Fact>]
let parse_empty_string_returns_empty_board() = 
    Assert.True([[]] =  game.gameControl.parseBoard(""))






