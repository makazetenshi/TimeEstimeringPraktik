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
using System.Windows.Shapes;

namespace praktik_estimering
{
    /// <summary>
    /// Interaction logic for Showroom.xaml
    /// </summary>
    public partial class Showroom : Window
    {
        public Showroom()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserService.Instance.getSelectedPeriodData();
            listAktivies.ItemsSource = UserService.Instance.getSelectedPeriodData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Overview view = new Overview();
            view.Show();
            this.Close();
        }
    }
}
