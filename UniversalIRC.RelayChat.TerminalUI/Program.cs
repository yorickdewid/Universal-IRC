using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Init();

            var top = Application.Top;
            var sc = top.ColorScheme;

            // Creates the top-level window to show
            var win = new App
            {
                X = 0,
                Y = 1, // Leave one row for the toplevel menu

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(win);

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_Help", new MenuItem [] {
                    new MenuItem ("_About", "", () => MessageBox.Query(50,7, "About", "Univeral IRC Chat Terminal UI", "Ok")),
                }),
            });
            top.Add(menu);

            Application.Run();
        }
    }
}
