namespace sokoban 
open System

module game =
    type Board = char list list

    let init () = @"
####
#  #######
#  ......#
#  ##### #
##   $@# #
#  $$$$ #
##  $ ###
#    #
######"

    // http://www.sokobano.de/wiki/index.php?title=Level_format
    let wall = '#'
    let player = '@'
    let player_on_goal_square = '@'
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
        (whichLine, playerPosition) 
        
    // y is first index , growing down on the board
    // x is index in array, growing to the right 
    let movePlayer (board: Board) (move: Char): Board =
        let (y,x) = getPlayerPosition board
        let (Δx,Δy)= match move with
                                | 'h' -> (-1,0) 
                                | 'j' -> (0,-1) 
                                | 'k' -> (1,0) 
                                | 'l' -> (0,1) 
                                | _ -> failwith "not a valid move" 
        let lineWithPlayer = board.[y] 
        let lineWithoutPlayer = List.updateAt x floor lineWithPlayer // must be smarter depending on what was there
        if (Δx <>0 ) then 
            let lineWithoutPlayer = List.updateAt x floor lineWithPlayer
            let lineWithPlayerInNewPos = List.updateAt (x+Δx) player lineWithoutPlayer
            board |> List.updateAt y lineWithPlayerInNewPos 
        else
            let newLineWithPlayer = board.[y+Δy] |> List.updateAt x player 
            board |> List.updateAt y lineWithoutPlayer |> List.updateAt (y+Δy) newLineWithPlayer
        

    let serializeBoard (board: Board) : string =
        let foo = board |> List.map (fun f -> new String (f |> List.toArray))
        foo |> String.concat Environment.NewLine 
