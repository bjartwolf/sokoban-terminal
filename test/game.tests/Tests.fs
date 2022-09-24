module Tests

open System
open sokoban 
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.init().Length > 4)

[<Fact>]
let ``Parse and serialize returns same board except the annoying first newline`` () =
    let initialBoard = game.init()
    let board = game.parseBoard(initialBoard)
    let serializedBoard = game.serializeBoard board
    Assert.True(String.Equals(initialBoard, serializedBoard)) 

[<Fact>]
let parse_wall_returns_wall () = 
    let board = "#"
    let expectedBoard = Map<int*int,Char> ([(0,0),'#'])
    let boardsAreEqual = expectedBoard =  game.parseBoard(board)
    Assert.True(boardsAreEqual)

[<Fact>]
let parse_character_returns_character () = 
    let expectedBoard = Map<int*int,Char> ([(0,0),'@'])
    let boardsAreEqual = expectedBoard =  game.parseBoard("@")
    Assert.True(boardsAreEqual)

[<Fact>]
let parse_empty_string_returns_empty_board () = 
    let board = game.parseBoard("")
    Assert.True(board.IsEmpty)

//[<Fact>]
//let parseBoards () =  
//    let board = game.parseBoard(game.init())
//    Assert.True('#' = board.Head.Head) 
//    Assert.True(9 = board.Length)
//    Assert.True(4 = board.Head.Length)

//[<Fact>]
//let getPlayerPosition_OnlyPlayer_00 () =
//    let board = game.parseBoard("@")
//    Assert.True((0,0) = game.getPlayerPosition board)

//[<Fact>]
//let getPlayerPosition_initial_board () =
//    let board = game.parseBoard(game.init())
//    Assert.True((6,4) = game.getPlayerPosition board)

//[<Fact>]
//let moveLeft_PlayerCanMove_PlayerMovesLeft () =
//    let boardBefore = @"
   
// @"
//    let boardAfter = @"
   
//@ "
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'h'
//    let positionAreEqual =  (expectedNewPostition = newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveRight_OutOfBounds_NothingChanges() =
//    let boardBefore = @"
   
// @"
//    let board = game.parseBoard(boardBefore)

//    let newBoard = game.movePlayer board 'l'
//    let positionAreEqual =  (board = newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveRight_PlayerHitsWall_NothingChanges() =
//    let boardBefore = @"
   
//@#"
//    let board = game.parseBoard(boardBefore)

//    let newBoard = game.movePlayer board 'l'
//    let positionAreEqual =  (board = newBoard)
//    Assert.True(positionAreEqual) 


//[<Fact>]
//let moveDown_PlayerCantMove_NothingChanges() =
//    let boardBefore = @"
   
// @"
//    let board = game.parseBoard(boardBefore)

//    let newBoard = game.movePlayer board 'j'
//    let positionAreEqual =  (board = newBoard)
//    Assert.True(positionAreEqual) 


//[<Fact>]
//let moveUp_PlayerCanMove_PlayerMovesUp () =
//    let boardBefore = @"
   
// @"
//    let boardAfter = @"
// @ 
//  "
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'k'
//    let positionAreEqual =  (expectedNewPostition = newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveLeft_PlayerIsOnGoalSquare_PlayerMovesLeft () =
//    let boardBefore = @"
  
// +"
//    let boardAfter = @"
  
//@."
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'h'
//    let positionAreEqual = (expectedNewPostition = newBoard)
//    //printfn "%A" (game.serializeBoard expectedNewPostition)
//    //printfn "%A" (game.serializeBoard newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveLeft_PlayerMovesOntoGoalSquare_PlayerIsOnTopOfGoalSquare() =
//    let boardBefore = @"
  
//.@"
//    let boardAfter = @"
  
//+ "
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'h'
//    let positionAreEqual = (expectedNewPostition = newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveDown_PlayerTriesToPushBoxOnWall_NothingChanges() =
//    let boardBefore = @"
// @ 
// $ 
// #"
//    let board = game.parseBoard(boardBefore)

//    let newBoard = game.movePlayer board 'j'
//    let positionAreEqual =  (board = newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveLeft_PlayerPushBoxOnFloor_BoxAndPlayerMoves() =
//    let boardBefore = @"
//@$ "
//    let boardAfter= @"
// @$"
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'l'
//    let positionAreEqual = (expectedNewPostition = newBoard)
//    printfn "%A" (game.serializeBoard expectedNewPostition)
//    printfn "%A" (game.serializeBoard newBoard)
//    Assert.True(positionAreEqual) 


//[<Fact>]
//let moveDown_PlayerPushBoxOnFloor_BoxAndPlayerMoves() =
//    let boardBefore = @"
// @ 
// $ 
//  "
//    let boardAfter= @"
   
// @ 
// $"
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'j'
//    let positionAreEqual = (expectedNewPostition = newBoard)
//    printfn "%A" (game.serializeBoard expectedNewPostition)
//    printfn "%A" (game.serializeBoard newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveDown_PlayerPushBoxOnGoal_BoxAndPlayerMovesAndItIsBoxOnGoal() =
//    let boardBefore = @"
// @ 
// $ 
// ."
//    let boardAfter= @"
   
// @ 
// *"
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'j'
//    let positionAreEqual = (expectedNewPostition = newBoard)
//    printfn "%A" (game.serializeBoard expectedNewPostition)
//    printfn "%A" (game.serializeBoard newBoard)
//    Assert.True(positionAreEqual) 


//[<Fact>]
//let moveLeft_PlayerPushBoxOnGoalSquare_BoxAndPlayerMovesAndBoxIsOnGoalSquare() =
//    let boardBefore = @"
//@$."
//    let boardAfter= @"
// @*"
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'l'
//    let positionAreEqual = (expectedNewPostition = newBoard)
//    printfn "%A" (game.serializeBoard expectedNewPostition)
//    printfn "%A" (game.serializeBoard newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveLeft_PlayerPushBoxFromGoalSquareToGoalSquare_BoxAndPlayerMovesAndBoxIsOnGoalSquare() =
//    let boardBefore = @"
//@*."
//    let boardAfter= @"
// +*"
//    let board = game.parseBoard(boardBefore)
//    let expectedNewPostition = game.parseBoard(boardAfter) 
//    let newBoard = game.movePlayer board 'l'
//    let positionAreEqual = (expectedNewPostition = newBoard)
//    //printfn "%A" (game.serializeBoard expectedNewPostition)
//    //printfn "%A" (game.serializeBoard newBoard)
//    Assert.True(positionAreEqual) 

//[<Fact>]
//let moveDown_PlayerTriesToPushBoxGoalSquareOnWall_NothingChanges() =
//    let boardBefore = @"
// @ 
// * 
// #"
//    let board = game.parseBoard(boardBefore)

//    let newBoard = game.movePlayer board 'j'
//    printfn "%A" (game.serializeBoard newBoard)
//    let positionAreEqual =  (board = newBoard)
//    Assert.True(positionAreEqual) 


