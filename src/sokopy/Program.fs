// For more information see https://aka.ms/fsharp-console-apps
open sokoban
printfn "Game 1"
let finalState,_= game.attemptMove(1,"urduullddrd",'l') 
printf "%A" finalState 


printfn ""
printfn "Game 3"
let finalState3,_= game.attemptMove(3,"urduullddrd",'l') 
printf "%A" finalState3 
