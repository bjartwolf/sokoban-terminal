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
document.onkeydown <- fun (keyEvent: KeyboardEvent) -> 
    match keyEvent.key with
        | "a" | "h" | "ArrowLeft" -> go 'l'
        | "w"| "k" | "ArrowUp" -> go 'u'
        | "s" | "j" |"ArrowDown" -> go 'd'
        | "d" | "l" |"ArrowRight" -> go 'r'
        | _ -> () 
                                 
