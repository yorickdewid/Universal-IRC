using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Init();

            SetupTopView(Application.Top);

            var top = Application.Top;

            Window currentWindow = new Window(string.Empty);

            var connectWindow = new ConnectWindow
            {
                X = 0,
                Y = 1,

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            var chatWindow = new ChatWindow("#channel") // connectWindow.Frame, 
            {
                X = 0,
                Y = 1,

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            connectWindow.Connect = (network, nick) =>
            {
                //MessageBox.Query(60, 7, string.Empty, $"Connecting {network}", "Ok");

                chatWindow.Bounds = currentWindow.Bounds;
                top.Remove(currentWindow);
                currentWindow = chatWindow;
                top.Add(currentWindow);
            };

            #region FUTURE
            //buttonConnect.Clicked = () =>
            //{
            //    try
            //    {
            //        var erd = new System.Uri("irc://" + networkInput.Text.ToString());
            //        MessageBox.Query(60, 5, string.Empty, $"Connecting to {erd}");
            //    }
            //    catch (System.UriFormatException)
            //    {
            //        MessageBox.ErrorQuery(60, 7, "Error", "Invalid network name", "Ok");
            //    }
            //};
            #endregion

            currentWindow = connectWindow;
            top.Add(currentWindow);

            Application.Run();
        }

        private static void SetupTopView(View view)
        {
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", UiSignalQuit),
                }),
                new MenuBarItem ("_Channel", new MenuItem [] {
                    new MenuItem ("_Channel list", "", null),
                    new MenuItem ("_Join", "", null),
                    new MenuItem ("_Part", "", null),
                }),
                new MenuBarItem ("_Help", new MenuItem [] {
                    new MenuItem ("_About", "", () => MessageBox.Query(50, 7, "About", "Univeral IRC Chat Terminal UI", "Ok")),
                }),
            });

            view.Add(menu);
        }

        private static void UiSignalQuit()
        {
            if (MessageBox.Query(80, 7, "Quit", "Are you sure you want to quit and part all active conversations?", "Ok", "Cancel") == 0)
            {
                Application.RequestStop();
            }
        }
    }
}
