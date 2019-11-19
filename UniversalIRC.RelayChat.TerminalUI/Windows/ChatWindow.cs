using System;
using System.Collections.Generic;
using Terminal.Gui;
using UniversalIRC.RelayChat.TerminalUI.Components;

namespace UniversalIRC.RelayChat.TerminalUI.Windows
{
    internal class ChatWindow : Window
    {
        private const int maxScrollback = 100;
        private readonly List<ChatLine> backlog = new List<ChatLine>();
        private ListView chatLst;
        private ExtendedTextField inputTxt;

        public class ChatLine
        {
            public DateTime Timestamp { get; } = DateTime.Now;
            public string Name { get; set; }
            public string Message { get; set; }

            public override string ToString()
            {
                return $"{Timestamp.ToLongTimeString()} <{Name}> {Message}";
            }
        }

        public ChatWindow(string title)
            : base($"Chat [ {title} ] #Crypto #Ubuntu")
        {
            AddLayoutView();
            AddLayoutAction();
        }

        public ChatWindow(Rect frame, string title)
            : base(frame, $"Chat {title}")
        {
            AddLayoutView();
            AddLayoutAction();
        }

        public void AddChatLine(ChatLine chat)
        {
            // TODO: pop items from backlog front

            backlog.Add(chat);
        }

        protected virtual void AddLayoutView()
        {
            chatLst = new ListView(backlog)
            {
                Y = 1,
                CanFocus = false,
                AllowsMarking = false,
            };

            inputTxt = new ExtendedTextField(string.Empty)
            {
                X = 1,
                Y = 25,
                Width = Dim.Fill()
            };

            Add(chatLst, inputTxt);
        }

        protected virtual void AddLayoutAction()
        {
            inputTxt.OnSubmit += (s, o) =>
            {
                AddChatLine(new ChatLine { Name = "Me", Message = inputTxt.Text.ToString() });
                inputTxt.Text = string.Empty;
                chatLst.Redraw(new Rect(1, 1, Frame.Width - 10, 20));
            };
        }
    }
}
