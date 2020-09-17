using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NetUseGui.Model;

namespace NetUseGui.ViewModel
{
   public class VmDataDesigner:OnPropertyChangedClass
    {
   //     private ObservableCollection<NetworkInterfaces> _networkCollection;
        private NetworkInterfaces _selectedInterface;
        private ICommand _connectComm;
        private string _statusMessage;
        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        // Свойство ТОЛЬКО для ЧТЕНИЯ !!!
        public ObservableCollection<NetworkInterfaces> NetworkCollection  {get;}
            = new ObservableCollection<NetworkInterfaces>();
        public NetworkInterfaces SelectedInterface { get => _selectedInterface; set => SetProperty(ref _selectedInterface, value); }
        public virtual async Task OnSearch(object value = null)
        {
            await Task.FromResult(false);
        }
        public virtual async Task OnConnectAsync(object value = null)
        {
            await Task.FromResult(false);
        }
        public ICommand SearchComm => _connectComm ?? (_connectComm = new AsyncRelayCommand(OnConnectAsync, (ex) => StatusMessage = ex.Message));
    }
}
