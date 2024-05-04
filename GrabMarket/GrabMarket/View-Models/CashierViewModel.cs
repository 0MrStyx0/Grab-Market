using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GrabMarket.Models;
using GrabMarket.Resources;
using GrabMarket.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace GrabMarket.View_Models
{
    internal class CashierViewModel:ViewModelBase
    {
        private ObservableCollection<Product> cashierProducts;
        public ObservableCollection<Product> CashierProducts
        {
            get { return cashierProducts; }
            set { Set<ObservableCollection<Product>>(ref cashierProducts, value); }
        }

        private ObservableCollection<BasketElementViewModel> basket;
        public ObservableCollection<BasketElementViewModel> Basket
        { get { return basket; } 
          set { Set<ObservableCollection<BasketElementViewModel>>(ref basket, value); }
        }

        private Check check;
        private List<Check> checks;

        private INavigatable navigation { get; set; }
        public CashierViewModel(INavigatable navigation, DataStore storage)
        {
            this.navigation = navigation;
            CashierProducts = storage.Products;
            Basket = new ObservableCollection<BasketElementViewModel>();
            check = new Check();
            checks = new List<Check>();
        }

        private string totalAmount;
        public string TotalAmount
        { get { return totalAmount; }
          set { Set(ref totalAmount, value); }
        }

        private RelayCommand toMenu;
        public RelayCommand ToMenu
        {
            get
            {
                return toMenu ?? new RelayCommand(() => { navigation.Navigate("Menu"); });
            }
        }

        private RelayCommand<Product> add;
        public RelayCommand<Product> Add
        {
            get
            {
                return add ?? new RelayCommand<Product>(
                    (param) =>
                    {
                        var selectedItem= param as Product;
                        var result=Basket.FirstOrDefault(x=>x.Code == selectedItem.Code);
                        if(result != null)
                        {
                            result.Count++;
                            result.Price = result.Price + selectedItem.Price;
                        }
                        else
                        {
                            Basket.Add(new BasketElementViewModel
                            {
                                Code = selectedItem.Code,
                                Price = selectedItem.Price,
                                Count = 1,
                                Name = selectedItem.Name
                            });
                        }
                        TotalAmount = Basket.Sum(x => x.Price).ToString();
                    });
            }
        }

        private RelayCommand<BasketElementViewModel> delete;
        public RelayCommand<BasketElementViewModel> Delete
        {
            get
            {
                return delete ?? new RelayCommand<BasketElementViewModel>(
                    (param) =>
                    {
                        Basket.Remove(param);
                        TotalAmount = Basket.Sum(x => x.Price).ToString();
                    });
            }
        }

        private RelayCommand punchCheck;
        public RelayCommand PunchCheck
        {
            get
            {
                return punchCheck ?? new RelayCommand(
                    () =>
                    {
                        check.Basket.Add(Basket);
                        check.TotalAmount = TotalAmount;
                        checks.Add(check);
                        using(FileStream fs=new FileStream("Checks.json", FileMode.OpenOrCreate))
                        {
                            JsonSerializer.Serialize<List<Check>>(fs, checks);
                            MessageBox.Show("Punching", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        Basket = new ObservableCollection<BasketElementViewModel>();
                        check = new Check();
                        TotalAmount = "";
                    });
            }
        }
    }
}
