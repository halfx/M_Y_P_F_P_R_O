﻿using System;
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

namespace XLY.SF.Project.Plugin.DataView
{
    /// <summary>
    /// ContactDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class ContactDetailControl : UserControl
    {
        public ContactDetailControl()
        {
            InitializeComponent();
        }

        public event DelgateDataViewSelectedItemChanged OnSelectedDataChanged;

        private void lsb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(OnSelectedDataChanged != null)
            {
                OnSelectedDataChanged(lsb1.SelectedItem);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (lsb1.Items.Count > 0)
            {
                lsb1.SelectedValue = lsb1.Items[0];
            }
        }
    }
}
