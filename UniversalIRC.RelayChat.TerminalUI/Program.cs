using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Init();

            SetupCanvas(Application.Top);
#if _x
            connectWindow.Connect = (network, nick) =>
            {
                //MessageBox.Query(60, 7, string.Empty, $"Connecting {network}", "Ok");

                chatWindow.Frame = currentWindow.Frame;
                top.Remove(currentWindow);
                currentWindow = chatWindow;
                top.Add(currentWindow);
                currentWindow.Redraw(currentWindow.Frame);
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
#endif
            Application.Run();
        }

        private static void SetupCanvas(View view)
        {
            var canvas = new Canvas
            {
                X = 0,
                Y = 0, // Pos.Top(view) + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            view.Add(canvas);
        }
    }
}
