﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLY.SF.Framework.Core.Base.CoreInterface;
using XLY.SF.Project.Domains;

/* ==============================================================================
* Assembly   ：	XLY.SF.Project.Plugin.DataReport.HtmlReportPlugin
* Description：	  
* Author     ：	fhjun
* Create Date：	2017/9/27 10:28:56
* ==============================================================================*/

namespace XLY.SF.Project.Plugin.DataReport
{
    /// <summary>
    /// HtmlReportPlugin
    /// </summary>
    public class HtmlReportPlugin : AbstractDataReportPlugin
    {
        public HtmlReportPlugin()
        {
            PluginInfo = new DataReportPluginInfo()
            {
                Guid = "DC22CD47-D05D-49D1-8B4E-98039FCB2BB0",
                Name = "Html",
                ReportExtension = ".html",
                Description = "",
                OrderIndex = 0,
                Platform = "",
                PluginType = PluginType.SpfReport,
                Group = ""
            };
        }

        private DataReportModulePluginInfo _module = null;

        protected override void Initialize(DataReportPluginArgument arg, IAsyncTaskProgress progress)
        {
            DataReportPluginInfo plugin = (DataReportPluginInfo)PluginInfo;
            if (plugin.Modules.Count == 0)
            {
                throw new Exception("Modules is null");
            }
            _module = plugin.Modules.FirstOrDefault(m => m.Name == arg.ReportModuleName) ?? plugin.Modules[0]; //默认使用第一个模板
            if (!Directory.Exists(arg.ReportPath))
            {
                Directory.CreateDirectory(arg.ReportPath);
            }
            BaseUtility.Helper.FileHelper.CopyDirectory(_module.ZipTempDirectory, arg.ReportPath);      //拷贝模板文件目录
        }

        protected override void ExportData(DataReportPluginArgument arg, IAsyncTaskProgress progress)
        {
            new DataSourceToJsonConverter().ConverterToJsonFile(arg, Path.Combine(arg.ReportPath, "data"));    //在data目录下生成json文件
        }

        protected override void ExportFile(DataReportPluginArgument arg, IAsyncTaskProgress progress)
        {

        }

        protected override string ExportCompleted(DataReportPluginArgument arg, IAsyncTaskProgress progress)
        {
            return Path.Combine(arg.ReportPath, _module.MainFile ?? "");
        }
    }
}
