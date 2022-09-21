using Terminal.Gui;

Application.Init();
var top = Application.Top;

// Creates the top-level window to show
var win = new Window("MyApp")
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
    var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
    return n == 0;
}


var game = "####\r\n#  #######\r\n#  ......#\r\n#  ##### #\r\n##   $@# #\r\n #  $$$$ #\r\n ##  $ ###\r\n  #    #\r\n  ######\r\n";
win.Add(
    new Label(3, 18, game)
);

Application.Run();
Application.Shutdown();

