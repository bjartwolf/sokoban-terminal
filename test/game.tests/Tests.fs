module Tests

open System
open sokoban 
open Xunit

[<Fact>]
let ``Game is not blank`` () =
    Assert.True(game.init(3).Length > 4)

[<Fact>]
let ``Parse and serialize returns same board except the annoying first newline`` () =
    let initialBoard = game.init(3)
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

[<Fact>]
let parseBoards () =  
    let board = game.parseBoard(game.init(3))
    Assert.True('#' = board.[(0,0)]) 
    Assert.True(9 = (board |> Map.filter (fun (x,y) t -> x = 0) |> Map.toList |> List.length))
    Assert.True(4 = (board |> Map.filter (fun (x,y) t -> y = 0) |> Map.toList |> List.length))

[<Fact>]
let getPlayerPosition_OnlyPlayer_00 () =
    let board = game.parseBoard("@")
    Assert.True((0,0) = game.getPlayerPosition board)

[<Fact>]
let getPlayerPosition_initial_board () =
    let board = game.parseBoard(game.init(3))
    Assert.True((6,4) = game.getPlayerPosition board)

[<Fact>]
let moveLeft_PlayerCanMove_PlayerMovesLeft () =
    let boardBefore = @"
   
 @"
    let boardAfter = @"
   
@ "
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,move) = game.movePlayer board 'l'
    let positionAreEqual =  (expectedNewPostition = newBoard)
    Assert.True(positionAreEqual) 
    let movesAreEqual = move = Some 'l' 
    Assert.True(movesAreEqual) 

[<Fact>]
let moveRight_OutOfBounds_NothingChanges() =
    let boardBefore = @"
   
 @"
    let board = game.parseBoard(boardBefore)

    let (newBoard,move) = game.movePlayer board 'r'
    let positionAreEqual =  (board = newBoard)
    Assert.True(positionAreEqual) 
    let movesAreEqual = move = None
    Assert.True(movesAreEqual)

[<Fact>]
let moveRight_PlayerHitsWall_NothingChanges() =
    let boardBefore = @"
   
@#"
    let board = game.parseBoard(boardBefore)

    let (newBoard, _) = game.movePlayer board 'r'
    let positionAreEqual =  (board = newBoard)
    Assert.True(positionAreEqual) 


[<Fact>]
let moveDown_PlayerCantMove_NothingChanges() =
    let boardBefore = @"
   
 @"
    let board = game.parseBoard(boardBefore)

    let (newBoard,_)  = game.movePlayer board 'd'
    let positionAreEqual =  (board = newBoard)
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
    let (newBoard,_) = game.movePlayer board 'u'
    let positionAreEqual =  (expectedNewPostition = newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveLeft_PlayerIsOnGoalSquare_PlayerMovesLeft () =
    let boardBefore = @"
  
 +"
    let boardAfter = @"
  
@."
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,_) = game.movePlayer board 'l'
    let positionAreEqual = (expectedNewPostition = newBoard)
    //printfn "%A" (game.serializeBoard expectedNewPostition)
    //printfn "%A" (game.serializeBoard newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveLeft_PlayerMovesOntoGoalSquare_PlayerIsOnTopOfGoalSquare() =
    let boardBefore = @"
  
.@"
    let boardAfter = @"
  
+ "
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,_) = game.movePlayer board 'l'
    let positionAreEqual = (expectedNewPostition = newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveDown_PlayerTriesToPushBoxOnWall_NothingChanges() =
    let boardBefore = @"
 @ 
 $ 
 #"
    let board = game.parseBoard(boardBefore)

    let (newBoard,_) = game.movePlayer board 'd'
    let positionAreEqual =  (board = newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveRight_PlayerPushBoxOnFloor_BoxAndPlayerMoves() =
    let boardBefore = @"
@$ "
    let boardAfter= @"
 @$"
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,_) = game.movePlayer board 'r'
    let positionAreEqual = (expectedNewPostition = newBoard)
    printfn "%A" (game.serializeBoard expectedNewPostition)
    printfn "%A" (game.serializeBoard newBoard)
    Assert.True(positionAreEqual) 


[<Fact>]
let moveDown_PlayerPushBoxOnFloor_BoxAndPlayerMoves() =
    let boardBefore = @"
 @ 
 $ 
  "
    let boardAfter= @"
   
 @ 
 $"
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,_) = game.movePlayer board 'd'
    let positionAreEqual = (expectedNewPostition = newBoard)
    printfn "%A" (game.serializeBoard expectedNewPostition)
    printfn "%A" (game.serializeBoard newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveDown_PlayerPushBoxOnGoal_BoxAndPlayerMovesAndItIsBoxOnGoal() =
    let boardBefore = @"
 @ 
 $ 
 ."
    let boardAfter= @"
   
 @ 
 *"
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,_) = game.movePlayer board 'd'
    let positionAreEqual = (expectedNewPostition = newBoard)
    printfn "%A" (game.serializeBoard expectedNewPostition)
    printfn "%A" (game.serializeBoard newBoard)
    Assert.True(positionAreEqual) 


