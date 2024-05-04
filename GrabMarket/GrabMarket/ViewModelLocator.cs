using GalaSoft.MvvmLight;
using GrabMarket.Resources;
using GrabMarket.Services;
using GrabMarket.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabMarket
{
    internal class ViewModelLocator
    {
        private MainPanelViewModel mainPanelViewModel {  get; set; }
        private MenuViewModel menuViewModel { get; set; }
        private CashierViewModel cashierViewModel {  get; set; }
        private ManagerViewModel managerViewModel { get; set; }
        private INavigatable navigation { get; set; }

        public ViewModelLocator(DataStore storage)
        {
            navigation = new Navigation();
            mainPanelViewModel = new MainPanelViewModel();
            menuViewModel = new MenuViewModel(navigation);
            cashierViewModel = new CashierViewModel(navigation, storage);
            managerViewModel = new ManagerViewModel(navigation,storage);

            navigation.Register("Menu", menuViewModel);
            navigation.Register("Manager", managerViewModel);
            navigation.Register("Cashier", cashierViewModel);

            navigation.Navigate("Menu");
        }

        public ViewModelBase GetMainPanelViewModel()
        {
            return mainPanelViewModel;
        }
    }
}
