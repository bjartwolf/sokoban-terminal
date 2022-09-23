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
        
