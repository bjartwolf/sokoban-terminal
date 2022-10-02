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

type Sprite = Blank | Player | Goal | Wall | Coin | BoxOnGoal

let draw (sprite: Sprite) (posx:int) (posy:int)  =
    let drawImage x y = canvaxContext.drawImage(U3.Case1 sprites, 
                                float x * 16.0,
                                float y * 16.0,
                                16.0,
                                16.0,
                                float posx * 48.0,
                                float posy * 48.0,
                                48.0,
                                48.0)
    match sprite with 
        | Blank -> drawImage 0 0 
        | Player -> drawImage 0 1
        | Goal -> drawImage 0 2
        | Wall -> drawImage 1 0 
        | Coin-> drawImage 1 1 
        | BoxOnGoal -> drawImage 1 2

draw Wall 0 0
draw Wall 0 1
draw Wall 1 0
draw Player 0 2
draw Player 0 2
draw Player 0 3

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
                                 
