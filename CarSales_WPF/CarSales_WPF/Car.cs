using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales_WPF
{
    public class Car
    {
        public string ModelName { get; set; }
        public DateTime DateOfSale { get; set; }
        public Double Price { get; set; }
        public Double DPH { get; set; }

        public bool WasSoldOnWeekend
        {
            get { return DateOfSale.DayOfWeek == DayOfWeek.Saturday || DateOfSale.DayOfWeek == DayOfWeek.Sunday; }
        }

        public double PriceWithDPH
        {
            get { return Price * ((100 + DPH) / 100.0); }
        }
    }
}
