﻿using GrabMarket.Models;
using GrabMarket.Resources;
using GrabMarket.Views;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace GrabMarket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DataStore storage = new DataStore();
            using (FileStream fs = new FileStream("Products.json", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    storage.Products = JsonSerializer.Deserialize<ObservableCollection<Product>>(fs);
                    fs.Close();
                }
            }
            List<Check> Checks = new List<Check>();
            using (FileStream fs = new FileStream("Checks.json", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    Checks = JsonSerializer.Deserialize<List<Check>>(fs);
                    fs.Close();
                }
            }
            ViewModelLocator locator = new ViewModelLocator(storage, Checks);
            MainPanel mainPanel = new MainPanel();
            mainPanel.DataContext = locator.GetMainPanelViewModel();
            mainPanel.ShowDialog();
        }
    }

}
