module App

open Browser.Dom
open sokoban
open Browser.Types
open System

let gameScreen = document.querySelector(".game-screen") :?> Browser.Types.HTMLDivElement

let boardNr = 3 
let mutable moves: string = ""

let go (dir: Char) =
    let (board, history)= game.attemptMove(boardNr, moves, dir)
    gameScreen.textContent <- board
    moves <- history

gameScreen.textContent <- game.init(boardNr) 
document.onkeydown <- fun (keyEvent: KeyboardEvent) -> go (keyEvent.key |> Seq.head)
                                 
