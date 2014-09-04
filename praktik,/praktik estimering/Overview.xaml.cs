﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        public Overview()
        {
            InitializeComponent();
        }

        private void overviewLoaded(object sender, RoutedEventArgs e)
        {
            datagridOldPeriods.ItemsSource = UserService.Instance.getPastPeriods().DefaultView;
            datagridOldPeriods.Columns[0].Visibility = Visibility.Hidden;
            datagridOldPeriods.Columns[1].Visibility = Visibility.Hidden;
          
        }

        private void ClickNewPeriod(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("not implemented");
        }

        private void clickViewOld(object sender, RoutedEventArgs e)
        {
            if (datagridOldPeriods.SelectedItem != null)
            {
                DataRowView row = datagridOldPeriods.SelectedItem as DataRowView;
                try
                {
                    DataRowView dataRow = (DataRowView)datagridOldPeriods.SelectedItem;

                    string cellvalue = dataRow.Row.ItemArray[0].ToString();


                    
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(n.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("choose the periode you want specified");
            }
            
        }

        private void clickLogout(object sender, RoutedEventArgs e)
        {
            UserService.Instance.logUserOut();

            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void datagridOldPeriods_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null)
                {
                    dataGridTextColumn.Binding.StringFormat = "{0:d}";
                }
            }
        }






    }
}
