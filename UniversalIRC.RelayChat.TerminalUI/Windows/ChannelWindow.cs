using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI.Windows
{
    internal class ChannelWindow : Window
    {
        private ListView channelLst;
        private Button joinBtn;

        public Action<string> Join;

        public ChannelWindow()
            : base("Network Channels")
        {
            AddLayoutView();
            AddLayoutAction();
        }

        public ChannelWindow(Rect frame)
            : base(frame, "Network Channels")
        {
            AddLayoutView();
            AddLayoutAction();
        }

        protected virtual void AddLayoutView()
        {
            var searchLbl = new Label(2, 1, "Search: ");
            var searchTxt = new TextField(15, 1, 30, "");
            var searchBtn = new Button(50, 1, "Filter");
            var divider = new Label(2, 3, "-------------------------------------------");

            channelLst = new ListView(new Rect(2, 4, divider.Frame.Width, 20), new List<string> { "#kaas", "#worst", "#ham", "#ei" });

            joinBtn = new Button("Join") { X = 2, Y = 24 };

            Add(searchLbl, searchTxt, searchBtn, divider, channelLst, joinBtn);
        }

        protected virtual void AddLayoutAction()
        {
            joinBtn.Clicked = () =>
            {
                Join?.Invoke("#channel");
            };
        }
    }
}
