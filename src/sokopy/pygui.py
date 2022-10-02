from tkinter import * 
from game.library import *

print("game 1 go")
gameNr = 3
start = init(gameNr)
print(start)
history = ""
while True:
    cmd = input("Skrive inn l,r,u,d,b")
    board, history = attempt_move(gameNr, history, cmd)
    print(board)
    print(history)
