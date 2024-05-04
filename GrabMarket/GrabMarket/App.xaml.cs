using GrabMarket.Models;
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
            FileInfo file = new FileInfo("Products.json");
            if (file.Exists)
            {
                if (file.Length == 0)
                {

                }
                else
                {
                    using (FileStream fs = new FileStream("Products.json", FileMode.OpenOrCreate))
                    {
                        storage.Products = JsonSerializer.Deserialize<ObservableCollection<Product>>(fs);
                        fs.Close();
                    }
                }
            }
            else
            {
                file.Create();
            }

            ViewModelLocator locator = new ViewModelLocator(storage);
            MainPanel mainPanel = new MainPanel();
            mainPanel.DataContext = locator.GetMainPanelViewModel();
            mainPanel.ShowDialog();
        }
    }

}
