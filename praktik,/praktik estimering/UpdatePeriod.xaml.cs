﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace praktik_estimering
{
    /// <summary>
    /// Interaction logic for UpdatePeriod.xaml
    /// </summary>
    public partial class UpdatePeriod : Window
    {
        public UpdatePeriod()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboboxPeriod.ItemsSource = UpdateService.Instance.getPeriods().DefaultView;
            comboboxPeriod.SelectedValuePath = "Id";
            comboboxPeriod.DisplayMemberPath = "Show";
            comboboxPeriod.SelectedIndex = 0;

            DatagridDay.ItemsSource = UpdateService.Instance.Day.DefaultView;

            DatagridDay.Columns[0].Visibility = Visibility.Hidden;
            DatagridDay.Columns[1].IsReadOnly = true;
        }
        private void ButtonDoneClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("If you do not press update the new data will not be saved. " +
                                                   "Do you wish to continue?", "Confirmation", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                Window over = new Overview();
                over.Show();
                Close();
            }
        }
        private void ButtonUpdateClicked(object sender, RoutedEventArgs e)
        {
            UpdateService.Instance.updatePeriods();
        }
        private void comboboxPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService.Instance.getDataSet((int)comboboxPeriod.SelectedValue);
        }
        private void dayGotFocus(object sender, RoutedEventArgs e)
        {
            DatagridDay.ItemsSource = UpdateService.Instance.Day.DefaultView;

            DatagridDay.Columns[0].Visibility = Visibility.Hidden;
            DatagridDay.Columns[1].IsReadOnly = true;
        }
        private void estimateGotFocus(object sender, RoutedEventArgs e)
        {
            DatagridEstimate.ItemsSource = UpdateService.Instance.Esti.DefaultView;

            DatagridEstimate.Columns[0].Visibility = Visibility.Hidden;
            DatagridEstimate.Columns[1].IsReadOnly = true;
        }
        private void PrakticalGotFocus(object sender, RoutedEventArgs e)
        {
            DatagridFormula.ItemsSource = UpdateService.Instance.Form.DefaultView;

            
            //DatagridFormula.Columns[0].Visibility = Visibility.Hidden;
        }
        private void examGotFocus(object sender, RoutedEventArgs e)
        {
            DatagridExamn.ItemsSource = UpdateService.Instance.Exam.DefaultView;

            //DatagridExamn.Columns[0].Visibility = Visibility.Hidden;
        }



    }
}
