from typing import (Tuple, Literal, Callable, Any, Optional)
from fable_modules.fable_library.list import (collect, FSharpList, map, map_indexed, filter, empty, of_array, to_array as to_array_1)
from fable_modules.fable_library.map import (of_seq, keys, filter as filter_1, try_find, add, to_list, empty as empty_1, FSharpMap__Add, FSharpMap__get_Values, FSharpMap__TryFind)
from fable_modules.fable_library.seq import (to_array, skip, head, contains)
from fable_modules.fable_library.seq2 import List_groupBy
from fable_modules.fable_library.string import (split, join, remove, is_null_or_empty)
from fable_modules.fable_library.types import Array
from fable_modules.fable_library.util import (equals, compare_arrays, number_hash, create_atom, string_hash)

def init(board_nr: int) -> str:
    board: str
    if board_nr == 0:
        board = " \r\n###\r\n#.#\r\n#$####\r\n#@ $.#\r\n######"

    elif board_nr == 1:
        board = " \r\n###\r\n######\r\n#. $ #\r\n# $  #\r\n#  @$#\r\n#.  .#\r\n######"

    elif board_nr == 3:
        board = "\r\n####\r\n#  #######\r\n#  ......#\r\n#  ##### #\r\n##   $@# #\r\n #  $$$$ #\r\n ##  $ ###\r\n  #    #\r\n  ######"

    else: 
        raise Exception("I don\'t have that")

    remove_first_newline: Array[str] = to_array(skip(len("\n"), board))
    return ''.join(remove_first_newline)


wall: str = "#"

player: str = "@"

player_on_goal_square: str = "+"

box: str = "$"

box_on_goal_square: str = "*"

goal_square: str = "."

floor: str = " "

keypress_left: str = "l"

keypress_down: str = "d"

keypress_up: str = "u"

keypress_right: str = "r"

def parse_board(board: str) -> Any:
    def mapping_4(x_1: FSharpList[Tuple[Tuple[int, int], str]], board: str=board) -> FSharpList[Tuple[Tuple[int, int], str]]:
        return x_1

    def mapping_3(tupled_arg: Tuple[int, FSharpList[str]], board: str=board) -> FSharpList[Tuple[Tuple[int, int], str]]:
        def mapping_2(x: int, c: str, tupled_arg: Tuple[int, FSharpList[str]]=tupled_arg) -> Tuple[Tuple[int, int], str]:
            return ((x, tupled_arg[0]), c)

        return map_indexed(mapping_2, tupled_arg[1])

    def mapping_1(y: int, e: FSharpList[str], board: str=board) -> Tuple[int, FSharpList[str]]:
        return (y, e)

    def predicate(l_1: FSharpList[str], board: str=board) -> bool:
        return not equals(l_1, empty())

    def mapping(l: str, board: str=board) -> FSharpList[str]:
        return of_array(list(l))

    class ObjectExpr1:
        @property
        def Compare(self) -> Callable[[Tuple[int, int], Tuple[int, int]], int]:
            def _arrow0(x_2: Tuple[int, int], y_2: Tuple[int, int]) -> int:
                return compare_arrays(x_2, y_2)

            return _arrow0

    return of_seq(collect(mapping_4, map(mapping_3, map_indexed(mapping_1, filter(predicate, map(mapping, of_array(split(board, list("\n")))))))), ObjectExpr1())


def get_player_position(board: Any) -> Tuple[int, int]:
    def predicate(_arg: Tuple[int, int], t: str, board: Any=board) -> bool:
        if t == player:
            return True

        else: 
            return t == player_on_goal_square


    return head(keys(filter_1(predicate, board)))


def get_tile(board: Any, pos_: int, pos__1: int) -> Optional[str]:
    return try_find((pos_, pos__1), board)


def can_push_box(board: Any, _arg2_: int, _arg2__1: int, _arg1_: int, _arg1__1: int) -> bool:
    _arg: Tuple[int, int] = (_arg2_, _arg2__1)
    _arg_1: Tuple[int, int] = (_arg1_, _arg1__1)
    tile_behind_box: Optional[str] = get_tile(board, _arg[0] + (2 * _arg_1[0]), _arg[1] + (2 * _arg_1[1]))
    if equals(tile_behind_box, floor):
        return True

    else: 
        return equals(tile_behind_box, goal_square)



