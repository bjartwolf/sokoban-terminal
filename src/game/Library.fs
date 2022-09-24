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
        let whichLine = List.findIndex (fun l -> l |> List.contains player) board
        let lineWithPlayer = board.[whichLine]
        let playerPosition = lineWithPlayer |> List.findIndex (fun pos -> pos = player)
        (playerPosition, whichLine) 

    let getTile (board: Board) ((x,y): int*int): Char option =
        try
            let piece = board.[y].[x]  
            Some piece
        with
            | _ -> None

    let legalMove (board: Board) ((Δx,Δy): int*int): bool = 
        let (x,y) = getPlayerPosition board
        let tile = getTile board (x+Δx,y+Δy)
        match tile with
            | Some '#' -> false 
            | Some _ -> true
            | None -> false
        
    let move (board: Board) ((Δx,Δy): int*int): Board = 
        let (x,y) = getPlayerPosition board
        let lineWithPlayer = board.[y] 
        let lineWithoutPlayer = List.updateAt x floor lineWithPlayer // must be smarter depending on what was there
        let horizontalMove = Δx <>0
        if horizontalMove then 
            let lineWithPlayerInNewPos = List.updateAt (x+Δx) player lineWithoutPlayer
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
