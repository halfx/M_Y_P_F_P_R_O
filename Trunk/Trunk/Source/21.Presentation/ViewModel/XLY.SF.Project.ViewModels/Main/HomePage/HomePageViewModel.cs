﻿using GalaSoft.MvvmLight.CommandWpf;
using ProjectExtend.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLY.SF.Framework.Core.Base.CoreInterface;
using XLY.SF.Framework.Core.Base.MessageBase;
using XLY.SF.Framework.Core.Base.ViewModel;
using XLY.SF.Project.CaseManagement;
using XLY.SF.Project.Models;
using XLY.SF.Project.Models.Entities;
using XLY.SF.Project.Models.Logical;
using XLY.SF.Project.ViewDomain.MefKeys;
using XLY.SF.Project.ViewDomain.Model.MessageElement;
using XLY.SF.Project.ViewModels.Tools;

namespace XLY.SF.Project.ViewModels.Main
{
    [Export(ExportKeys.HomePageViewModel, typeof(ViewModelBase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomePageViewModel : ViewModelBase
    {
        #region Constructors

        [ImportingConstructor]
        public HomePageViewModel(IRecordContext<RecentCase> dbService, IPopupWindowService service, IMessageBox msgService)
        {
            MainFunDepict = Path.Combine(Environment.CurrentDirectory, "CacheData\\FunctionDepict\\首页-样图_03.png");
            Sub1FunDepict = Path.Combine(Environment.CurrentDirectory, "CacheData\\FunctionDepict\\首页-样图_05.png");
            Sub2FunDepict = Path.Combine(Environment.CurrentDirectory, "CacheData\\FunctionDepict\\首页-样图_08.png");
            Sub3FunDepict = Path.Combine(Environment.CurrentDirectory, "CacheData\\FunctionDepict\\首页-样图_10.png");

            _dbService = dbService;
            _winService = service;
            _msgBoxService = msgService;

            CreateCaseCommand = new ProxyRelayCommand(ExecuteCreateCaseCommand, base.ModelName);
            OpenAllCaseCommand = new ProxyRelayCommand(ExecuteOpenAllCaseCommand, base.ModelName);
            OpenLocalCaseCommand = new ProxyRelayCommand(ExecuteOpenLocalCaseCommand, base.ModelName);
            OpenCaseCommand = new ProxyRelayCommand<RecentCaseEntityModel>(ExecuteOpenCaseCommand, base.ModelName);

            RecentCaseItems = new ObservableCollection<RecentCaseEntityModel>();

            LoadRecentCreationCaseItems();
        }

        #endregion

        #region Commands

        /// <summary>
        /// 新建案例
        /// </summary>
        public ProxyRelayCommand CreateCaseCommand { get; set; }
        /// <summary>
        /// 打开所有案例
        /// </summary>
        public ProxyRelayCommand OpenAllCaseCommand { get; set; }
        /// <summary>
        /// 打开本地案例
        /// </summary>
        public ProxyRelayCommand OpenLocalCaseCommand { get; set; }
        /// <summary>
        /// 打开案例
        /// </summary>
        public ProxyRelayCommand<RecentCaseEntityModel> OpenCaseCommand { get; set; }

        #endregion

        #region Properties

        #region Private

        /// <summary>
        /// 数据库服务
        /// </summary>
        private IRecordContext<RecentCase> _dbService;

        /// <summary>
        /// 消息框服务
        /// </summary>
        private IMessageBox _msgBoxService;

        /// <summary>
        /// 弹窗服务
        /// </summary>
        private IPopupWindowService _winService;

        #endregion

        /// <summary>
        /// 最近案例
        /// </summary>
        public ObservableCollection<RecentCaseEntityModel> RecentCaseItems { get; set; }

        #region 暂用

        public string MainFunDepict { get; private set; }
        public string Sub1FunDepict { get; private set; }
        public string Sub2FunDepict { get; private set; }
        public string Sub3FunDepict { get; private set; }

        #endregion

        #endregion

        #region Tools

        //加载最新创建案例
        private void LoadRecentCreationCaseItems()
        {
            var @case = _dbService.Records.OrderByDescending((t) => t.Timestamp).Take(10).ToModels<RecentCase, RecentCaseEntityModel>();
            foreach (var item in @case)
            {
                RecentCaseItems.Add(item);
            }
        }

        #endregion

        protected override void Closed()
        {

        }

        #region ExcuteCommands

        private string ExecuteOpenCaseCommand(RecentCaseEntityModel arg)
        {
            var @case = Case.Open(arg.CaseProjectFile);
            if (@case != null)
            {
                SystemContext.Instance.CurrentCase = @case;
                base.NavigationForMainWindow(ExportKeys.DeviceSelectView);
                return string.Format("打开案例{0}成功", @case.Name);
            }
            else
            {
                _msgBoxService.ShowDialogErrorMsg("打开的案例不存在或已经删除");
                return string.Empty;
            }
        }

        private string ExecuteCreateCaseCommand()
        {
            //展开创建案例界面
            EditCaseNavigationHelper.SetEditCaseViewStatus(true, false);

            return "打开新建案例";
        }

        private string ExecuteOpenAllCaseCommand()
        {
            base.NavigationForNewWindow(ExportKeys.CaseListView);

            return "打开所有案例";
        }

        private string ExecuteOpenLocalCaseCommand()
        {
            string fileFullPath = _winService.OpenFileDialog("案例项目文件|*.cp");
            if (!string.IsNullOrWhiteSpace(fileFullPath))
            {
                var @case = Case.Open(fileFullPath);
                if (@case != null)
                {
                    SystemContext.Instance.CurrentCase = @case;
                    base.NavigationForMainWindow(ExportKeys.DeviceSelectView);
                    return string.Format("打开本地案例{0}成功", @case.Name);
                }
                else
                    _msgBoxService.ShowDialogErrorMsg("打开的案例不存在或已经删除");
            }
            return string.Empty;
        }

        #endregion
    }
}