def legal_move(board: Any, Δ_: int, Δ__1: int) -> bool:
    Δ_1: Tuple[int, int] = (Δ_, Δ__1)
    Δy: int = Δ_1[1] or 0
    Δx: int = Δ_1[0] or 0
    pattern_input: Tuple[int, int] = get_player_position(board)
    y: int = pattern_input[1] or 0
    x: int = pattern_input[0] or 0
    pos_0027: Tuple[int, int] = (x + Δx, y + Δy)
    t_0027: Optional[str] = get_tile(board, pos_0027[0], pos_0027[1])
    if t_0027 is None:
        return False

    elif t_0027 == wall:
        c_2: str = t_0027
        return False

    else: 
        def _arrow2(__unit: Literal[None]=None, board: Any=board, Δ_: int=Δ_, Δ__1: int=Δ__1) -> bool:
            c_1: str = t_0027
            return True if (c_1 == box) else (c_1 == box_on_goal_square)

        if _arrow2():
            c_3: str = t_0027
            return can_push_box(board, x, y, Δx, Δy)

        else: 
            return True




def move(board: Any, _arg1_: int, _arg1__1: int) -> Any:
    _arg: Tuple[int, int] = (_arg1_, _arg1__1)
    Δy: int = _arg[1] or 0
    Δx: int = _arg[0] or 0
    pattern_input: Tuple[int, int] = get_player_position(board)
    y: int = pattern_input[1] or 0
    x: int = pattern_input[0] or 0
    pos_0027: Tuple[int, int] = (x + Δx, y + Δy)
    tile: Optional[str] = get_tile(board, x, y)
    tile_Δ: Optional[str] = get_tile(board, x + Δx, y + Δy)
    tile_Δ_0027: str = player_on_goal_square if (True if equals(tile_Δ, goal_square) else equals(tile_Δ, box_on_goal_square)) else player
    is_pushing_box: bool = True if equals(tile_Δ, box) else equals(tile_Δ, box_on_goal_square)
    board_with_player_back: Any = add(pos_0027, tile_Δ_0027, add((x, y), floor if equals(tile, player) else goal_square, board))
    if is_pushing_box:
        return add((x + (2 * Δx), y + (2 * Δy)), box_on_goal_square if equals(get_tile(board, x + (2 * Δx), y + (2 * Δy)), goal_square) else box, board_with_player_back)

    else: 
        return board_with_player_back



def move_player(board: Any, keypress: str) -> Tuple[Any, Optional[str]]:
    Δ: Tuple[int, int]
    if keypress == "d":
        Δ = (0, 1)

    elif keypress == "l":
        Δ = (-1, 0)

    elif keypress == "r":
        Δ = (1, 0)

    elif keypress == "u":
        Δ = (0, -1)

    else: 
        raise Exception("There are only four known directions.")

    if legal_move(board, Δ[0], Δ[1]):
        return (move(board, Δ[0], Δ[1]), keypress)

    else: 
        return (board, None)



def serialize_board(board: Any) -> str:
    def mapping_4(arg: Array[str], board: Any=board) -> str:
        return ''.join(arg)

    def mapping_3(list_5: FSharpList[str], board: Any=board) -> Array[str]:
        return to_array_1(list_5)

    def mapping_2(list_3: FSharpList[Tuple[Tuple[int, int], str]], board: Any=board) -> FSharpList[str]:
        def mapping_1(tuple_1: Tuple[Tuple[int, int], str], list_3: FSharpList[Tuple[Tuple[int, int], str]]=list_3) -> str:
            return tuple_1[1]

        return map(mapping_1, list_3)

    def mapping(tuple: Tuple[int, FSharpList[Tuple[Tuple[int, int], str]]], board: Any=board) -> FSharpList[Tuple[Tuple[int, int], str]]:
        return tuple[1]

    def projection(tupled_arg: Tuple[Tuple[int, int], str], board: Any=board) -> int:
        return tupled_arg[0][1]

    class ObjectExpr5:
        @property
        def Equals(self) -> Callable[[int, int], bool]:
            def _arrow3(x: int, y_1: int) -> bool:
                return x == y_1

            return _arrow3

        @property
        def GetHashCode(self) -> Callable[[int], int]:
            def _arrow4(x: int) -> int:
                return number_hash(x)

            return _arrow4

    return join("\n", map(mapping_4, map(mapping_3, map(mapping_2, map(mapping, List_groupBy(projection, to_list(board), ObjectExpr5()))))))


