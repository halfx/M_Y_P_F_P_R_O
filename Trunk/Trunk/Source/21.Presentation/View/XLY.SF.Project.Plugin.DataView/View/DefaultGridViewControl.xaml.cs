﻿using System;
using System.Collections;
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
using XLY.SF.Project.Domains;
using XLY.SF.Project.Themes;

namespace XLY.SF.Project.Plugin.DataView.View.Controls
{
    /// <summary>
    /// DefaultGridViewControl.xaml 的交互逻辑
    /// </summary>
    public partial class DefaultGridViewControl : UserControl
    {
        public DefaultGridViewControl()
        {
            InitializeComponent();
            this.Loaded += DefaultGridViewControl_Loaded;
        }

        private void DefaultGridViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindGrid();
        }

        #region 公共属性和方法

        public event DelgateDataViewSelectedItemChanged OnSelectedDataChanged;
        #endregion

        #region praviate
        DataViewPluginArgument _arg => this.DataContext as DataViewPluginArgument;
        #endregion

        #region 事件
        /// <summary>
        /// 表格行选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectedDataChanged?.Invoke(new DataPreviewPluginArgument() { CurrentData = (sender as DataGrid).SelectedItem,  PluginId = _arg.DataSource?.PluginInfo?.Guid, Type = (_arg.CurrentData as dynamic)?.Type });
        }

        /// <summary>
        /// 点击了全部勾选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookmarkAll_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int newBmk = cb.IsChecked == true ? 0 : -1;
            loadingPanel.Visibility = Visibility.Visible;
            IDataItems di = _arg.Items;
            Task.Factory.StartNew(() =>
            {
                //di.UpdateRange("BookMarkId", newBmk);
                //foreach (AbstractDataItem i in dg.ItemsSource)
                //{
                //    i.BookMarkId = newBmk;
                //}
                this.Dispatcher.Invoke(() => loadingPanel.Visibility = Visibility.Collapsed);
            });
        }

        /// <summary>
        /// 点击了全部标记按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            //CheckBox cb = sender as CheckBox;
            //loadingPanel.Visibility = Visibility.Visible;
            //IDataItems di = _arg.Items;
            //var ischecked = cb.IsChecked;
            //int state = cb.IsChecked == null ? -1 : cb.IsChecked == true ? 1 : 0;
            //Task.Factory.StartNew(() =>
            //{
            //    di.UpdateRange("IsChecked", state);
            //    foreach (AbstractDataItem i in dg.ItemsSource)
            //    {
            //        i.IsChecked = ischecked;
            //    }
            //    this.Dispatcher.Invoke(() => loadingPanel.Visibility = Visibility.Collapsed);
            //});
        }

        /// <summary>
        /// 列模板定义中的属性名称，在读取列模板时替换为实际的属性
        /// </summary>
        private const string PROPERTY_NAME = "$PropertyName$";

        /// <summary>
        /// 动态生成表格列
        /// </summary>
        private void BindGrid()
        {
            dg.Columns.Clear();
            object type = _arg.CurrentData is TreeNode node ? node.Type : _arg.CurrentData is AbstractDataSource sp ? sp.Type : null;
            if(type == null)
            {
                return;
            }
            ////添加勾选列
            //DataGridTemplateColumn chkCol = this.FindResource("checkboxColumnTemplate") as DataGridTemplateColumn;
            //chkCol.Width = 43;
            //chkCol.MinWidth = 43;
            //dg.Columns.Add(chkCol);
            //添加书签列
            DataGridTemplateColumn bmkCol = this.FindResource("bookmarkColumnTemplate") as DataGridTemplateColumn;
            bmkCol.Width = 85;
            bmkCol.MinWidth = 85;
            dg.Columns.Add(bmkCol);

            if (type is Type t)
            {
                foreach(var attr in DisplayAttributeHelper.FindDisplayAttributes(t).OrderBy(d=>d.ColumnIndex))
                {
                    if (attr.Visibility == EnumDisplayVisibility.ShowInDatabase)        //该属性不需要显示在界面上
                        continue;

                    var ww = attr.Width <= 0 ? new DataGridLength(50, DataGridLengthUnitType.Auto) : new DataGridLength(attr.Width, DataGridLengthUnitType.Pixel);
                    var minw = attr.Width <= 0 ? 50 : attr.Width;
                    var maxw = 400;
                    if (attr.Owner.Name == "DataState")      //如果是状态列，则单独处理
                    {
                        DataGridTemplateColumn stateCol = new DataGridTemplateColumn() { Header = attr.Text, Width = ww, MinWidth = minw, MaxWidth = maxw };
                        stateCol.CellTemplate = XamlResouceReader.ToDataTemplate<DataTemplate>("ThemesStyle.DataGridStyle.DataGridDataStateColumnTemplate.xaml", c => c.Replace(PROPERTY_NAME, attr.Owner.Name));
                        dg.Columns.Add(stateCol);
                    }
                    else if (attr.ColumnType == EnumColumnType.URL) //超链接列
                    {
                        DataGridTemplateColumn col = new DataGridTemplateColumn() { Header = attr.Text, Width = ww, MinWidth = minw, MaxWidth = maxw };
                        col.CellTemplate = XamlResouceReader.ToDataTemplate<DataTemplate>("ThemesStyle.DataGridStyle.DataGridUrlColumnTemplate.xaml", c => c.Replace(PROPERTY_NAME, attr.Owner.Name));
                        dg.Columns.Add(col);
                    }
                    else if (attr.ColumnType == EnumColumnType.Image)  //图片列
                    {
                        DataGridTemplateColumn col = new DataGridTemplateColumn() { Header = attr.Text, Width = ww, MinWidth = minw, MaxWidth = maxw };
                        col.CellTemplate = XamlResouceReader.ToDataTemplate<DataTemplate>("ThemesStyle.DataGridStyle.DataGridImageColumnTemplate.xaml", c => c.Replace(PROPERTY_NAME, attr.Owner.Name));
                        dg.Columns.Add(col);
                    }
                    else
                    {
                        DataGridBoundColumn col = new DataGridTextColumn() { Header = attr.Text, Binding = new Binding(attr.Owner.Name), Width = ww, MinWidth = minw, MaxWidth = maxw };
                        dg.Columns.Add(col);
                    }
                }
            }
            else if(type is string)
            {

            }
        }
        #endregion

    }
}
