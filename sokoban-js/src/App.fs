module App

open Browser.Dom
open sokoban
// Mutable variable to count the number of times we clicked the button
let mutable count = 0

// Get a reference to our button and cast the Element to an HTMLButtonElement
let downButton = document.querySelector(".button-down") :?> Browser.Types.HTMLButtonElement
let upButton = document.querySelector(".button-up") :?> Browser.Types.HTMLButtonElement
let gameScreen = document.querySelector(".game-screen") :?> Browser.Types.HTMLParagraphElement

gameScreen.textContent <- game.init(3) 

// Register our listener
downButton.onclick <- fun _ ->
    downButton.innerText <- sprintf "You cxlicked: %i time(s)" count
