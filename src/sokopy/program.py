from typing import Tuple
from game.library import attempt_move
from fable_modules.fable_library.string import (to_console, printf)

to_console(printf("Game 1"))

pattern_input_00404: Tuple[str, str] = attempt_move(1, "urduullddrd", "l")

final_state: str = pattern_input_00404[0]

to_console(printf("%A"))(final_state)

to_console(printf(""))

to_console(printf("Game 3"))

pattern_input_004010_002D1: Tuple[str, str] = attempt_move(3, "urduullddrd", "l")

final_state3: str = pattern_input_004010_002D1[0]

to_console(printf("%A"))(final_state3)

