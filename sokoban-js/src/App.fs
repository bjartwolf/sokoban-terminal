module App

open Browser.Dom
open sokoban
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
gameScreen.textContent <- game.init(boardNr) 

// Register our listener
downButton.onclick <- fun _ ->
    let (board, history)= game.attemptMove(boardNr, moves, 'd')
    gameScreen.textContent <- board
    moves <- history

upButton.onclick <- fun _ ->
    let (board, history)= game.attemptMove(boardNr, moves, 'u')
    gameScreen.textContent <- board
    moves <- history

rightButton.onclick <- fun _ ->
    let (board, history)= game.attemptMove(boardNr, moves, 'r')
    gameScreen.textContent <- board
    moves <- history

leftButton.onclick <- fun _ ->
    let (board, history)= game.attemptMove(boardNr, moves, 'l')
    gameScreen.textContent <- board
    moves <- history
