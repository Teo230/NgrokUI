using NgrokUI.Models;
using NgrokUI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NgrokUI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Prop
        private MainWindowModel _mainWindowModel;

        private ObservableCollection<string> _protocols = new ObservableCollection<string>();
        public ObservableCollection<string> Protocols
        {
            get => _protocols;
            set
            {
                if (_protocols != value)
                {
                    _protocols = value;
                    NotifyPropertyChanged(nameof(Protocols));
                }
            }
        }

        private string _protocol;
        public string Protocol
        {
            get => _protocol;
            set
            {
                if (_protocol != value)
                {
                    _protocol = value;
                    NotifyPropertyChanged(nameof(Protocol));
                }
            }
        }

        private int _portNumber;
        public int PortNumber
        {
            get => _portNumber;
            set
            {
                if (_portNumber != value)
                {
                    _portNumber = value;
                    NotifyPropertyChanged(nameof(PortNumber));
                }
            }
        }

        private string _consoleOutput;
        public string ConsoleOutput
        {
            get => _consoleOutput;
            set
            {
                if (_consoleOutput != value)
                {
                    _consoleOutput = value;
                    NotifyPropertyChanged(nameof(ConsoleOutput));
                }
            }
        }
        #endregion

        #region Ctr
        public MainWindowViewModel()
        {
            Initialize();
            _mainWindowModel = new MainWindowModel();
            _mainWindowModel.elaborationCompleted += ModelElaboration;
        }
        #endregion

        #region Methods
        private void Initialize()
        {
            GetProtocols();
        }

        private void GetProtocols()
        {
            Protocols.Add("http");
            Protocols.Add("tcp");
            Protocols.Add("tls");
        }

        private EnumDictionary.Protocols GetProtocol()
        {
            var EProto = new EnumDictionary.Protocols();
            switch (Protocol)
            {
                case "http":
                    EProto = EnumDictionary.Protocols.http;
                    break;
                case "tcp":
                    EProto = EnumDictionary.Protocols.tcp;
                    break;
                case "tls":
                    EProto = EnumDictionary.Protocols.tls;
                    break;
            }

            return EProto;
        }

        private void ModelElaboration(object sender, Utility.EventArgs e)
        {
            if (e.StartTunnelReceived.HasValue)
            {
                if (e.StartTunnelReceived.Value)
                {
                    _mainWindowModel.GetTunnels();
                }
            }

            if (e.TunnelReceived.HasValue)
            {
                if (e.TunnelReceived.Value)
                {
                    ConsoleOutput = _mainWindowModel.TunnelResult.tunnels.FirstOrDefault().public_url;
                }
            }
        }
        #endregion

        #region Commands
        private ICommand _confirm;
        public ICommand Confirm
        {
            get
            {
                if (this._confirm == null)
                {
                    this._confirm = new RelayCommands(obj =>
                    {

                        using (var process = new Process())
                        {
                            //process.StartInfo.FileName = @"cmd.exe";
                            //process.StartInfo.Arguments = @"/c ngrok.exe";
                            //process.StartInfo.UseShellExecute = false;
                            //process.StartInfo.RedirectStandardInput = true;
                            //process.StartInfo.Verb = "runas";
                            //process.StartInfo.WindowStyle

                            //process.Start();
                        }

                        //_mainWindowModel.StartTunnel(GetProtocol(), PortNumber);
                        _mainWindowModel.GetTunnels();
                    }, obj => true);
                }
                return this._confirm;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
