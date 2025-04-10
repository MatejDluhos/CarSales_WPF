using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarSales_WPF
{
    public partial class DatabasePage : Page
    {
        public DatabasePage(List<Car> cars)
        {
            InitializeComponent();

            var groupedCars = cars
            .GroupBy(car => car.ModelName)
            .Select(group => new
            {
                ModelName = group.Key,
                Cars = group.ToList(),
                WeekendTotalWithoutDPH = group.Where(car => car.WasSoldOnWeekend).Sum(car => car.Price),
                WeekendTotalWithDPH = group.Where(car => car.WasSoldOnWeekend).Sum(car => car.PriceWithDPH)
            }).ToList();

            DataGrid.ItemsSource = groupedCars;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }
    }
}
