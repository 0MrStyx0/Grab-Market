using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GrabMarket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GrabMarket.View_Models
{
    internal class MenuViewModel:ViewModelBase
    {
        private INavigatable navigation { get; set; }
        public MenuViewModel(INavigatable navigation)
        {
            this.navigation = navigation;
        }

        private RelayCommand<string> toThePanel;
        public RelayCommand<string> ToThePanel
        {
            get
            {
                return toThePanel ?? new RelayCommand<string>((param) =>
                {
                    if (param == "Manager") navigation.Navigate("Manager");
                    else if (param == "Cashier") navigation.Navigate("Cashier");
                });
            }
        }
    }
}
