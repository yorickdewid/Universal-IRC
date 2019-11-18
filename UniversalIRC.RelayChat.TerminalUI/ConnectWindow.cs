using System;
using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class ConnectWindow : Window
    {
        private TextField networkTxt;
        private TextField nicknameTxt;
        private Button connectBtn;

        public event EventHandler OnConnect;
        public Action<string, string> Connect;

        public ConnectWindow()
            : base("Connect IRC Network")
        {
            AddLayoutView();
            AddLayoutAction();
        }

        public ConnectWindow(Rect frame)
            : base(frame, "Connect IRC Network")
        {
            AddLayoutView();
            AddLayoutAction();
        }

        protected virtual void AddLayoutView()
        {
            var networkLbl = new Label("Network: ")
            {
                X = 1,
                Y = 2
            };

            var nicknameLbl = new Label("Nickname: ")
            {
                X = Pos.Left(networkLbl),
                Y = Pos.Top(networkLbl) + 1
            };

            networkTxt = new TextField(string.Empty)
            {
                X = Pos.Right(networkLbl) + 1,
                Y = Pos.Top(networkLbl),
                Width = 40
            };

            nicknameTxt = new TextField(string.Empty)
            {
                X = Pos.Left(networkTxt),
                Y = Pos.Top(nicknameLbl),
                Width = Dim.Width(networkTxt)
            };

            connectBtn = new Button("Connect")
            {
                X = Pos.Left(networkLbl),
                Y = Pos.Top(networkLbl) + 3,
            };

            Add(networkLbl, nicknameLbl, networkTxt, nicknameTxt, connectBtn);
        }

        protected virtual void AddLayoutAction()
        {
            connectBtn.Clicked = () =>
            {
                if (string.IsNullOrEmpty(networkTxt.Text.ToString()))
                {
                    MessageBox.ErrorQuery(30, 6, "Error", "Network cannot be empty", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(nicknameTxt.Text.ToString()))
                {
                    MessageBox.ErrorQuery(30, 6, "Error", "Nickname cannot be empty", "Ok");
                    return;
                }

                Connect?.Invoke(networkTxt.Text.ToString(), nicknameTxt.Text.ToString());
                OnConnect?.Invoke(this, EventArgs.Empty);
            };
        }
    }
}
