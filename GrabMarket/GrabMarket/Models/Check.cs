using GrabMarket.View_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabMarket.Models
{
    class Check
    {
       public List<ObservableCollection<BasketElementViewModel>> Basket {  get; set; } = new List<ObservableCollection<BasketElementViewModel>>();
       public string TotalAmount {  get; set; }
    }
}
