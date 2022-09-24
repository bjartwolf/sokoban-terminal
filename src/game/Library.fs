namespace sokoban 
open System

module game =
    type Board = char list list

    let init (): String = 
        let board = @"
####
#  #######
#  ......#
#  ##### #
##   $@# #
#  $$$$ #
##  $ ###
#    #
######" 
        let removeFirstNewline = board |> Seq.skip (Environment.NewLine.Length) |> Seq.toArray
        String(removeFirstNewline)

    // http://www.sokobano.de/wiki/index.php?title=Level_format
    let wall = '#'
    let player = '@'
    let player_on_goal_square = '+'
    let box = '$'
    let box_on_goal_square = '*'
    let goal_square = '.'
    let floor = ' '

    let keypress_left = 'h'
    let keypress_down = 'j'
    let keypress_up = 'k'
    let keypress_right = 'l'

    let parseBoard (board:string): Board = 
        board.Split(Environment.NewLine) 
            |> Array.toList 
            |> List.map (fun l -> l.ToCharArray() |> Array.toList)
            |> List.filter (fun l -> l <> [])

    let getPlayerPosition (board: Board): int*int =
        let whichLine = List.findIndex (fun l -> List.contains player l || List.contains player_on_goal_square l) board
        let lineWithPlayer = board.[whichLine]
        let playerPosition = lineWithPlayer |> List.findIndex (fun pos -> pos = player || pos = player_on_goal_square)
        (playerPosition, whichLine) 

    let getTile (board: Board) ((x,y): int*int): Char option =
        try
            let piece = board.[y].[x]  
            Some piece
        with
            | _ -> None

    let canPushBox (board: Board) ((x,y): int*int) ((Δx,Δy): int*int): bool = 
        let tileBehindBox = getTile board (x+2*Δx, y+2*Δy)
        let isFloorBehindBox = tileBehindBox = Some floor 
        isFloorBehindBox 

    let legalMove (board: Board) ((Δx,Δy): int*int): bool = 
        let (x,y) = getPlayerPosition board
        let attemptedNewPosition = x+Δx,y+Δy
        let tileInNewPosition = getTile board attemptedNewPosition 
        match tileInNewPosition with
            | Some '#' -> false 
            | Some '$' when not (canPushBox board (x,y) (Δx,Δy)) -> false 
            | Some '$' when (canPushBox board (x,y) (Δx,Δy)) -> true 
            | Some _ -> true
            | None -> false
        
    let move (board: Board) ((Δx,Δy): int*int): Board = 
        let (x,y) = getPlayerPosition board
        let lineWithPlayer = board.[y] 
        let whatWasUnderPlayer = match getTile board (x,y) with
                                        | Some character when character = player -> floor 
                                        | Some character when character = player_on_goal_square -> goal_square 
                                        | _ -> failwith "Should not happen" 
        let lineWithoutPlayer = List.updateAt x whatWasUnderPlayer lineWithPlayer

        let tileWithPlayerOnTop = match getTile board (x+Δx,y+Δy) with
                                        | Some character when character = goal_square -> player_on_goal_square 
                                        | Some _ -> player 
                                        | _ -> failwith "Should not happen" 
        let isPushingBox =  match getTile board (x+Δx,y+Δy) with
                                        | Some character when character = box -> true 
                                        | _ -> false 

        let horizontalMove = Δx <>0
        if horizontalMove then 
            let lineWithPlayerInNewPos = if isPushingBox then
                                            List.updateAt (x+Δx) tileWithPlayerOnTop lineWithoutPlayer
                                            |> List.updateAt (x+2*Δx) box 
                                         else
                                            List.updateAt (x+Δx) tileWithPlayerOnTop lineWithoutPlayer
            board |> List.updateAt y lineWithPlayerInNewPos 
        else
            let newLineWithPlayer = board.[y+Δy] |> List.updateAt x player 
            board |> List.updateAt y lineWithoutPlayer |> List.updateAt (y+Δy) newLineWithPlayer

    // y is first index , growing down on the board
    // x is index in array, growing to the right 
    let movePlayer (board: Board) (keypress: Char): Board =
        let Δ = match keypress with
                                | 'h' -> (-1,0) 
                                | 'j' -> (0,1) 
                                | 'l' -> (1,0) 
                                | 'k' -> (0,-1) 
                                | _ -> failwith "not a valid move" 
        if (legalMove board Δ) then
            move board Δ
        else 
            board

    let serializeBoard (board: Board) : string =
        let foo = board |> List.map (fun f -> new String (f |> List.toArray))
        foo |> String.concat Environment.NewLine 

    let makeMove(board: string, move: Char) = 
        let board = parseBoard board
        let newBoard = movePlayer board move
        serializeBoard newBoard
