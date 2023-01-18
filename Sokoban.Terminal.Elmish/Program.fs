open Terminal.Gui.Elmish
open Terminal.Gui

type Model = {
    History : string
    Board : string 
    BoardNr : int
}

type Move = Up | Down | Left | Right | Undo
type Msg =
    | Move of Move 
    | Level of int 

let boardNr = 3
let init () : Model * Cmd<Msg> =
    let model = {
        History = ""
        Board = sokoban.game.startNewBoard(boardNr)
        BoardNr = 3
    }
    model, Cmd.none

let update (msg:Msg) (model:Model) =
    let moveDir (m: char) = 
        sokoban.game.attemptMove(model.BoardNr,model.History, m)
    let (board, history)= match msg with
                     | Move Right -> moveDir 'r' 
                     | Move Left -> moveDir 'l' 
                     | Move Up -> moveDir 'u' 
                     | Move Down -> moveDir 'd' 
                     | Move Undo -> moveDir 'b' 
                     | Level i -> sokoban.game.startNewBoard(i), ""
    match msg with
        | Move _ -> {model with History = history; Board = board}, Cmd.none
        | Level i -> {model with History = history; Board = board; BoardNr = i}, Cmd.none

let view (model:Model) (dispatch:Msg->unit) =
    View.page [
        page.menuBar [
            menubar.menus [
                menu.menuBarItem [
                    menu.prop.title "Level"
                    menu.prop.children [
                        menu.submenuItem [
                            menu.prop.title "Normal levels"
                            menu.prop.children [
                                menu.menuItem ("Level 0", (fun () -> dispatch (Level 0)))
                                menu.menuItem ("Level 1", (fun () -> dispatch (Level 1)))
                                menu.menuItem ("Level 3", (fun () -> dispatch (Level 3)))
                            ]
                        ]
                    ]
                ]
            ]
        ]
        prop.children [
            View.label [
                prop.position.x.center
                prop.position.y.at 1
                prop.textAlignment.left
                prop.color (Color.BrightYellow, Color.Green)
                label.text model.Board 
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 5
                button.hotKey Key.CursorUp
                label.text "Up"
                button.onClick (fun () -> dispatch (Move Up))
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 7
                button.hotKey Key.CursorDown
                label.text "Down"
                button.onClick (fun () -> dispatch (Move Down))
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.x.center
                prop.position.y.at 9
                button.hotKey Key.CursorLeft
                label.text "Left"
                button.onClick (fun () -> dispatch (Move Left))
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 11
                button.hotKey Key.CursorRight
                label.text "Right"
                button.onClick (fun () -> dispatch (Move Right))
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 13
                label.text "Undo"
                button.onClick (fun () -> dispatch (Move Undo))
            ]
        ]
    ]

Program.mkProgram init update view  
    |> Program.run

