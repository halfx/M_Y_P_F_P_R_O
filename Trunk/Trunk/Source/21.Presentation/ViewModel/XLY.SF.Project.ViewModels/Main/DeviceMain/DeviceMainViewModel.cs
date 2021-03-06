﻿using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XLY.SF.Framework.BaseUtility;
using XLY.SF.Framework.Core.Base.MefIoc;
using XLY.SF.Framework.Core.Base.MessageBase;
using XLY.SF.Framework.Core.Base.MessageBase.Navigation;
using XLY.SF.Framework.Core.Base.ViewModel;
using XLY.SF.Project.Domains;
using XLY.SF.Project.Models;
using XLY.SF.Project.Models.Entities;
using XLY.SF.Project.ViewDomain.MefKeys;
using XLY.SF.Project.ViewDomain.VModel.DevHomePage;
using XLY.SF.Project.ViewModels.Main.CaseManagement;
using XLY.SF.Project.ViewModels.Main.DeviceMain.Navigation;

namespace XLY.SF.Project.ViewModels.Main.DeviceMain
{
    [Export(ExportKeys.DeviceMainViewModel, typeof(ViewModelBase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DeviceMainViewModel : ViewModelBase
    {
        #region 子界面类型
        /// <summary>
        /// 子界面类型
        /// </summary>
        public enum DevMainSubViewType
        {
            /// <summary>
            /// 提取主页
            /// </summary>
            DevHomePage,
            /// <summary>
            /// 提取结果
            /// </summary>
            ExtractResult,
            /// <summary>
            /// 文件浏览
            /// </summary>
            FileView,
            /// <summary>
            /// 智能预警
            /// </summary>
            AutoWarning,
        }

        #endregion

        #region Properties

        #region 设备信息

        private DeviceModel _curDevModel;
        /// <summary>
        /// 当前设备信息
        /// </summary>
        public DeviceModel CurDevModel
        {
            get
            {
                return _curDevModel;
            }
            set
            {
                _curDevModel = value;
                base.OnPropertyChanged();
            }
        }

        #endregion

        #region 设备主页导航

        private DevMainSubViewType _curSubViewType;
        /// <summary>
        /// 当前子界面类型
        /// </summary>
        public DevMainSubViewType CurSubViewType
        {
            get
            {
                return this._curSubViewType;
            }

            set
            {
                this._curSubViewType = value;
                base.OnPropertyChanged();
            }
        }

        private object _subView;
        /// <summary>
        /// 子界面内容
        /// </summary>
        public object SubView
        {
            get
            {
                return this._subView;
            }

            set
            {
                this._subView = value;
                base.OnPropertyChanged();
            }
        }

        /// <summary>
        /// 设备主页导航器
        /// </summary>
        public SubViewCacheManager DevNavigationManager { get; private set; }

        #endregion

        #endregion

        #region Commands

        /// <summary>
        /// 文件浏览
        /// </summary>
        public ProxyRelayCommand FileViewCommand { get; private set; }

        /// <summary>
        /// 数据提取结果
        /// </summary>
        public ProxyRelayCommand ExtractionResultCommand { get; private set; }

        /// <summary>
        /// 跳转到智能预警
        /// </summary>
        public ProxyRelayCommand EarlyWarningCommand { get; private set; }

        /// <summary>
        /// 跳转到设备首页
        /// </summary>
        public ProxyRelayCommand DeviceHomePageCommand { get; private set; }

        #endregion

        public DeviceMainViewModel()
        {
            base.MessageAggregation.RegisterGeneralMsg<bool>(this, GeneralKeys.ExtractTaskCompleteMsg, TaskCompleteCallback);
            ExtractionResultCommand = new ProxyRelayCommand(ExeucteExtractionResultCommand);
            FileViewCommand = new ProxyRelayCommand(ExecuteFileViewCommand);
            EarlyWarningCommand = new ProxyRelayCommand(EarlyWarningViewCommand);
            DeviceHomePageCommand = new ProxyRelayCommand(ExecuteDeviceHomePageCommand);
        }

        protected override void InitLoad(object parameters)
        {
            if (parameters != null)
            {
                var _curDevice = parameters as DeviceExtractionAdorner;
                if (_curDevice == null)
                    throw new NullReferenceException(string.Format("当前设备为NULL"));
                CreateDeviceByType(_curDevice);
                //根据设备ID创建导航器
                DevNavigationManager = new SubViewCacheManager(_curDevice.Device.ID);


                //首次加载使用设备首页
                SwitchSubView(DevMainSubViewType.DevHomePage, CurDevModel);
                //SubView = DevNavigationManager.GetOrCreateView(ExportKeys.DeviceHomePageView, CurDevModel);
            }
        }

        #region ExecuteCommands

        private string ExecuteFileViewCommand()
        {
            SwitchSubView(DevMainSubViewType.FileView, CurDevModel.IDevSource);
            return string.Empty;
        }

        private string EarlyWarningViewCommand()
        {
           SwitchSubView(DevMainSubViewType.AutoWarning, CurDevModel.IDevSource);
            //SetAutoWarningPath(tmp.Target.Path);
            return string.Empty;
        }


        private string ExecuteDeviceHomePageCommand()
        {
            SwitchSubView(DevMainSubViewType.DevHomePage, CurDevModel);
            return string.Empty;
        }

        private string ExeucteExtractionResultCommand()
        {
            var tmp = CurDevModel.DeviceExtractionAdorner as DeviceExtractionAdorner;
            SwitchSubView(DevMainSubViewType.ExtractResult, tmp.Target.Path);
            
            return string.Empty;
        }

        #endregion

        /// <summary>
        /// 设置智能预警的文件路径
        /// </summary>
        private void SetAutoWarningPath(string path)
        {
            var viewTmp = DevNavigationManager.GetOrCreateView(ExportKeys.AutoWarningView, path);
            if (string.IsNullOrWhiteSpace(path))
            {
                viewTmp.DataSource.ReceiveParameters(path);
            }
        }

        #region Tools

        #region 设备类型转换

        /// <summary>
        /// 根据类型获取对应的设备信息
        /// </summary>
        /// <param name="idev"></param>
        private void CreateDeviceByType(DeviceExtractionAdorner idev)
        {
            switch (idev.Device.DeviceType)
            {
                case EnumDeviceType.None:
                    break;
                case EnumDeviceType.Phone:
                    CurDevModel = GetPhoneDevice(idev);
                    break;
                case EnumDeviceType.Chip:
                    break;
                case EnumDeviceType.Disk:
                    CurDevModel = GetLocalFileDevice(idev);
                    break;
                case EnumDeviceType.SDCard:
                    CurDevModel = GetMemoryCardDevice(idev);
                    break;
                case EnumDeviceType.SIM:
                    break;
            }
        }

        private DeviceModel GetPhoneDevice(DeviceExtractionAdorner idev)
        {
            var tmpDev = idev.Device as XLY.SF.Project.Domains.Device;
            DeviceModel targetDev = new PhoneDevModel()
            {
                Name = idev.Name,
                DevModel = tmpDev.Model,
                IMEI = tmpDev.IMEI,
                IsRoot = tmpDev.IsRoot,
                SerialNumber = tmpDev.SerialNumber,
                System = string.Format("{0}{1}", tmpDev.OSType, tmpDev.OSVersion),
                IsAndroid = tmpDev.OSType == EnumOSType.Android,

                DeviceTotalSize = tmpDev.TotalDiskCapacity * 1.0 / 1024 / 1024 / 1024,
                UnusedTotalSizeOfDevice = tmpDev.TotalDiskAvailable * 1.0 / 1024 / 1024 / 1024,
                SDCardTotalSize = tmpDev.TotalDataCapacity * 1.0 / 1024 / 1024 / 1024,
                UnusedTotalSizeOfSD = tmpDev.TotalDataAvailable * 1.0 / 1024 / 1024 / 1024,

                DeviceExtractionAdorner = idev,
                IDevSource = idev.Device
            };

            return targetDev;
        }

        private DeviceModel GetLocalFileDevice(DeviceExtractionAdorner idev)
        {
            var lfDev = idev.Device as XLY.SF.Project.Domains.LocalFileDevice;
            DeviceModel targetDev = new LocalFileDevModel()
            {
                Name = idev.Name,
                FilePath = lfDev.PathName,
                FileTypeName = "默认类型",
                DeviceExtractionAdorner = idev,
                IDevSource = idev.Device
            };
            return targetDev;
        }

        private DeviceModel GetMemoryCardDevice(DeviceExtractionAdorner idev)
        {
            var sdCDev = idev.Device as XLY.SF.Project.Domains.SDCardDevice;
            DeviceModel targetDev = new MemoryCardDevModel()
            {
                Name = idev.Name,
                MemoryCardTypeName = "",
                Number = sdCDev.DiskNumber.ToString(),
                DeviceExtractionAdorner = idev,
                IDevSource = idev.Device
            };
            return targetDev;
        }

        #endregion

        #endregion

        #region 任务完成回调

        //自动提取完成
        private void TaskCompleteCallback(GeneralArgs<bool> args)
        {
            //跳转
            var tmp = CurDevModel.DeviceExtractionAdorner as DeviceExtractionAdorner;
            //var a = DevNavigationManager.GetOrCreateView(ExportKeys.DataDisplayView, tmp.Target.Path);
            //a.DataSource.ReceiveParameters(args.Parameters);
            //SubView = a;


            SwitchSubView(DevMainSubViewType.ExtractResult, tmp.Target.Path, args.Parameters);
            SetAutoWarningPath(tmp.Target.Path);
        }

        #endregion

        #region 界面切换

        /// <summary>
        /// 切换子界面
        /// </summary>
        /// <param name="subType">子界面类型</param>
        /// <param name="initParams">初始化参数</param>
        /// <param name="params">参数</param>
        private void SwitchSubView(DevMainSubViewType subType, object initParams, object @params = null)
        {
            string exportKey = string.Empty;
            switch (subType)
            {
                case DevMainSubViewType.DevHomePage:
                    exportKey = ExportKeys.DeviceHomePageView;
                    break;
                case DevMainSubViewType.ExtractResult:
                    exportKey = ExportKeys.DataDisplayView;
                    break;
                case DevMainSubViewType.FileView:
                    exportKey = ExportKeys.FileBrowingView;
                    break;

                case DevMainSubViewType.AutoWarning:
                    //todo 迁入tfs时需要删除
                    exportKey = ExportKeys.AutoWarningView;
                    break;
            }
            CurSubViewType = subType;
            var viewTmp = DevNavigationManager.GetOrCreateView(exportKey, initParams);
            if (@params != null)
                viewTmp.DataSource.ReceiveParameters(@params);
            SubView = viewTmp;
        }

        #endregion
    }
}
