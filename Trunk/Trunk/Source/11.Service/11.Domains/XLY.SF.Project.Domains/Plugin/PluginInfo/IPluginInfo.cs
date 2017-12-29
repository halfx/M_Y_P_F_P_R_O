﻿using System;

/* ==============================================================================
* Assembly   ：	XLY.SF.Project.Domains.IPluginInfo
* Description：	  
* Author     ：	fhjun
* Create Date：	2017/9/12 10:26:42
* ==============================================================================*/

namespace XLY.SF.Project.Domains
{
    /// <summary>
    /// 提供所有插件结构定义的基类
    /// </summary>
    public interface IPluginInfo
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        string Guid { get; set; }

        /// <summary>
        /// 插件类型
        /// </summary>
        PluginType PluginType { get; set; }

        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 插件版本,代表该插件支持的APP最低版本
        /// </summary>
        Version Version { get;  }

        /// <summary>
        /// 插件描述信息
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 图标位置，
        /// </summary>
        string Icon { get; set; }

        /// <summary>
        /// 插件分组信息
        /// </summary>
        string Group { get; set; }

        /// <summary>
        /// 组序号
        /// </summary>
        int GroupIndex { get; set; }

        /// <summary>
        /// 插件排序
        /// </summary>
        int OrderIndex { get; set; }

        /// <summary>
        /// 插件状态
        /// </summary>
        PluginState State { get; set; }
    }
}
