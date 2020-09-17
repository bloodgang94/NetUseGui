using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using NetUseGui.Model;

namespace NetUseGui.ViewModel
{
    public class NetUseVm : VmDataDesigner
    {
        private readonly NetUseModel _model;
        public NetUseVm()
        {
            _model = new NetUseModel();
        }
        
        public override async Task OnSearch(object value = null)
        {
            await _model.SearchNetworkAsync();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                NetworkCollection.Clear();
                foreach (NetworkInterfaces net in _model.NetworkCollection)
                    NetworkCollection.Add(net);
            }));
        }

        public override async Task OnConnectAsync(object value = null)
        {
            NetworkInterfaces networkInterfaces = value as NetworkInterfaces;
            if (networkInterfaces == null) MessageBox.Show("Не выбран интерфейс для подключения");
            else await networkInterfaces.Plug();
        }
    }
}
