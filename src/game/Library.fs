namespace sokoban 
open System

module game =
    type Board = Map<(int*int),char> 

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
        let a = board.Split(Environment.NewLine) 
                |> Array.toList 
                |> List.map (fun l -> l.ToCharArray() |> Array.toList)
                |> List.filter (fun l -> l <> [])

        let b = a |> List.mapi (fun y e -> (y,e))
        let c = b |> List.map (fun (y,e) -> e |> List.mapi (fun x c -> (x,y),c))
                  |> List.collect (id)
        c |> Map

    //let getPlayerPosition (board: Board): int*int =
    //    let whichLine = List.findIndex (fun l -> List.contains player l || List.contains player_on_goal_square l) board
    //    let lineWithPlayer = board.[whichLine]
    //    let playerPosition = lineWithPlayer |> List.findIndex (fun pos -> pos = player || pos = player_on_goal_square)
    //    (playerPosition, whichLine) 

    //let getTile (board: Board) ((x,y): int*int): Char option =
    //    try
    //        let piece = board.[y].[x]  
    //        Some piece
    //    with
    //        | _ -> None

    //let canPushBox (board: Board) ((x,y): int*int) ((Δx,Δy): int*int): bool = 
    //    let tileBehindBox = getTile board (x+2*Δx, y+2*Δy)
    //    tileBehindBox = Some floor || tileBehindBox = Some goal_square 

    //let legalMove (board: Board) (Δ: int*int): bool = 
    //    let (Δx,Δy) = Δ
    //    let (x,y) = getPlayerPosition board
    //    let pos' = x+Δx,y+Δy
    //    let t' = getTile board pos' 
    //    match t' with
    //        | Some c when c = wall -> false 
    //        | Some c when c = box || c = box_on_goal_square -> canPushBox board (x,y) (Δx,Δy)
    //        | Some _ -> true
    //        | None -> false
        
    //let move (board: Board) ((Δx,Δy): int*int): Board = 
    //    let (x,y) = getPlayerPosition board
    //    let tile = getTile board (x,y)
    //    let tile_Δ = getTile board (x+Δx,y+Δy)


    //    let tile_Δ' = if tile_Δ = Some goal_square || tile_Δ= Some box_on_goal_square then
    //                                 player_on_goal_square 
    //                              else 
    //                                 player

    //    let isPushingBox =  tile_Δ = Some box || tile_Δ = Some box_on_goal_square 

    //    let horizontalMove = Δx <>0

    //    let whatWasUnderPlayer = if tile = Some player then floor else goal_square
    //    let lineWithPlayer = board.[y] 
    //    let lineWithoutPlayer = List.updateAt x whatWasUnderPlayer lineWithPlayer
    //    let isBoxPushedOnGoalSquare = getTile board (x+2*Δx,y+2*Δy) = Some goal_square 

    //    if horizontalMove then 
    //        let lineWithPlayerInNewPos = if isPushingBox then
    //                                        let boxTile = if isBoxPushedOnGoalSquare then box_on_goal_square else box
    //                                        List.updateAt (x+Δx) tile_Δ' lineWithoutPlayer
    //                                        |> List.updateAt (x+2*Δx) boxTile 
    //                                     else
    //                                        List.updateAt (x+Δx) tile_Δ' lineWithoutPlayer
    //        board |> List.updateAt y lineWithPlayerInNewPos 
    //    else
    //        let newLineWithPlayer = board.[y+Δy] |> List.updateAt x player 
    //        if (isPushingBox) then 
    //            let boxTile = if isBoxPushedOnGoalSquare then box_on_goal_square else box
    //            let lineWithBox = board.[y+2*Δy] |> List.updateAt x boxTile 
    //            board |> List.updateAt y lineWithoutPlayer |> List.updateAt (y+Δy) newLineWithPlayer
    //                  |> List.updateAt (y+2*Δy) lineWithBox 
    //        else
    //            board |> List.updateAt y lineWithoutPlayer |> List.updateAt (y+Δy) newLineWithPlayer

    //// y is first index , growing down on the board
    //// x is index in array, growing to the right 
    //let movePlayer (board: Board) (keypress: Char): Board =
    //    let Δ = match keypress with
    //                            | 'h' -> (-1,0) 
    //                            | 'j' -> (0,1) 
    //                            | 'l' -> (1,0) 
    //                            | 'k' -> (0,-1) 
    //                            | _ -> failwith "not a valid move" 
    //    if (legalMove board Δ) then
    //        move board Δ
    //    else 
    //        board

    let serializeBoard (board: Board) : string =
        let foo = board |> Map.toList  |> List.groupBy (fun ((_,y),_) -> y) 
        let bar = foo |> List.map (snd) |> List.map (List.map (snd)) 
        let baz = bar |> List.map (Array.ofList)
        let foobar = baz |> List.map (String)
        let foobaz = foobar |> String.concat Environment.NewLine
        foobaz

    //let makeMove(board: string, move: Char) = 
    //    let board = parseBoard board
    //    let newBoard = movePlayer board move
    //    serializeBoard newBoard
