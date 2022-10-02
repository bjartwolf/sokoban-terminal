// For more information see https://aka.ms/fsharp-console-apps
open sokoban
open Fable.Python.TkInter

printfn "Game 1"
let finalState,_= game.attemptMove(1,"urduullddrd",'l') 
printf "%A" finalState 


printfn ""
printfn "Game 3"
let finalState3,_= game.attemptMove(3,"urduullddrd",'l') 
printf "%A" finalState3 

let root = Tk()
root.title ("Fable Python Rocks on Tkinter!")
root.mainloop ()
