using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabMarket.View_Models
{
    class BasketElementViewModel : ViewModelBase
    {
        public Guid Code { get; set; }
        public string Name {  get; set; }

        private int count;
        public int Count
        {
            get { return count; }
            set { Set(ref count, value); }
        }

        private float? price;
        public float? Price
        {
            get { return price; }
            set { Set(ref price, value); }
        }

        public BasketElementViewModel()
        {
            Count = 0;
            Price = 0;
        }
    }
}
