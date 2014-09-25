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

namespace praktik_estimering
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //UserService us = UserService.Instance();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            
            if (UserService.Instance.login(textboxInitials.Text, passwordbox.Password))
            {
                Overview view = new Overview();
                view.Show();
                this.Close();

            }
        }

        private void Grid_TouchEnter(object sender, TouchEventArgs e)
        {
            LoginClick(sender, e);
        }

        private void ButtonLogin_Admin_Click(object sender, RoutedEventArgs e)
        {
            if(UserService.Instance.loginAdmin(textboxInitials.Text,passwordbox.Password))
            {
                Window view = new Admin();
                view.Show();
                this.Close();
            }
        }
    }
}
