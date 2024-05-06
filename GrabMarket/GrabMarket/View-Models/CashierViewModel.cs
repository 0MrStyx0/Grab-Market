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

        private ObservableCollection<Product> visibleProducts;
        public ObservableCollection<Product> VisibleProducts
        {
            get { return visibleProducts; }
            set { Set<ObservableCollection<Product>>(ref visibleProducts, value); }
        }

        private ObservableCollection<BasketElementViewModel> basket;
        public ObservableCollection<BasketElementViewModel> Basket
        { get { return basket; } 
          set { Set<ObservableCollection<BasketElementViewModel>>(ref basket, value); }
        }

        private Check check;
        private List<Check> checks;

        private INavigatable navigation { get; set; }
        public CashierViewModel(INavigatable navigation, DataStore storage, List<Check> Checks)
        {
            this.navigation = navigation;
            CashierProducts = storage.Products;
            VisibleProducts = CashierProducts;
            Basket = new ObservableCollection<BasketElementViewModel>();
            check = new Check();
            checks = Checks;
        }

        private string totalAmount;
        public string TotalAmount
        { get { return totalAmount; }
          set { Set(ref totalAmount, value); }
        }

        private RelayCommand<string> showFilter;
        public RelayCommand<string> ShowFilter
        {
            get
            {
                return showFilter ?? new RelayCommand<string>(
                    (param) =>
                    {
                        if(param == "All")
                        {
                            VisibleProducts = CashierProducts;
                        }
                        else
                        {
                            var filtered = CashierProducts.Where(x => x.Type == param).ToList();
                            VisibleProducts = new ObservableCollection<Product>(filtered);
                        }
                    });
            }
        }

        private RelayCommand toMenu;
        public RelayCommand ToMenu
        {
            get
            {
                return toMenu ?? new RelayCommand(() => { navigation.Navigate("Menu"); TotalAmount = ""; Basket = new ObservableCollection<BasketElementViewModel>(); });
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
                        var result = Basket.FirstOrDefault(x => x.Code == selectedItem.Code);
                        if(result == null)
                        {
                            Basket.Add(new BasketElementViewModel
                            {
                                Code = selectedItem.Code,
                                Price = selectedItem.Price,
                                Count = 1,
                                Name = selectedItem.Name
                            });
                        }
                        else
                        {
                            MessageBox.Show("Product in basket!","Info",MessageBoxButton.OK, MessageBoxImage.Information);
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

        private RelayCommand<BasketElementViewModel> plus;
        public RelayCommand<BasketElementViewModel> Plus
        {
            get
            {
                return plus ?? new RelayCommand<BasketElementViewModel>(
                    (param) =>
                    {
                        param.Count++;
                        var result = CashierProducts.FirstOrDefault(x => x.Code == param.Code);
                        param.Price += result.Price;
                        TotalAmount = Math.Round((double)Basket.Sum(x => x.Price), 2).ToString();
                    });
            }
        }

        private RelayCommand<BasketElementViewModel> minus;
        public RelayCommand<BasketElementViewModel> Minus
        {
            get
            {
                return minus ?? new RelayCommand<BasketElementViewModel>(
                    (param) =>
                    {
                        param.Count--;
                        if(param.Count == 0)
                        {
                            Basket.Remove(param);
                        }
                        else
                        {
                            var result = CashierProducts.FirstOrDefault(x => x.Code == param.Code);
                            param.Price -= result.Price;
                        }
                        TotalAmount = Math.Round((double)Basket.Sum(x => x.Price), 2).ToString();
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
                        try
                        {
                            if (Basket.Count == 0) throw new Exception("Empty Basket!");
                            check.Basket.Add(Basket);
                            check.TotalAmount = TotalAmount;
                            check.Date = DateTime.Now.ToString();
                            checks.Add(check);

                            string Check = "        GRAB MARKET\n" +
                                      "-------------------------------\n" +
                                      "Name:\t\tPrice:\n\n";
                            for(int i = 0; i < Basket.Count; i++)
                            {
                                Check += Basket[i].Name + "\t       ";
                                Check += Basket[i].Count + "\t    ";
                                Check += Math.Round((double)Basket[i].Price,2) + "\n\n";
                            }
                            Check += "-------------------------------\n" +
                                     $"TOTAL AMOUNT:      {check.TotalAmount}\n\n" +
                                     $"{check.Date}";
                            using (FileStream fs = new FileStream("Checks.json", FileMode.Create))
                            {
                                JsonSerializer.Serialize<List<Check>>(fs, checks);
                                MessageBox.Show(Check, "Check", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            Basket = new ObservableCollection<BasketElementViewModel>();
                            check = new Check();
                            TotalAmount = "";
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
            }
        }
    }
}
