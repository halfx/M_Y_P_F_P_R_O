﻿// ***********************************************************************
// Assembly:XLY.SF.Project.Plugin.Adapter.Loader
// Author:Songbing
// Created:2017-04-11 14:50:57
// Description:
// ***********************************************************************
// Last Modified By:
// Last Modified On:
// ***********************************************************************

using System.Collections.Generic;
using XLY.SF.Framework.Core.Base.CoreInterface;
using XLY.SF.Project.Domains;


namespace XLY.SF.Project.Plugin.Adapter
{
    /// <summary>
    /// 插件加载器抽象类
    /// </summary>
    internal abstract class AbstractPluginLoader : IPluginLoader
    {
        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="asyn"></param>
        /// <returns></returns>
        public IEnumerable<IPlugin> Load(IAsyncTaskProgress asyn, params string[] pluginPaths)
        {
            if(!IsLoaded)
            {
                this.LoadPlugin(asyn, pluginPaths);
                this.IsLoaded = true;
            }

            return this.Plugins;
        }

        /// <summary>
        /// 是否已加载
        /// </summary>
        protected bool IsLoaded = false;

        /// <summary>
        /// 插件列表
        /// </summary>
        public virtual ICollection<IPlugin> Plugins { get; set; }

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="asyn"></param>
        /// <returns></returns>
        protected abstract void LoadPlugin(IAsyncTaskProgress asyn, params string[] pluginPaths);

    }
}
