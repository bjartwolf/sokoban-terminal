namespace game
open System

module gameControl =
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
        let parseLine (l: string): char list = l.ToCharArray() |> Array.toList
        let lines = board.Split(Environment.NewLine) |> Array.toList 
        let parseBoard = List.map parseLine
        parseBoard lines |> List.filter (fun l -> l <> [])
