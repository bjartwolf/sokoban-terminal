import { head, skip, toArray } from "../../sokoban-js/src/.fable/fable-library.3.2.9/Seq.js";
import { FSharpMap__TryFind, FSharpMap__get_Values, FSharpMap__Add, empty as empty_1, toList, add, tryFind, filter as filter_1, keys, ofSeq } from "../../sokoban-js/src/.fable/fable-library.3.2.9/Map.js";
import { toArray as toArray_1, ofArray, empty, filter, mapIndexed, map, collect } from "../../sokoban-js/src/.fable/fable-library.3.2.9/List.js";
import { createAtom, numberHash, compareArrays, equals } from "../../sokoban-js/src/.fable/fable-library.3.2.9/Util.js";
import { isNullOrEmpty, remove, join, split } from "../../sokoban-js/src/.fable/fable-library.3.2.9/String.js";
import { List_groupBy } from "../../sokoban-js/src/.fable/fable-library.3.2.9/Seq2.js";

export function init(board_nr) {
    const board0 = " \r\n###\r\n#.#\r\n#$####\r\n#@ $.#\r\n######";
    const board1 = " \r\n###\r\n######\r\n#. $ #\r\n# $  #\r\n#  @$#\r\n#.  .#\r\n######";
    const board3 = "\r\n####\r\n#  #######\r\n#  ......#\r\n#  ##### #\r\n##   $@# #\r\n #  $$$$ #\r\n ##  $ ###\r\n  #    #\r\n  ######";
    let board;
    switch (board_nr) {
        case 0: {
            board = board0;
            break;
        }
        case 1: {
            board = board1;
            break;
        }
        case 3: {
            board = board3;
            break;
        }
        default: {
            throw (new Error("I don\u0027t have that"));
        }
    }
    const removeFirstNewline = toArray(skip("\n".length, board.split("")));
    return removeFirstNewline.join('');
}

export const wall = "#";

export const player = "@";

export const player_on_goal_square = "+";

export const box = "$";

export const box_on_goal_square = "*";

export const goal_square = ".";

export const floor = " ";

export const keypress_left = "l";

export const keypress_down = "d";

export const keypress_up = "u";

export const keypress_right = "r";

export function parseBoard(board) {
    return ofSeq(collect((x_1) => x_1, map((tupledArg) => {
        const y_1 = tupledArg[0] | 0;
        const e_1 = tupledArg[1];
        return mapIndexed((x, c) => [[x, y_1], c], e_1);
    }, mapIndexed((y, e) => [y, e], filter((l_1) => (!equals(l_1, empty())), map((l) => ofArray(l.split("")), ofArray(split(board, "\n".split("")))))))), {
        Compare: (x_2, y_2) => compareArrays(x_2, y_2),
    });
}

export function getPlayerPosition(board) {
    return head(keys(filter_1((_arg1, t) => {
        if (t === player) {
            return true;
        }
        else {
            return t === player_on_goal_square;
        }
    }, board)));
}

export function getTile(board, pos_0, pos_1) {
    const pos = [pos_0, pos_1];
    return tryFind(pos, board);
}

export function canPushBox(board, _arg2_0, _arg2_1, _arg1_0, _arg1_1) {
    const _arg2 = [_arg2_0, _arg2_1];
    const _arg1 = [_arg1_0, _arg1_1];
    const y = _arg2[1] | 0;
    const x = _arg2[0] | 0;
    const $0394y = _arg1[1] | 0;
    const $0394x = _arg1[0] | 0;
    const tileBehindBox = getTile(board, x + (2 * $0394x), y + (2 * $0394y));
    if (equals(tileBehindBox, floor)) {
        return true;
    }
    else {
        return equals(tileBehindBox, goal_square);
    }
}

