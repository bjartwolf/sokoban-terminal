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
    let board = game.gameControl.parseBoard("")
    Assert.True(board.IsEmpty)

[<Fact>]
let parseBoards () =  
    let board = game.gameControl.parseBoard(game.gameControl.init())
    Assert.True('#' = board.Head.Head) 
    Assert.True(9 = board.Length)
    Assert.True(4 = board.Head.Length)