[<Fact>]
let moveRight_PlayerPushBoxOnGoalSquare_BoxAndPlayerMovesAndBoxIsOnGoalSquare() =
    let boardBefore = @"
@$."
    let boardAfter= @"
 @*"
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,_) = game.movePlayer board 'r'
    let positionAreEqual = (expectedNewPostition = newBoard)
    printfn "%A" (game.serializeBoard expectedNewPostition)
    printfn "%A" (game.serializeBoard newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveRight_PlayerPushBoxFromGoalSquareToGoalSquare_BoxAndPlayerMovesAndBoxIsOnGoalSquare() =
    let boardBefore = @"
@*."
    let boardAfter= @"
 +*"
    let board = game.parseBoard(boardBefore)
    let expectedNewPostition = game.parseBoard(boardAfter) 
    let (newBoard,_) = game.movePlayer board 'r'
    let positionAreEqual = (expectedNewPostition = newBoard)
    //printfn "%A" (game.serializeBoard expectedNewPostition)
    //printfn "%A" (game.serializeBoard newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let moveDown_PlayerTriesToPushBoxGoalSquareOnWall_NothingChanges() =
    let boardBefore = @"
 @ 
 * 
 #"
    let board = game.parseBoard(boardBefore)

    let (newBoard,_) = game.movePlayer board 'd'
    printfn "%A" (game.serializeBoard newBoard)
    let positionAreEqual =  (board = newBoard)
    Assert.True(positionAreEqual) 

[<Fact>]
let board0_playermovesdowntwice_nothingChanges() =
    let board = game.startNewBoard(0)
    let _,history1 = game.attemptMove(0,"",'d') 
    let _,history2 = game.attemptMove(0,history1,'d') 
    Assert.True(String.IsNullOrEmpty(history2)) 


[<Fact>]
let board3_playermovesdowntwice_nothingChanges() =
    let board = game.startNewBoard(3)
    let _,history1 = game.attemptMove(3,"",'d') 
    let _,history2 = game.attemptMove(3,history1,'d') 
    let isHistoryEqual = history2 = "dd"
    Assert.True(isHistoryEqual) 

[<Fact>]
let board0_udrr_winsgame() =
    let gameNr = 0
    let _ = game.startNewBoard(gameNr)
    let _,history1 = game.attemptMove(gameNr,"",'u') 
    let _,history2 = game.attemptMove(gameNr,history1,'d') 
    let _,history3 = game.attemptMove(gameNr,history2,'r') 
    let finalState,history4 = game.attemptMove(gameNr,history3,'r') 
    let isHistoryEqual = history4 = "udrr"
    Assert.True(isHistoryEqual) 
    Assert.True(finalState.Contains("WIN")) 


[<Fact>]
let board0_udr_downotwinsgame() =
    let gameNr = 0
    let _ = game.startNewBoard(gameNr)
    let _,history1 = game.attemptMove(gameNr,"",'u') 
    let _,history2 = game.attemptMove(gameNr,history1,'d') 
    let finalState ,history3 = game.attemptMove(gameNr,history2,'r') 
    let isHistoryEqual = history3 = "udr"
    Assert.True(isHistoryEqual) 
    Assert.True(not (finalState.Contains("WIN"))) 


[<Fact>]
let board0_noinitialize_udrr_winsgame() =
    let gameNr = 0
    let finalState,history4 = game.attemptMove(gameNr,"udr",'r') 
    let isHistoryEqual = history4 = "udrr"
    Assert.True(isHistoryEqual) 
    Assert.True(finalState.Contains("WIN")) 

[<Fact>]
let board1_noinitialize_urduullddrdl_wins() =
    let gameNr = 1
    let finalState,history = game.attemptMove(gameNr,"urduullddrd",'l') 
    let isHistoryEqual = history = "urduullddrdl"
    Assert.True(isHistoryEqual) 
    Assert.True(finalState.Contains("WIN")) 
