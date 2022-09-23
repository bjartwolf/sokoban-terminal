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
    var n = MessageBox.Query(50, 7, "Quit game", "You're trying to say you like DOS\r\nbetter than me, right?", "Yes", "No");
    return n == 0;
}



var board = sokoban.game.init();

var gameWindow = new Label(3, 3, board);

win.Add( gameWindow);
win.KeyDown += KeyPress;

void KeyPress(View.KeyEventEventArgs obj)
{
    var keypress = obj.KeyEvent.Key;
    if (keypress == Key.h || keypress == Key.j || keypress == Key.k || keypress == Key.l)
    {
        var character = keypress.ToString().ToCharArray()[0];
        board = sokoban.game.makeMove(board, character);
        gameWindow.Text = board;

    }
}

Application.Run();
Application.Shutdown();

