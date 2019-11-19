using System;
using System.Collections.Generic;
using Terminal.Gui;
using UniversalIRC.RelayChat.TerminalUI.Windows;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class Canvas : View
    {
        private readonly Stack<View> _viewStack;
        private readonly IRouter _router;
        private readonly IList<Lazy<Window>> _routeMapper;

        public Canvas()
            : this(new Rect(), null)
        {
        }

        public Canvas(IRouter router)
            : this(new Rect(), router)
        {
        }

        public Canvas(Rect frame, IRouter router)
            : base(frame)
        {
            _viewStack.Push(new ConnectWindow());

            //routeMapper = new List<Lazy<Window>>
            //{
            //    new Lazy<ConnectWindow>(),
            //    new Lazy<ChatWindow>(),
            //    new Lazy<ChannelWindow>(),
            //};

            _router = router ?? new Router();
            _router.OnRoute += HandleRoute;

            AddLayoutView();
            AddLayoutAction();
        }

        private void HandleRoute(object sender, System.EventArgs e)
        {
            //
        }

        protected virtual void AddLayoutView()
        {
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", string.Empty, SignalQuit),
                }),
                new MenuBarItem ("_Channel", new MenuItem [] {
                    new MenuItem ("_Channel list", string.Empty, ()=> _router.Push("channel_list")),
                    new MenuItem ("_Join", string.Empty, null),
                    new MenuItem ("_Part", string.Empty, null),
                }),
                new MenuBarItem ("_Help", new MenuItem [] {
                    new MenuItem ("_About", string.Empty, () => MessageBox.Query(50, 7, "About", "Univeral IRC Chat Terminal UI", "Ok")),
                }),
            });

            Add(menu);

            _viewStack.Peek().X = Pos.X(menu);
            _viewStack.Peek().Y = Pos.Bottom(menu);
            _viewStack.Peek().Width = Dim.Fill();
            _viewStack.Peek().Height = Dim.Fill();

            Add(_viewStack.Peek());
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
