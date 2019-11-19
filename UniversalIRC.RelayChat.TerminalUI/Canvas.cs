using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class Canvas : View
    {
        private View currentView;

        public Canvas()
        {
            currentView = new ConnectWindow();

            AddLayoutView();
            AddLayoutAction();
        }

        public Canvas(Rect frame)
            : base(frame)
        {
            currentView = new ConnectWindow();

            AddLayoutView();
            AddLayoutAction();
        }

        protected virtual void AddLayoutView()
        {
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", string.Empty, SignalQuit),
                }),
                new MenuBarItem ("_Channel", new MenuItem [] {
                    new MenuItem ("_Channel list", string.Empty, null),
                    new MenuItem ("_Join", string.Empty, null),
                    new MenuItem ("_Part", string.Empty, null),
                }),
                new MenuBarItem ("_Help", new MenuItem [] {
                    new MenuItem ("_About", string.Empty, () => MessageBox.Query(50, 7, "About", "Univeral IRC Chat Terminal UI", "Ok")),
                }),
            });

            Add(menu);

            currentView.X = Pos.X(menu);
            currentView.Y = Pos.Bottom(menu);
            currentView.Width = Dim.Fill();
            currentView.Height = Dim.Fill();

            Add(currentView);
        }

        protected virtual void AddLayoutAction()
        {
            //
        }

        private void SignalQuit()
        {
            if (MessageBox.Query(80, 7, "Quit", "Are you sure you want to quit and part all active conversations?", "Ok", "Cancel") == 0)
            {
                Application.RequestStop();
            }
        }
    }
}
