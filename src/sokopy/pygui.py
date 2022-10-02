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

def move(self, direction):
     global history, board, gameNr
     board, history = attempt_move(gameNr, history, direction)
     canvas.itemconfig(tekst,text = board)
     tk.update()
     return board, history

def left(self):
    move(self, "l")

def right(self):
    move(self, "r")

def up(self):
    move(self,"u")

def down(self):
    move(self, "d")

def changeBoard(self, boardNr):
    global history, board, gameNr
    history = ""
    gameNr = boardNr 
    board = init(gameNr) 
    canvas.itemconfig(tekst,text = board)
    tk.update()
    return history, board, gameNr

def board0(self):
    changeBoard(self, 0)

def board1(self):
    changeBoard(self,1)

def board3(self):
    changeBoard(self,3)


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
