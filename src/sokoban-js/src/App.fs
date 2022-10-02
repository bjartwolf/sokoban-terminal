module App

open Browser.Dom
open sokoban
open Browser.Types
open System
open Fable.Core
open Fable.Core.JsInterop

let gameScreen = document.getElementById("game-screen") :?> Browser.Types.HTMLDivElement
let canvas = document.getElementById("grafikk") :?> Browser.Types.HTMLCanvasElement
let canvaxContext = canvas.getContext_2d() 
let sprites: HTMLImageElement = document.getElementById("sprites") :?> Browser.Types.HTMLImageElement

let mutable boardNr = 3 
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

canvas.width <- 26.0*48.0
canvas.height<- 26.0*48.0

let renderImage (board:string) =
    let gameMap = game.parseBoard board 
    gameMap |> Map.iter ( fun ((x,y):int*int) (tile:char)-> 
            match tile with 
                | '#' -> draw Wall x y 
                | '@' -> draw Player x y 
                | ' ' -> draw Blank x y 
                | '.' -> draw Goal x y 
                | '+' -> draw Player x y 
                | '*' -> draw BoxOnGoal x y 
                | '$' -> draw Coin x y 
                | _ -> draw Blank x y
            () 
            )
    ()
 
canvaxContext?imageSmoothingEnabled <- false
canvaxContext?mozImageSmoothingEnabled <- false

let go (dir: Char) =
    let (board, history)= game.attemptMove(boardNr, moves, dir)
    gameScreen.textContent <- board
    renderImage board
    moves <- history

let init nr = 
    canvaxContext.clearRect(0,0,canvas.width, canvas.height)
    moves <- ""
    gameScreen.textContent <- game.init(nr) 
    renderImage (game.init(nr))

init boardNr
document.onkeydown <- fun (keyEvent: KeyboardEvent) -> 
    match keyEvent.key with
        | "a" | "h" | "ArrowLeft" -> go 'l'
        | "w"| "k" | "ArrowUp" -> go 'u'
        | "s" | "j" |"ArrowDown" -> go 'd'
        | "d" | "l" |"ArrowRight" -> go 'r'
        | "u" | "b"  -> go 'b'
        | "0" -> boardNr <- 0;init 0 
        | "1" -> boardNr <- 1; init 1 
        | "3" -> boardNr <- 3; init 3 
        | _ -> () 
                                 
