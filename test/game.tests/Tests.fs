module Tests

open System
open sokoban 
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.init().Length > 4)

[<Fact>]
let parse_wall_returns_wall () = 
    let board = "#"
    Assert.True([['#']] =  game.parseBoard(board))

[<Fact>]
let parse_character_returns_character () = 
    Assert.True([['@']] =  game.parseBoard("@"))

[<Fact>]
let parse_empty_string_returns_empty_board () = 
    let board = game.parseBoard("")
    Assert.True(board.IsEmpty)

[<Fact>]
let parseBoards () =  
    let board = game.parseBoard(game.init())
    Assert.True('#' = board.Head.Head) 
    Assert.True(9 = board.Length)
    Assert.True(4 = board.Head.Length)

[<Fact>]
let getPlayerPosition_OnlyPlayer_00 () =
    let board = game.parseBoard("@")
    Assert.True((0,0) = game.getPlayerPosition board)
