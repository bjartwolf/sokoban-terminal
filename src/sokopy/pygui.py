from tkinter import * 
from game.library import *

gameNr = 3
global board
board = init(gameNr)
global history
history = ""

tk = Tk()
tk.geometry("500x500")

canvas= Canvas(tk, width= 500, height= 500, bg="blue")

tekst = canvas.create_text(250, 250, text=board, fill="white", font=('TkFixedFont'))
canvas.pack()

def left(self):
     global history, board, gameNr
     board, history = attempt_move(gameNr, history, "l")
     canvas.itemconfig(tekst,text = board)
     tk.update()
     return board, history

def right(self):
     global history, board, gameNr
     board, history = attempt_move(gameNr, history, "r")
     canvas.itemconfig(tekst,text = board)
     tk.update()
     return board, history

def up(self):
     global history, board, gameNr
     board, history = attempt_move(gameNr, history, "u")
     canvas.itemconfig(tekst,text = board)
     tk.update()
     return board, history

def down(self):
     global history, board, gameNr
     board, history = attempt_move(gameNr, history, "d")
     canvas.itemconfig(tekst,text = board)
     tk.update()
     return board, history

def board0(self):
    global history, board, gameNr
    history = ""
    gameNr = 0
    board = init(gameNr) 
    canvas.itemconfig(tekst,text = board)
    tk.update()
    return history, board, gameNr

def board1(self):
    global history, board, gameNr
    history = ""
    gameNr = 1
    board = init(gameNr) 
    canvas.itemconfig(tekst,text = board)
    tk.update()
    return history, board, gameNr

def board3(self):
    global history, board, gameNr
    history = ""
    gameNr = 3
    board = init(gameNr) 
    canvas.itemconfig(tekst,text = board)
    tk.update()
    return history, board, gameNr



tk.bind('a',left)
tk.bind('h',left)
tk.bind('w',up)
tk.bind('k',up)
tk.bind('f',right)
tk.bind('l',right)
tk.bind('s',down)
tk.bind('j',down)
tk.bind('0',board0)
tk.bind('1',board1)
tk.bind('3',board3)

tk.mainloop ()
