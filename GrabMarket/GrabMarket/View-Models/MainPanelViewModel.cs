using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabMarket.View_Models
{
    internal class MainPanelViewModel:ViewModelBase
    {
        private ViewModelBase currentPanel;

        public ViewModelBase CurrentPanel
        {
            get { return currentPanel; }
            set { Set(ref currentPanel, value); }
        }

        public MainPanelViewModel()
        {
            Messenger.Default.Register<ViewModelBase>(this, (panelViewModel) => { CurrentPanel = panelViewModel; });
        }
    }
}
