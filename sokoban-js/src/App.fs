module App

open Browser.Dom
open sokoban
open Browser.Types
open System

// Mutable variable to count the number of times we clicked the button
let mutable count = 0

// Get a reference to our button and cast the Element to an HTMLButtonElement
let downButton = document.querySelector(".button-down") :?> Browser.Types.HTMLButtonElement
let upButton = document.querySelector(".button-up") :?> Browser.Types.HTMLButtonElement
let leftButton = document.querySelector(".button-left") :?> Browser.Types.HTMLButtonElement
let rightButton = document.querySelector(".button-right") :?> Browser.Types.HTMLButtonElement
let gameScreen = document.querySelector(".game-screen") :?> Browser.Types.HTMLDivElement

let boardNr = 3 
let mutable moves: string = ""

let go (dir: Char) =
    let (board, history)= game.attemptMove(boardNr, moves, dir)
    gameScreen.textContent <- board
    moves <- history

gameScreen.textContent <- game.init(boardNr) 


downButton.onclick <- fun _ -> go ('d')
upButton.onclick <- fun _ -> go 'u'
rightButton.onclick <- fun _ -> go 'r' 
leftButton.onclick <- fun _ -> go 'l'

document.onkeydown <- fun (keyEvent: KeyboardEvent) -> go (keyEvent.key |> Seq.head)
                                 
