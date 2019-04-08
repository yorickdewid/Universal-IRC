using System;

using UniversalIRC.Core.Models;
using UniversalIRC.Helpers;

namespace UniversalIRC.ViewModels
{
    public class ConnectDialogViewModel : Observable
    {
        private string _host;

        public string Host
        {
            get => _host;
            set
            {
                Set(ref _host, value);
                OnPropertyChanged(nameof(CanConnect));
            }
        }

        private short _port = 6667;

        public short Port
        {
            get => _port;
            set
            {
                Set(ref _port, value);
                OnPropertyChanged(nameof(CanConnect));
            }
        }

        private bool _useSsl = true;

        public bool UseSsl
        {
            get => _useSsl;
            set => Set(ref _useSsl, value);
        }

        private string _nickname;

        public string Nickname
        {
            get => _nickname;
            set
            {
                Set(ref _nickname, value);
                OnPropertyChanged(nameof(CanConnect));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        private bool _rememberCredentials = true;

        public bool RememberCredentials
        {
            get => _rememberCredentials;
            set => Set(ref _rememberCredentials, value);
        }

        public bool CanConnect
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Host)
                       && !string.IsNullOrWhiteSpace(Nickname)
                       && Port != 0;
            }
        }

        public Network NetworkModel
        {
            get
            {
                return new Network(name: Host,
                    host: Host.Trim(),
                    account: new ChatUserAccount(Nickname.Trim()));
            }
        }
    }
}
