module App

open Browser.Dom
open sokoban
open Browser.Types
open System
open Fable.Core

let gameScreen = document.getElementById("game-screen") :?> Browser.Types.HTMLDivElement
let canvas = document.getElementById("grafikk") :?> Browser.Types.HTMLCanvasElement
let canvaxContext = canvas.getContext_2d() 
let sprites: HTMLImageElement = document.getElementById("sprites") :?> Browser.Types.HTMLImageElement

let boardNr = 3 
let mutable moves: string = ""

canvaxContext.drawImage(U3.Case1 sprites, 
                            0.0,
                            0.0,
                            126.0,
                            46.0,
                            0.0,
                            1.0,
                            48.0,
                            48.0)

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
                                 
