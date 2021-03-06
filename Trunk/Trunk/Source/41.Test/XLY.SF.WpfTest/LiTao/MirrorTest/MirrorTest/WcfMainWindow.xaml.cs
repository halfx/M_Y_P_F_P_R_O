﻿using DllClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using XLY.SF.Framework.Core.Base.CoreInterface;
using XLY.SF.Project.Devices;
using XLY.SF.Project.Domains;
using XLY.SF.Project.ProxyService;
using XLY.SF.Project.ViewModels.Main;

namespace MirrorTest
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class WcfMainWindow : Window
    {
        public WcfMainWindow()
        {
            
            InitializeComponent();
            this.Foreground = Brushes.Black;
            MirrorView.DataContext = _mirrorViewModel;
            this.Loaded += Window_Loaded;
            this.Closed += MainWindow_Closed; ;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            serverHostManager.StopServerHost();
        }

        WcfMirrorViewModel _mirrorViewModel = new WcfMirrorViewModel();
        ServerHostManager serverHostManager = new ServerHostManager();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            serverHostManager.StartServerHost();            

            ProxyFactory.DeviceMonitor.OnDeviceConnected += (dev, isOnline) =>
            {
                this.Dispatcher.Invoke(new Action(() => DeviceMonitor_OnDeviceConnected(dev, isOnline)));
            };
            ProxyFactory.DeviceMonitor.OpenDeviceService();
        }     

        private void DeviceMonitor_OnDeviceConnected(IDevice dev, bool isOnline)
        {
            _mirrorViewModel.SourcePosition.RefreshPartitions(dev);
            
            SPFTask task = new SPFTask();
            task.Name = "TestName";
            task.Device = (Device)dev;            
            _mirrorViewModel.Initialize(task);
        }
    }
}
