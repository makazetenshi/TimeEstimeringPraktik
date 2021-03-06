﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace praktik_estimering
{
    /// <summary>
    /// Interaction logic for exam.xaml
    /// </summary>
    public partial class exam : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public exam()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datagridExam.ItemsSource = PeriodService.Instance.ExamnsList().DefaultView;
            datagridExam.Columns[0].IsReadOnly = true;
            datagridExam.Columns[1].IsReadOnly = false;
            datagridExam.Columns[2].IsReadOnly = false;
            datagridExam.Columns[3].IsReadOnly = false;

            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
           }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = (DataView)datagridExam.ItemsSource;
            DataTable dt = cloneTable(dv);

            if (PeriodService.Instance.InsertExamnActivities(dt))
            {
                Showroom sr = new Showroom();
                sr.Show();
                Close();
            }
        }
        private DataTable cloneTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to cancel?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                PeriodService.Instance.cancelEverything();
                Window ov = new Overview();
                ov.Show();
                Close();
            }
        }
    }
}
