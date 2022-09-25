namespace sokoban 
open System

module game =
    // y is first index , growing down on the board
    // x is index in array, growing to the right 
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
        board.Split(Environment.NewLine) 
            |> Array.toList
            |> List.map (fun l -> l.ToCharArray() |> Array.toList)
            |> List.filter (fun l -> l <> [])
            |> List.mapi (fun y e -> (y,e))
            |> List.map (fun (y,e) -> e |> List.mapi (fun x c -> (x,y),c))
            |> List.collect (id)
            |> Map

    let getPlayerPosition (board: Board): int*int =
          board |> Map.filter (fun _ t -> t=player || t=player_on_goal_square )|> Map.keys |> Seq.head 

    let getTile (board: Board) (pos: int*int): Char option = Map.tryFind pos board

    let canPushBox (board: Board) ((x,y): int*int) ((Δx,Δy): int*int): bool = 
        let tileBehindBox = getTile board (x+2*Δx, y+2*Δy)
        tileBehindBox = Some floor || tileBehindBox = Some goal_square 

    let legalMove (board: Board) (Δ: int*int): bool = 
        let (Δx,Δy) = Δ
        let (x,y) = getPlayerPosition board
        let pos' = x+Δx,y+Δy
        let t' = getTile board pos' 
        match t' with
            | Some c when c = wall -> false 
            | Some c when c = box || c = box_on_goal_square -> canPushBox board (x,y) (Δx,Δy)
            | Some _ -> true
            | None -> false
        
    let move (board: Board) ((Δx,Δy): int*int): Board = 
        let (x,y) = getPlayerPosition board
        let pos' = x+Δx,y+Δy
        let tile = getTile board (x,y)
        let tile_Δ = getTile board (x+Δx,y+Δy)


        let tile_Δ' = if tile_Δ = Some goal_square || tile_Δ= Some box_on_goal_square then
                                     player_on_goal_square 
                                  else 
                                     player

        let isPushingBox =  tile_Δ = Some box || tile_Δ = Some box_on_goal_square 

        let whatWasUnderPlayer = if tile = Some player then floor else goal_square
        let boardWithoutPlayer = board |> Map.add (x,y) whatWasUnderPlayer
        let boardWithPlayerBack = boardWithoutPlayer |> Map.add pos' tile_Δ'

        if isPushingBox then 
            let isBoxPushedOnGoalSquare = getTile board (x+2*Δx,y+2*Δy) = Some goal_square 
            let boxTile = if isBoxPushedOnGoalSquare then box_on_goal_square else box
            boardWithPlayerBack |> Map.add (x+2*Δx,y+2*Δy) boxTile
        else 
            boardWithPlayerBack 

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
        board |> Map.toList  
              |> List.groupBy (fun ((_,y),_) -> y) 
              |> List.map (snd) |> List.map (List.map (snd)) 
              |> List.map (Array.ofList)
              |> List.map (String)
              |> String.concat Environment.NewLine

    let makeMove(board: string, move: Char) = 
        let board = parseBoard board
        let newBoard = movePlayer board move
        serializeBoard newBoard
