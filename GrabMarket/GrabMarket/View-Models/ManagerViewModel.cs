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

        private ObservableCollection<Product> visibleProducts;
        public ObservableCollection<Product> VisibleProducts
        {
            get { return visibleProducts; }
            set { Set<ObservableCollection<Product>>(ref visibleProducts, value); }
        }

        private string nameCommand;
        public string NameCommand
        {
            get { return nameCommand; }
            set { Set(ref nameCommand, value); }
        }

        public string EditType { get; set; }

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
            VisibleProducts = ManagerProducts;
            CurrentProduct = new Product();
            NameCommand = "Add";
        }

        private RelayCommand<string> showFilter;
        public RelayCommand<string> ShowFilter
        {
            get
            {
                return showFilter ?? new RelayCommand<string>(
                    (param) =>
                    {
                        if (param == "All")
                        {
                            VisibleProducts = ManagerProducts;
                        }
                        else
                        {
                            var filtered = ManagerProducts.Where(x => x.Type == param).ToList();
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
                return toMenu ?? new RelayCommand(() => { navigation.Navigate("Menu"); NewCode = Guid.Empty; NameCommand = "Add"; CurrentProduct = new Product(); });
            }
        }

        private RelayCommand generateCode;
        public RelayCommand GenerateCode
        {
            get
            { 
                return generateCode ?? new RelayCommand(
                    () =>
                    {
                        if (NameCommand == "Edit")
                        {
                            MessageBox.Show("You can't change the Code", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (NameCommand == "Add")
                        {
                            NewCode = Guid.NewGuid();
                            CurrentProduct.Code = NewCode;
                        }
                    });
            }
        }

        private RelayCommand<Product> edit;
        public RelayCommand<Product> Edit
        {
            get
            {
                return edit ?? new RelayCommand<Product>(
                    (param) =>
                    {
                        NameCommand = "Edit";
                        CurrentProduct = param;
                        NewCode = CurrentProduct.Code;
                        EditType = CurrentProduct.Type;
                    });
            }
        }

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? new RelayCommand(
                    () =>
                    {
                        if (NameCommand == "Add")
                        {
                            var isExist = ManagerProducts.FirstOrDefault(x => { if (x.Name == CurrentProduct.Name && x.Type == CurrentProduct.Type) return true; else return false; });
                            if (isExist == null)
                            {
                                if (CurrentProduct.ImageUrl == null) CurrentProduct.ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/022/059/000/small_2x/no-image-available-icon-vector.jpg";
                                try
                                {
                                    if (CurrentProduct.Unit == null || CurrentProduct.Type == null || CurrentProduct.Code == Guid.Empty || CurrentProduct.Price == null)
                                    {
                                        throw new Exception("Wrong Price format or empty values!!!");
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
                                MessageBox.Show("Product Exists!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else if (NameCommand == "Edit") 
                        {
                            if (CurrentProduct.ImageUrl == null) CurrentProduct.ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/022/059/000/small_2x/no-image-available-icon-vector.jpg";
                            try
                            {
                                if (CurrentProduct.Unit == null || CurrentProduct.Type == null || CurrentProduct.Code == Guid.Empty || CurrentProduct.Price == null)
                                {
                                    throw new Exception("Wrong Price format or empty values!!!");
                                }
                                var product = ManagerProducts.FirstOrDefault(x=>x.Code == CurrentProduct.Code);
                                product = CurrentProduct;
                                NewCode = Guid.Empty;
                                if (VisibleProducts != ManagerProducts)
                                {
                                    var filtered = ManagerProducts.Where(x => x.Type == EditType).ToList();
                                    VisibleProducts = new ObservableCollection<Product>(filtered);
                                }
                                CurrentProduct = new Product();
                                NameCommand = "Add";
                                EditType = "";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    });
            }
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
                    VisibleProducts.Remove(param);
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
                        using(FileStream fs=new FileStream("Products.json", FileMode.Create))
                        {
                            JsonSerializer.Serialize(fs, ManagerProducts);
                            MessageBox.Show("Data save", "DATA WAS SAVED!", MessageBoxButton.OK, MessageBoxImage.Information);
                            fs.Close();
                        }
                    });
            }
        }
    }
}
