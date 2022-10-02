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
let frame = Frame(root,300,300,"pink") 
frame.pack()
let label = Label(frame, finalState3,"red", "blue")
label.place(0,0)
root.title ("Fable Python Rocks on Tkinter!")
root.mainloop ()
