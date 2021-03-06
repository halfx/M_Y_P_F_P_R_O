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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XLY.SF.Project.Themes.CustromControl
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:XLY.SF.Project.Themes.CustromControl"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:XLY.SF.Project.Themes.CustromControl;assembly=XLY.SF.Project.Themes.CustromControl"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:LoadingControl/>
    ///
    /// </summary>
    public class LoadingControl : Control
    {
        private Storyboard _sbOnLoading;
        private StackPanel _spMain;

        static LoadingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingControl), new FrameworkPropertyMetadata(typeof(LoadingControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _spMain = this.Template.FindName("sp_Main", this) as StackPanel;
            _sbOnLoading = this.Template.Resources["OnLoading"] as Storyboard;
            _sbOnLoading.Begin(_spMain);

            RtFills = new Brush[6];
            SdColors = new Color[6];

            char[] colorTmp = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'A', 'B', 'C', 'D', 'E', 'F' };
            
            GetRandomColor(colorTmp);
        }

        private void GetRandomColor(char[] colorTmp)
        {
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                sb.Append('#');
                for (int ii = 0; ii < 6; ii++)
                {
                    sb.Append(colorTmp[r.Next(colorTmp.Length - 1)]);
                }
                SdColors[i]= (Color)ColorConverter.ConvertFromString(sb.ToString());
                RtFills[i] = new SolidColorBrush(SdColors[i]);
                sb.Clear();
            }
        }

        public Brush[] RtFills { get; set; }
        public Color[] SdColors { get; set; }
    }
}
