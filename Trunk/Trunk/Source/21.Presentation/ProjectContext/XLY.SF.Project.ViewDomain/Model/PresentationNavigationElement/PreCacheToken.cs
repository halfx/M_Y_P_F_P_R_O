﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 创建人：Bob
 * 
 * 目前界面所有缓存采用设备ID和导出Key来区分
 * 设备ID应为唯一
 * 增加ExportKey是由于所有设备主页中的界面都是公有的，所以需要在加上ExportKey作为区分
 * 
 */

namespace XLY.SF.Project.ViewDomain.Model.PresentationNavigationElement
{
    /// <summary>
    /// 展示层缓存标记
    /// </summary>
    public class PreCacheToken
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DevID { get; }

        /// <summary>
        /// View导出Key
        /// </summary>
        public string ExportKey { get; }

        public PreCacheToken(string devID, string exportKey)
        {
            DevID = devID;
            ExportKey = exportKey;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            else
            {
                var objTmp = obj as PreCacheToken;
                return objTmp?.DevID == DevID && objTmp?.ExportKey == ExportKey;
            }
        }

        public override int GetHashCode()
        {
            return (DevID + ExportKey).GetHashCode();
        }
    }
}
