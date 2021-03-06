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
    ///     <MyNamespace:IconToggleButton/>
    ///
    /// </summary>
    public class IconRadioButton : RadioButton
    {
        static IconRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconRadioButton), new FrameworkPropertyMetadata(typeof(IconRadioButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (base.Template.FindName("Arrow", this) != null)
                MouseEnter += IconRadioButton_MouseEnter;
        }

        private void IconRadioButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.IsChecked = true;
        }

        #region Icon

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(IconRadioButton), new PropertyMetadata(null));

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(IconRadioButton), new PropertyMetadata(20d));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(IconRadioButton), new PropertyMetadata(20d));

        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconPaddingProperty =
            DependencyProperty.Register("IconPadding", typeof(Thickness), typeof(IconRadioButton), new PropertyMetadata(new Thickness(0, 0, 28, 0)));

        #endregion

        #region 是否需要进入Icon

        public Thickness MouseOverIconPadding
        {
            get { return (Thickness)GetValue(MouseOverIconPaddingProperty); }
            set { SetValue(MouseOverIconPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverIconPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverIconPaddingProperty =
            DependencyProperty.Register("MouseOverIconPadding", typeof(Thickness), typeof(IconRadioButton), new PropertyMetadata(null));

        #endregion
    }
}
