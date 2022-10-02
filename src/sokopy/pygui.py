from tkinter import * 
from game.library import *

print("game 1 go")
gameNr = 3
board = init(gameNr)
print(board)
global history
history = ""

tk = Tk()
tk.geometry("750x280")

canvas= Canvas(tk, width= 1000, height= 750, bg="blue")

tekst = canvas.create_text(300, 100, text=board, fill="white", font=('TkFixedFont'))
canvas.pack()

def left(self):
     newboard, newhistory = attempt_move(gameNr, history, "l")
     board = newboard
     canvas.itemconfig(tekst,text = board)
     tk.update()

tk.bind('l',left)

tk.mainloop ()
#while True:
#    cmd = input("Skrive inn l,r,u,d,b")
#    board, history = attempt_move(gameNr, history, cmd)
#    print(board)
#    print(history)
