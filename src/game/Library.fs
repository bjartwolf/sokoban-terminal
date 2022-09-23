namespace game

module gameControl =
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

    let getGameSmall () = @"
##
##"
    // http://www.sokobano.de/wiki/index.php?title=Level_format
    let wall = '#'
    let player = '@'
    let player_on_goal_square = '@'
    let box = '$'
    let box_on_goal_square = '*'
    let goal_square = '.'
    let floor = ' '



