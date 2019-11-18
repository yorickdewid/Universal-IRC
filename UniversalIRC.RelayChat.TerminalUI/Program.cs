using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class Program
    {
        private class TestField : TextField
        {
            public Action Submitted;

            public TestField(int x, int y, int w, string t)
                : base(x, y, w, t)
            {

            }

            public override bool ProcessKey(KeyEvent kb)
            {
                if (kb.Key == Key.ControlJ || kb.KeyValue == 10)
                {
                    Submitted?.Invoke();
                }

                return base.ProcessKey(kb);
            }
        }

        private static void Main(string[] args)
        {
            Application.Init();

            var top = Application.Top;
            var sc = top.ColorScheme;

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", null),
                }),
                new MenuBarItem ("_Channel", new MenuItem [] {
                    new MenuItem ("_Channel list", "", null),
                    new MenuItem ("_Join", "", null),
                    new MenuItem ("_Part", "", null),
                }),
                new MenuBarItem ("_Help", new MenuItem [] {
                    new MenuItem ("_About", "", () => MessageBox.Query(50,7, "About", "Univeral IRC Chat Terminal UI", "Ok")),
                }),
            });
            top.Add(menu);

            // Creates the top-level window to show
            var connectWindow = new ConnectWindow
            {
                X = 0,
                Y = 1,

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
            //top.Add(connectWindow);

            //connectWindow.Connect = (network, nick) =>
            //{
            //    MessageBox.Query(60, 7, string.Empty, $"Hi {nick}", "Ok");

            // Creates the top-level window to show
            var win = new Window("Chat #channel") // connectWindow.Frame, 
            {
                X = 0,
                Y = 1,

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var chatLog = new List<string> {
                "Hi", "Hello", "Kaas is gewoon heel erg lekker", "Something else to say?",
            "Hi", "Hello", "Kaas is gewoon heel erg lekker", "Something else to say?",
            "Hi", "Hello", "Kaas is gewoon heel erg lekker", "Something else to say?",
            };

            var chatLst = new ListView(new Rect(1, 1, top.Frame.Width - 10, 20), chatLog)
            {
                CanFocus = false,
                AllowsMarking = false,
            };

            var inputTxt = new TestField(1, 25, top.Frame.Width - 10, "");
            inputTxt.Submitted = () =>
            {
                MessageBox.Query(50, 7, "About", inputTxt.Text.ToString(), "Ok");
                chatLog.Add(inputTxt.Text.ToString());
                inputTxt.Text = string.Empty;
                chatLst.Redraw(new Rect(1, 1, top.Frame.Width - 10, 20));
            };

            win.Add(chatLst, inputTxt);

            //top.Remove(connectWindow);
            top.Add(win);
            //};

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

            Application.Run();
        }
    }
}
