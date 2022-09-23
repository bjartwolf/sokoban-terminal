module Tests

open System
open sokoban 
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.init().Length > 4)

[<Fact>]
let ``Parse and serialize returns same board except the annoying first newline`` () =
    let initialBoard = game.init()[1..]
    let board = game.parseBoard(initialBoard)
    let serializedBoard = game.serializeBoard board
    Assert.True(String.Equals(initialBoard, serializedBoard)) 

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

[<Fact>]
let getPlayerPosition_initial_board () =
    let board = game.parseBoard(game.init())
    Assert.True((4,6) = game.getPlayerPosition board)

[<Fact>]
let moveLeft_PlayerCanMove_PlayerMovesLeft () =
    let boardBefore = @"
   
 @"
    let boardAfter = @"
   
@ "
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let newBoard = game.movePlayer board 'h'
    let positionAreEqual =  (expectedNewPostition = newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveUp_PlayerCanMove_PlayerMovesUp () =
    let boardBefore = @"
   
 @"
    let boardAfter = @"
 @ 
  "
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let newBoard = game.movePlayer board 'k'
    let positionAreEqual =  (expectedNewPostition = newBoard)
    Assert.True(positionAreEqual) 

