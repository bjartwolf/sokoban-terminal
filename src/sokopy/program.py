from __future__ import annotations
from tkinter import (Tk, Frame, Label)
from typing import Tuple
from game.library import attempt_move
from fable_modules.fable_library.string import (to_console, printf)

to_console(printf("Game 1"))

pattern_input_00406_002D16: Tuple[str, str] = attempt_move(1, "urduullddrd", "l")

final_state: str = pattern_input_00406_002D16[0]

to_console(printf("%A"))(final_state)

to_console(printf(""))

to_console(printf("Game 3"))

pattern_input_004012_002D17: Tuple[str, str] = attempt_move(3, "urduullddrd", "l")

final_state3: str = pattern_input_004012_002D17[0]

to_console(printf("%A"))(final_state3)

root: Tk = Tk()

frame: Frame = Frame(root, width=300, height=300, bg="pink")

frame.pack()

label: Label = Label(frame, text=final_state3, fg="red", bg="blue")

label.place(x=0, y=0)

root.title("Fable Python Rocks on Tkinter!")

root.mainloop()

