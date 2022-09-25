using Terminal.Gui;

Application.Init();
var top = Application.Top;

// Creates the top-level window to show
var win = new Window("Sokoban - the retro game in a retro looking terminal")
{
    X = 0,
    Y = 1, // Leave one row for the toplevel menu

    // By using Dim.Fill(), it will automatically resize without manual intervention
    Width = Dim.Fill(),
    Height = Dim.Fill()
};

top.Add(win);

var menu = new MenuBar(new[] {
    new MenuBarItem ("_File", new MenuItem [] {
        new("_Quit", "", () => { if (Quit ()) top.Running = false; })
    }),
});
top.Add(menu);

static bool Quit()
{
    var n = MessageBox.Query(50, 7, "Quit game", $"You're trying to say you like DOS{Environment.NewLine}better than me, right?", "Yes", "No");
    return n == 0;
}

var boardNr = 3;
var board = sokoban.game.startNewBoard(boardNr);

var history = string.Empty; 
var help = new Label(3, 3, "Press h,j,k,l to move, r to reset, 0,1 or 3 to change board, u to undo");
var historyWindow = new Label(3, 4, history);
var gameWindow = new Label(10, 10, board);

win.Add( help);
win.Add( historyWindow);
win.Add( gameWindow);
win.KeyDown += KeyPress;

void KeyPress(View.KeyEventEventArgs obj)
{
    var keypress = obj.KeyEvent.Key;
    if (keypress is Key.h or Key.j or Key.k or Key.l or Key.r or Key.u or Key.D0 or Key.D1 or Key.D3)
    {
        if (keypress is Key.r)
        {
            history = "";
            board = sokoban.game.startNewBoard(boardNr);
        } 
        else if (keypress is Key.D0 or Key.D1 or Key.D3)
        {
            history = "";
            boardNr = keypress switch
            {
                Key.D0 => 0,
                Key.D1 => 1,
                Key.D3 => 3
            };
            board = sokoban.game.startNewBoard(boardNr);
        }
        else
        {
            var move = keypress switch
            {
                Key.k => "u",
                Key.j => "d",
                Key.l => "r",
                Key.h => "l",
                Key.u => "b",
            _ => throw new ArgumentOutOfRangeException($"No such key is configured...")
            };
            (board,history) = sokoban.game.attemptMove(boardNr,history, move.First());
        }
        gameWindow.Text = board;
        historyWindow.Text = history;
    }
}

Application.Run();
Application.Shutdown();