def make_move(board: str, move_1: str) -> str:
    return serialize_board(move_player(parse_board(board), move_1)[0])


class ObjectExpr7:
    @property
    def Compare(self) -> Callable[[Tuple[int, str], Tuple[int, str]], int]:
        def _arrow6(x: Tuple[int, str], y: Tuple[int, str]) -> int:
            return compare_arrays(x, y)

        return _arrow6


all_known_boards: Any = create_atom(empty_1(ObjectExpr7()))

def start_new_board(boardnr: int) -> str:
    board: Any = parse_board(init(boardnr))
    all_known_boards(FSharpMap__Add(all_known_boards(), (boardnr, ""), board), True)
    return serialize_board(board)


def won_game(board: Any) -> bool:
    class ObjectExpr10:
        @property
        def Equals(self) -> Callable[[str, str], bool]:
            def _arrow8(x: str, y: str) -> bool:
                return x == y

            return _arrow8

        @property
        def GetHashCode(self) -> Callable[[str], int]:
            def _arrow9(x: str) -> int:
                return string_hash(x)

            return _arrow9

    if not contains(goal_square, FSharpMap__get_Values(board), ObjectExpr10()):
        class ObjectExpr13:
            @property
            def Equals(self) -> Callable[[str, str], bool]:
                def _arrow11(x_1: str, y_1: str) -> bool:
                    return x_1 == y_1

                return _arrow11

            @property
            def GetHashCode(self) -> Callable[[str], int]:
                def _arrow12(x_1: str) -> int:
                    return string_hash(x_1)

                return _arrow12

        return not contains(player_on_goal_square, FSharpMap__get_Values(board), ObjectExpr13())

    else: 
        return False



def lookup_board(boardnr: int, state: str) -> Any:
    b: Optional[Any] = FSharpMap__TryFind(all_known_boards(), (boardnr, state))
    if b is None:
        if state == "":
            return parse_board(init(boardnr))

        else: 
            last_move: str = state[len(state) - 1]
            return move_player(lookup_board(boardnr, remove(state, len(state) - 1)), last_move)[0]


    else: 
        return b



def remember_board(boardnr: int, state: str, board: Any) -> None:
    all_known_boards(FSharpMap__Add(all_known_boards(), (boardnr, state), board), True)


def attempt_move(boardnr: int, history: str, move_1: str) -> Tuple[str, str]:
    def write_win_on_board(board: Any, boardnr: int=boardnr, history: str=history, move_1: str=move_1) -> Any:
        return add((3, 0), "N", add((2, 0), "I", add((1, 0), "W", board)))

    if move_1 == "b":
        history_undoed: str = "" if is_null_or_empty(history) else remove(history, len(history) - 1)
        board_1: Any = lookup_board(boardnr, history_undoed)
        if won_game(board_1):
            return (serialize_board(write_win_on_board(board_1)), history_undoed)

        else: 
            return (serialize_board(board_1), history_undoed)


    else: 
        pattern_input: Tuple[Any, Optional[str]] = move_player(lookup_board(boardnr, history), move_1)
        move_made: Optional[str] = pattern_input[1]
        board_0027: Any = pattern_input[0]
        history_0027: str = history + ("" if (move_made is None) else move_made)
        remember_board(boardnr, history_0027, board_0027)
        if won_game(board_0027):
            return (serialize_board(write_win_on_board(board_0027)), history_0027)

        else: 
            return (serialize_board(board_0027), history_0027)



