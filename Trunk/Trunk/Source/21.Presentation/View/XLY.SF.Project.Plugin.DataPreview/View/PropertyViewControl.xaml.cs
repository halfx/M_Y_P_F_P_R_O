﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XLY.SF.Framework.BaseUtility;
using XLY.SF.Project.BaseUtility.Helper;
using XLY.SF.Project.Domains;

namespace XLY.SF.Project.Plugin.DataPreview.View
{
    /// <summary>
    /// PropertyViewControl.xaml 的交互逻辑
    /// </summary>
    public partial class PropertyViewControl : UserControl, IDataPreviewRelease
    {
        /// <summary>
        /// 无效时间
        /// </summary>
        private readonly static DateTime InvalidDateTime = new DateTime(1970, 1, 1);

        public PropertyViewControl()
        {
            InitializeComponent();
            this.Loaded += PropertyViewControl_Loaded;
        }

        public void Release()
        {

        }

        private bool IsUserControl_Loaded = false;

        private void PropertyViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsUserControl_Loaded)
            {//只加载一次
                IsUserControl_Loaded = true;
            }
            else
            {
                return;
            }

            if (this.DataContext == null)
            {
                return;
            }
            var obj = this.DataContext as DataPreviewPluginArgument;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (obj.CurrentData is string file)
            {
                if (!File.Exists(file))
                {
                    return;
                }
                FileInfo fi = new FileInfo(file);
                dic[Languagekeys.FileName] = fi.Name;
                dic[Languagekeys.FullPath] = fi.FullName;
                dic[Languagekeys.FileSize] = $"{FileHelper.GetFileSize(fi.Length)} ({fi.Length} {Languagekeys.Bytes})";
                if (fi.CreationTime.Equals(InvalidDateTime))
                {
                    dic[Languagekeys.CreateTime] = String.Empty;
                }
                else
                {
                    dic[Languagekeys.CreateTime] = fi.CreationTime.ToString();
                }
                if (fi.LastWriteTime.Equals(InvalidDateTime))
                {
                    dic[Languagekeys.ModifyTime] = String.Empty;
                }
                else
                {
                    dic[Languagekeys.ModifyTime] = fi.LastWriteTime.ToString();
                }
                if (fi.LastAccessTime.Equals(InvalidDateTime))
                {
                    dic[Languagekeys.AccessTime] = String.Empty;
                }
                else
                {
                    dic[Languagekeys.AccessTime] = fi.LastAccessTime.ToString();
                }
                dic[Languagekeys.Attributes] = fi.Attributes.HasFlag(FileAttributes.ReadOnly) ? (string)Languagekeys.ZhiDu : "";
            }
            else if (obj.CurrentData != null)
            {
                //foreach (var pro in obj.CurrentData.GetType().GetProperties())
                //{
                //    dic[pro.Name] = pro.GetValue(obj.CurrentData).ToSafeString();
                //}
                foreach (var attr in DisplayAttributeHelper.FindDisplayAttributes(obj.CurrentData.GetType()).OrderBy(d => d.ColumnIndex))
                {
                    if (attr.Visibility == EnumDisplayVisibility.ShowInDatabase)        //该属性不需要显示在界面上
                        continue;
                    dic[attr.Text] = attr.GetValue(obj.CurrentData).ToSafeString();
                }
            }

            int row = 0;
            Style tbStyle = this.FindResource("tbProperty") as Style;
            foreach (var p in dic)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

                TextBlock tb = new TextBlock() { Text = p.Key + " :" };
                tb.VerticalAlignment = VerticalAlignment.Top;
                tb.HorizontalAlignment = HorizontalAlignment.Right;
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Margin = new Thickness(3);
                grid.Children.Add(tb);
                tb.SetValue(Grid.RowProperty, row);

                TextBox tb2 = new TextBox() { Text = p.Value };
                tb2.VerticalAlignment = VerticalAlignment.Top;
                tb2.HorizontalAlignment = HorizontalAlignment.Left;
                tb2.Margin = new Thickness(3);
                if(tbStyle != null)
                {
                    tb2.Style = tbStyle;
                }
                grid.Children.Add(tb2);
                tb2.SetValue(Grid.RowProperty, row);
                tb2.SetValue(Grid.ColumnProperty, 1);
                row++;
            }
        }
    }
}
