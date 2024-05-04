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
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace GrabMarket.View_Models
{
    internal class ManagerViewModel:ViewModelBase
    {
        private ObservableCollection<Product> managerProducts;
        public ObservableCollection<Product> ManagerProducts
        {
            get { return managerProducts; }
            set {Set<ObservableCollection<Product>>(ref  managerProducts, value); }
        }

        private Guid newCode;
        public Guid NewCode
        {
            get { return newCode; } 
            set { Set(ref newCode, value);}
        }

        private Product currentProduct;
        public Product CurrentProduct
        {
            get { return currentProduct; }
            set { Set(ref currentProduct, value); }
        }

        private INavigatable navigation { get; set; }

        public ManagerViewModel(INavigatable navigation, DataStore storage)
        {
            this.navigation = navigation;
            ManagerProducts = storage.Products;
            CurrentProduct = new Product();
        }

        private RelayCommand toMenu;
        public RelayCommand ToMenu
        {
            get
            {
                return toMenu ?? new RelayCommand(() => { navigation.Navigate("Menu"); });
            }
        }

        private RelayCommand generateCode;
        public RelayCommand GenerateCode
        {
            get { return generateCode ?? new RelayCommand(() => { NewCode = Guid.NewGuid(); CurrentProduct.Code = NewCode; }); }
        }

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? new RelayCommand(() =>
            {
                var isExist = ManagerProducts.FirstOrDefault(x => { if (x.Name == CurrentProduct.Name && x.Type == CurrentProduct.Type) return true; else return false; });
                if (isExist == null)
                {
                    if (CurrentProduct.ImageUrl == null) CurrentProduct.ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/022/059/000/small_2x/no-image-available-icon-vector.jpg";
                    try
                    {
                        if (CurrentProduct.Unit == null || CurrentProduct.Type == null || CurrentProduct.Code == Guid.Empty || CurrentProduct.Price == null)
                        {
                            throw new Exception("Elements must not be empty");
                        }
                        ManagerProducts.Add(currentProduct);
                        NewCode = Guid.Empty;
                        CurrentProduct = new Product();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Уже создан");
                }
            });}
        }

        private RelayCommand<Product> delete;
        public RelayCommand<Product> Delete
        {
            get
            {
                return delete ?? new RelayCommand<Product>(
                (param) =>
                {
                    ManagerProducts.Remove(param);
                });
            }
        }

        private RelayCommand saveData;
        public RelayCommand SaveData
        {
            get
            {
                return saveData ?? new RelayCommand(
                    () =>
                    {
                        using(FileStream fs=new FileStream("Products.json", FileMode.Open))
                        {
                            JsonSerializer.Serialize<ObservableCollection<Product>>(fs, managerProducts);
                            MessageBox.Show("Data save", "DATA WAS SAVED!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
            }
        }
    }
}
