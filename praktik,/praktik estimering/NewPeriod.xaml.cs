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
    /// Interaction logic for NewPeriod.xaml
    /// </summary>
    public partial class NewPeriod : Window
    {
        public NewPeriod()
        {
            InitializeComponent();
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
           this.Close();

        }

        private void ButtonNextClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
