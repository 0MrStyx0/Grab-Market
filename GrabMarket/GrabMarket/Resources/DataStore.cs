using GrabMarket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabMarket.Resources
{
    class DataStore
    {
        public ObservableCollection<Product> Products { get; set; }
        public DataStore()
        {
            Products = new ObservableCollection<Product>();
        }
    }
}
