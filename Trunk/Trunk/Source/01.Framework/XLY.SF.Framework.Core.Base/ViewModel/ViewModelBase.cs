﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using XLY.SF.Framework.Core.Base.CoreInterface;
using XLY.SF.Framework.Core.Base.MessageAggregation;
using XLY.SF.Framework.Core.Base.MessageBase;
using XLY.SF.Framework.Core.Base;


/*************************************************
 * 创建人：Bob
 * 创建时间：2017/2/24 16:23:37
 * 类功能说明：
 *
 *************************************************/

namespace XLY.SF.Framework.Core.Base.ViewModel
{
    public class ViewModelBase : NotifyPropertyBase
    {
        #region 公有属性定义

        /// <summary>
        /// ViewModel标识
        /// </summary>
        public Guid ViewModelID { get; private set; }

        /// <summary>
        /// 是否加载过
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// 事件聚合器（广播消息）
        /// </summary>
        public MsgAggregation MessageAggregation
        {
            get
            {
                return MsgAggregation.Instance;
            }
        }

        #endregion

        #region 构造函数
        
        internal void SetViewID(Guid viewID)
        {
            ViewModelID = viewID;
        }

        #endregion

        #region 关闭View

        /// <summary>
        /// 注销监听消息，同时回调关闭
        /// </summary>
        public void ViewClosedCallback()
        {
            //注销所有消息
            MsgAggregation.Instance.UnRegisterMsgAll(this);
            //DialogResult = false;
            Closed();
        }

        /// <summary>
        /// 视图关闭后
        /// </summary>
        protected virtual void Closed()
        {
        }

        /// <summary>
        /// 关闭View【通过消息关闭View】
        /// </summary>
        protected void CloseView()
        {
            CloseViewOfNewWindowArgs args = new CloseViewOfNewWindowArgs(ViewModelID);
            //发送关闭View消息
            MsgAggregation.Instance.SendNavigationMsgForCloseWindow(args);
        }

        #endregion

        #region 加载ViewModel

        /// <summary>
        /// 加载ViewModel，只加载一次
        /// </summary>
        public void LoadViewModel(object parameters = null)
        {
            if (IsLoaded) return;
            IsLoaded = true;
            InitLoad(parameters);
        }

        /// <summary>
        /// 初始化时加载
        /// </summary>
        /// <param name="parameters"></param>
        protected virtual void InitLoad(object parameters)
        {

        }

        /// <summary>
        /// 接收参数
        /// </summary>
        public virtual void ReceiveParameters(object parameters)
        {

        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Release()
        {

        }

        #endregion

        #region 导航

        /// <summary>
        /// 主界面导航
        /// </summary>
        /// <param name="exportKey"></param>
        public void NavigationForMainWindow(string exportKey, object param = null)
        {
            NormalNavigationArgs args = NormalNavigationArgs.CreateMainViewNavigationArgs(exportKey, param);
            MsgAggregation.Instance.SendNavigationMsgForMainView(args);
        }

        /// <summary>
        /// 打开新窗体
        /// </summary>
        /// <param name="exportKey"></param>
        public void NavigationForNewWindow(string exportKey,
            object param = null,
            bool showInTaskBar = false,
            bool topMost = false)
        {
            NormalNavigationArgs args = NormalNavigationArgs.CreateWindowNavigationArgs(exportKey, param, showInTaskBar, topMost);
            MsgAggregation.Instance.SendNavigationMsgForWindow(args);
        }

        #endregion

        #region 返回结果【需要时重载】
        
        /// <summary>
        /// 是否有模式对话框返回值
        /// </summary>
        public bool DialogResult { get; protected set; }

        /// <summary>
        /// 返回当前界面结果
        /// </summary>
        /// <returns></returns>
        public virtual object GetResult()
        {
            return null;
        }

        #endregion

        #region ViewContainer

        /// <summary>
        /// 当前View的容器【目前主要考虑用于屏幕截图所用】
        /// </summary>
        private object ViewContainer { get; set; }

        /// <summary>
        /// 设置ViewContainer
        /// </summary>
        /// <param name="container"></param>
        public void SetViewContainer(object container)
        {
            ViewContainer = container;
        }

        /// <summary>
        /// 获取ViewContainer
        /// </summary>
        /// <returns></returns>
        protected object GetViewContainer()
        {
            return ViewContainer;
        }

        #endregion
    }
}
