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

namespace XLY.SF.Project.Themes
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:XLY.SF.Project.Themes"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:XLY.SF.Project.Themes;assembly=XLY.SF.Project.Themes"
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
    ///     <MyNamespace:WindowEx/>
    ///
    /// </summary>
    public class WindowEx : Window
    {
        static WindowEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowEx), new FrameworkPropertyMetadata(typeof(WindowEx)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            base.WindowStyle = WindowStyle.None;
            base.AllowsTransparency = true;
            base.ResizeMode = ResizeMode.NoResize;
            base.OnInitialized(e);
        }

        #region 屏蔽系统参数

        public new WindowStartupLocation WindowStartupLocation { get => base.WindowStartupLocation; }

        public new WindowStyle WindowStyle { get => base.WindowStyle; }

        public new bool AllowsTransparency { get => base.AllowsTransparency; }

        public new ResizeMode ResizeMode { get => base.ResizeMode; }

        #endregion

        /// <summary>
        /// 是否可以改变窗体大小
        /// </summary>
        public bool CanResize { get; set; }
    }
}
