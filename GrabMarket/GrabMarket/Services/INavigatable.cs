using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabMarket.Services
{
    internal interface INavigatable
    {
        public void Register(string panelName, ViewModelBase panelViewModel);
        public void Navigate(string panelName);
    }
}