export function legalMove(board, $0394_0, $0394_1) {
    let c_2, c;
    const $0394 = [$0394_0, $0394_1];
    const $0394_2 = $0394;
    const $0394y = $0394_2[1] | 0;
    const $0394x = $0394_2[0] | 0;
    const patternInput = getPlayerPosition(board);
    const y = patternInput[1] | 0;
    const x = patternInput[0] | 0;
    const pos$0027 = [x + $0394x, y + $0394y];
    const t$0027 = getTile(board, pos$0027[0], pos$0027[1]);
    let pattern_matching_result;
    if (t$0027 != null) {
        if ((c = t$0027, c === wall)) {
            pattern_matching_result = 0;
        }
        else {
            pattern_matching_result = 1;
        }
    }
    else {
        pattern_matching_result = 1;
    }
    switch (pattern_matching_result) {
        case 0: {
            return false;
        }
        case 1: {
            let pattern_matching_result_1;
            if (t$0027 != null) {
                if ((c_2 = t$0027, (c_2 === box) ? true : (c_2 === box_on_goal_square))) {
                    pattern_matching_result_1 = 0;
                }
                else {
                    pattern_matching_result_1 = 1;
                }
            }
            else {
                pattern_matching_result_1 = 1;
            }
            switch (pattern_matching_result_1) {
                case 0: {
                    return canPushBox(board, x, y, $0394x, $0394y);
                }
                case 1: {
                    if (t$0027 == null) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
        }
    }
}

export function move(board, _arg1_0, _arg1_1) {
    const _arg1 = [_arg1_0, _arg1_1];
    const $0394y = _arg1[1] | 0;
    const $0394x = _arg1[0] | 0;
    const patternInput = getPlayerPosition(board);
    const y = patternInput[1] | 0;
    const x = patternInput[0] | 0;
    const pos$0027 = [x + $0394x, y + $0394y];
    const tile = getTile(board, x, y);
    const tile_$0394 = getTile(board, x + $0394x, y + $0394y);
    const tile_$0394$0027 = (equals(tile_$0394, goal_square) ? true : equals(tile_$0394, box_on_goal_square)) ? player_on_goal_square : player;
    const isPushingBox = equals(tile_$0394, box) ? true : equals(tile_$0394, box_on_goal_square);
    const whatWasUnderPlayer = equals(tile, player) ? floor : goal_square;
    const boardWithoutPlayer = add([x, y], whatWasUnderPlayer, board);
    const boardWithPlayerBack = add(pos$0027, tile_$0394$0027, boardWithoutPlayer);
    if (isPushingBox) {
        const isBoxPushedOnGoalSquare = equals(getTile(board, x + (2 * $0394x), y + (2 * $0394y)), goal_square);
        const boxTile = isBoxPushedOnGoalSquare ? box_on_goal_square : box;
        return add([x + (2 * $0394x), y + (2 * $0394y)], boxTile, boardWithPlayerBack);
    }
    else {
        return boardWithPlayerBack;
    }
}

export function movePlayer(board, keypress) {
    let $0394;
    switch (keypress) {
        case "d": {
            $0394 = [0, 1];
            break;
        }
        case "l": {
            $0394 = [-1, 0];
            break;
        }
        case "r": {
            $0394 = [1, 0];
            break;
        }
        case "u": {
            $0394 = [0, -1];
            break;
        }
        default: {
            throw (new Error("There are only four known directions."));
        }
    }
    if (legalMove(board, $0394[0], $0394[1])) {
        return [move(board, $0394[0], $0394[1]), keypress];
    }
    else {
        return [board, void 0];
    }
}

export function serializeBoard(board) {
    return join("\n", map((arg00) => (arg00.join('')), map((list_4) => toArray_1(list_4), map((list_2) => map((tuple_1) => tuple_1[1], list_2), map((tuple) => tuple[1], List_groupBy((tupledArg) => {
        const y = tupledArg[0][1] | 0;
        return y | 0;
    }, toList(board), {
        Equals: (x, y_1) => (x === y_1),
        GetHashCode: (x) => numberHash(x),
    }))))));
}

export function makeMove(board, move_1) {
    const board_1 = parseBoard(board);
    const newBoard = movePlayer(board_1, move_1)[0];
    return serializeBoard(newBoard);
}

export let allKnownBoards = createAtom(empty_1());

export function startNewBoard(boardnr) {
    const board = parseBoard(init(boardnr));
    allKnownBoards(FSharpMap__Add(allKnownBoards(), [boardnr, ""], board), true);
    return serializeBoard(board);
}

export function wonGame(board) {
    if (!FSharpMap__get_Values(board).has(goal_square)) {
        return !FSharpMap__get_Values(board).has(player_on_goal_square);
    }
    else {
        return false;
    }
}

export function lookupBoard(boardnr, state) {
    const b = FSharpMap__TryFind(allKnownBoards(), [boardnr, state]);
    const matchValue = [b, state];
    if (matchValue[0] == null) {
        if (matchValue[1] === "") {
            return parseBoard(init(boardnr));
        }
        else {
            const lastMove = state[state.length - 1];
            const previousState = remove(state, state.length - 1);
            const board_1 = movePlayer(lookupBoard(boardnr, previousState), lastMove)[0];
            return board_1;
        }
    }
    else {
        const board = matchValue[0];
        return board;
    }
}

export function rememberBoard(boardnr, state, board) {
    allKnownBoards(FSharpMap__Add(allKnownBoards(), [boardnr, state], board), true);
}

export function attemptMove(boardnr, history, move_1) {
    let move_2;
    const writeWinOnBoard = (board) => add([3, 0], "N", add([2, 0], "I", add([1, 0], "W", board)));
    if (move_1 === "b") {
        const history_undoed = isNullOrEmpty(history) ? "" : remove(history, history.length - 1);
        const board_1 = lookupBoard(boardnr, history_undoed);
        if (wonGame(board_1)) {
            return [serializeBoard(writeWinOnBoard(board_1)), history_undoed];
        }
        else {
            return [serializeBoard(board_1), history_undoed];
        }
    }
    else {
        const board_2 = lookupBoard(boardnr, history);
        const patternInput = movePlayer(board_2, move_1);
        const moveMade = patternInput[1];
        const board$0027 = patternInput[0];
        const history$0027 = history + ((moveMade == null) ? "" : ((move_2 = moveMade, move_2)));
        rememberBoard(boardnr, history$0027, board$0027);
        if (wonGame(board$0027)) {
            return [serializeBoard(writeWinOnBoard(board$0027)), history$0027];
        }
        else {
            return [serializeBoard(board$0027), history$0027];
        }
    }
}

