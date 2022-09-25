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

var menu = new MenuBar(new MenuBarItem[] {
    new MenuBarItem ("_File", new MenuItem [] {
        new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
    }),
});
top.Add(menu);

static bool Quit()
{
    var n = MessageBox.Query(50, 7, "Quit game", $"You're trying to say you like DOS{Environment.NewLine}better than me, right?", "Yes", "No");
    return n == 0;
}



var board = sokoban.game.init(0);

var help = new Label(3, 3, "Press h,j,k,l to move, r to reset, 0-5 to change board.");
var gameWindow = new Label(10, 10, board);

win.Add( help);
win.Add( gameWindow);
win.KeyDown += KeyPress;

void KeyPress(View.KeyEventEventArgs obj)
{
    var keypress = obj.KeyEvent.Key;
    if (keypress is Key.h or Key.j or Key.k or Key.l or Key.r)
    {
        var character = keypress.ToString().ToCharArray()[0];
        var move = keypress switch
        {
            Key.k => "u",
            Key.j => "d",
            Key.l => "r",
            Key.h => "l",
            Key.r => "reset",
            _ => throw new ArgumentOutOfRangeException($"No such key is configured...")
        };
        if (keypress is Key.r)
        {
            board = sokoban.game.init(3);
        }
        else
        {
            board = sokoban.game.makeMove(board, move.First());
        }
        gameWindow.Text = board;

    }
}

Application.Run();
Application.Shutdown();

