﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLY.SF.Project.ViewDomain.MefKeys
{
    public class ExportKeys
    {
        #region 其他

        #region 单例包装器
        /// <summary>
        /// 单例包装器
        /// </summary>
        public const string OtherSingleWrapper = "ExportKeys_OtherSingleWrapper";

        #endregion

        #region 配置文件Helper
        /// <summary>
        /// 配置文件Helper
        /// </summary>
        public const string OtherSysConfigHelper = "ExportKey_OtherSysConfigHelper";

        #endregion

        #region 消息框
        /// <summary>
        /// 消息框
        /// </summary>
        public const string OtherMessageBox = "ExportKey_OtherMessageBox";

        #endregion

        #region 模块接口
        /// <summary>
        /// 模块接口
        /// </summary>
        public const string OtherLoadModule = "ExportKey_OtherLoadModule";

        #endregion

        #endregion

        #region 模块

        #region 登录

        /// <summary>
        /// 登录View
        /// </summary>
        public const string ModuleLoginView = "ExportKey_ModuleLoginView";
        /// <summary>
        /// 登录ViewModel
        /// </summary>
        public const string ModuleLoginViewModel = "ExportKey_ModuleLoginViewModel";

        #endregion

        #region 加载

        /// <summary>
        /// 加载View
        /// </summary>
        public const string ModuleLoadingView = "ExportKey_ModuleLoadingView";
        /// <summary>
        /// 加载ViewModel
        /// </summary>
        public const string ModuleLoadingViewModel = "ExportKey_ModuleLoadingViewModel";

        #endregion

        #region 主界面

        /// <summary>
        /// 主界面View
        /// </summary>
        public const string ModuleMainUcView = "ExportKey_ModuleMainUcView";
        /// <summary>
        /// 主界面ViewModel
        /// </summary>
        public const string ModuleMainViewModel = "ExportKey_ModuleMainViewModel";

        public const String DeviceListView = "ExportKey_DeviceListView";
        public const String DeviceListViewModel = "ExportKey_DeviceListViewModel";
        
        #endregion

        #region 首页

        /// <summary>
        /// 首页View
        /// </summary>
        public const string HomePageView = "ExportKey_HomePageView";
        /// <summary>
        /// 首页ViewModel
        /// </summary>
        public const string HomePageViewModel = "ExportKey_HomePageViewModel";

        #endregion

        #region 模版

        /// <summary>
        /// 模版
        /// </summary>
        public const string ViewTemplateView = "ExportKey_ViewTemplateView";
        /// <summary>
        /// 模版
        /// </summary>
        public const string ViewTemplateViewModel = "ExportKey_ViewTemplateViewModel";

        #endregion

        #region 案例

        public const string CaseCreationView = "ExportKey_CaseCreationView";

        public const string CaseCreationViewModel = "ExportKey_CaseCreationViewModel";

        public const string CaseListView = "ExportKey_CaseListView";

        public const string CaseListViewModel = "ExportKey_CaseListViewModel";

        public const string CaseSelectionView = "ExportKey_CaseSelectionView";

        public const string CaseSelectionViewModel = "ExportKey_CaseSelectionViewModel";

        public const string DeviceWindowContentView = "ExportKey_DeviceWindowContentView";

        public const string DeviceWindowContentViewModel = "ExportKey_DeviceWindowContentViewModel";

        public const string DeviceWindowClosedMsg = "DeviceWindowClosed";

        #endregion

        #region 数据源选择
        public const string DeviceSelectView = "ExportKey_DeviceSelectView";
        public const string DeviceSelectViewModel = "ExportKey_DeviceSelectViewModel";
        public const string DeviceSelectFileView = "DeviceSelectFileView";
        public const string DeviceSelectFileViewModel = "DeviceSelectFileViewModel";
        public const string DeviceAddedMsg = "AddDevice";
        #endregion

        #region 设备提取首页

        /// <summary>
        /// 设备首页
        /// </summary>
        public const string DeviceHomePageView = "ExportKey_DeviceHomePageView";
        public const string DeviceHomePageViewModel = "ExportKey_DeviceHomePageViewModel";

        /// <summary>
        /// 设备主页
        /// </summary>
        public const string DeviceMainView = "ExportKey_DeviceMainView";
        /// <summary>
        /// 设备主页
        /// </summary>
        public const string DeviceMainViewModel = "ExportKey_DeviceMainViewModel";

        /// <summary>
        /// 提取展示页
        /// </summary>
        public const string ExtractionView = "ExportKey_ExtractionView";

        /// <summary>
        /// 提取展示页
        /// </summary>
        public const string ExtractionViewModel = "ExportKey_ExtractionViewModel";

        /// <summary>
        /// 文件浏览页
        /// </summary>
        public const string FileBrowingView = "ExportKey_ModuleFileBowingView";

        /// <summary>
        /// 智能预警页面
        /// </summary>
        public const string AutoWarningView = "ExportKey_AutoWarningView";

        /// <summary>
        /// 智能预警页面
        /// </summary>
        public const string AutoWarningViewModel = "ExportKey_AutoWarningViewModel";

        #endregion

        #region 选择控件（路径，文件，打开）
        /// <summary>
        /// 文件选择
        /// </summary>
        public const string SelectFileView = "ExportKey_SelectFileView";
        /// <summary>
        /// 文件选择
        /// </summary>
        public const string SelectFileViewViewModel = "ExportKey_SelectFileViewViewModel";

        /// <summary>
        /// 文件夹选择
        /// </summary>
        public const string SelectFolderView = "ExportKey_SelectFolderView";
        /// <summary>
        /// 文件夹选择
        /// </summary>
        public const string SelectFolderViewModel = "ExportKey_SelectFolderViewModel";

        #endregion

        #region 数据展示
        public const string DataDisplayView = "DataDisplayView";
        public const string DataDisplayViewModel = "DataDisplayViewModel";
        #endregion

        #endregion 数据导出
        public const String ExportDataView = "ExportDataView";
        public const String ExportDataViewViewModel = "ExportDataViewViewModel";
        #region 数据导出

        #region 数据预览
        public const string DataPreView = "DataPreView";
        public const string DataPreViewModel = "DataPreViewModel";
        #endregion

        #region 设置

        #region 用户

        public const String SettingsUserListView = "ExportKey_Settings_UserListView";

        public const String SettingsUserListViewModel = "ExportKey_Settings_UserListViewModel";

        public const String SettingsUserInfoView = "ExportKey_Settings_UserInfoView";

        public const String SettingsUserInfoViewModel = "ExportKey_Settings_UserInfoViewModel";

        #endregion

        #region 系统设置

        public const String SettingsBasicView = "ExportKey_Settings_BasicSettingsView";

        public const String SettingsBasicViewModel = "ExportKey_Settings_BasicSettingsViewModel";

        public const String SettingsCaseTypeView = "ExportKey_Settings_CaseTypeView";

        public const String SettingsCaseTypeViewModel = "ExportKey_Settings_CaseTypeViewModel";

        public const String SettingsUnitView = "ExportKey_Settings_UnitView";

        public const String SettingsUnitViewModel = "ExportKey_Settings_UnitViewModel";

        public const String SettingsInspectionView = "ExportKey_Settings_InspectionView";

        public const String SettingsInspectionViewModel = "ExportKey_Settings_InspectionViewModel";

        public const String SettingsView = "ExportKey_SettingsView";

        #endregion

        #region 插件管理

        public const String SettingsPluginListView = "ExportKey_Settings_PluginListView";

        public const String SettingsPluginListViewViewModel = "ExportKey_Settings_PluginListViewModel ";

        #endregion

        #region 操作指引

        public const String SettingsGuideHomeView = "ExportKey_Settings_GuideHomeView";

        public const String SettingsGuideHomeViewModel = "ExportKey_Settings_GuideHomeViewModel";

        #endregion

        #endregion

        #region

        public const string MirrorView= "ExportKey_MirrorView";

        #endregion

        #endregion

        #region 插件
        /// <summary>
        /// 表示导出为插件(如数据解析插件）
        /// </summary>
        public const string PluginKey = "plugin";
        /// <summary>
        /// 表示导出为脚本插件(如python插件）
        /// </summary>
        public const string PluginScriptKey = "PluginScriptKey";
        /// <summary>
        /// 表示导出为插件加载器
        /// </summary>
        public const string PluginLoaderKey = "PluginLoader";
        /// <summary>
        /// 表示导出为插件适配器
        /// </summary>
        public const string PluginAdapterKey = "PluginAdapter";
        /// <summary>
        /// 表示导出为脚本执行上下文
        /// </summary>
        public const string ScriptContextKey = "ScriptContext";
        #endregion
    }
}
