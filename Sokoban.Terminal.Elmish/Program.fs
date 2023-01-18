open Terminal.Gui.Elmish
open Terminal.Gui
open System

type Model = {
    History : string
    Board : string 
}

type Msg =
    | Up 
    | Down 
    | Left 
    | Right 
    | Undo 
let boardNr = 3
let init () : Model * Cmd<Msg> =
    let model = {
        History = ""
        Board = sokoban.game.startNewBoard(boardNr)
    }
    model, Cmd.none


let update (msg:Msg) (model:Model) =
    let move = match msg with
                     | Right -> 'r'
                     | Left -> 'l'
                     | Up -> 'u'
                     | Down -> 'd'
                     | Undo -> 'b'
    let (board,history) = sokoban.game.attemptMove(boardNr,model.History, move)
    {model with History = history; Board = board}, Cmd.none

let view (model:Model) (dispatch:Msg->unit) =
    View.page [
        page.menuBar [
            menubar.menus [
                menu.menuBarItem [
                    menu.prop.title "Menu 1"
                    menu.prop.children [
                        menu.submenuItem [
                            menu.prop.title "Sub Menu 1"
                            menu.prop.children [
                                menu.menuItem ("Sub Item 1", (fun () -> System.Diagnostics.Debug.WriteLine($"Sub menu 1 triggered")))
                                menu.menuItem [
                                    menu.prop.title "Sub Item 2"
                                    menu.item.action (fun () -> System.Diagnostics.Debug.WriteLine($"Sub menu 2 triggered"))
                                    menu.item.itemstyle.check
                                    menu.item.isChecked true
                                ]
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
                prop.textAlignment.centered
                prop.color (Color.BrightYellow, Color.Green)
            ] 
            View.label [
                prop.position.x.center
                prop.position.y.at 1
                prop.textAlignment.centered
                prop.color (Color.BrightYellow, Color.Green)
                label.text model.Board 
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 5
                label.text "Up"
                button.onClick (fun () -> dispatch Up)
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 7
                label.text "Down"
                button.onClick (fun () -> dispatch Down)
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.x.center
                prop.position.y.at 9
                label.text "Left"
                button.onClick (fun () -> dispatch Left)
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 11
                label.text "Right"
                button.onClick (fun () -> dispatch Right)
            ] 

            View.button [
                prop.position.x.at(0)
                prop.position.y.at 13
                label.text "Undo"
                button.onClick (fun () -> dispatch Undo)
            ]
        ]
    ]

Program.mkProgram init update view  
    |> Program.run

