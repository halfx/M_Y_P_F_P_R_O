﻿using System;
using System.Collections;
using System.Collections.Generic;
using XLY.SF.Framework.Core.Base.ViewModel;

namespace XLY.SF.Project.Domains
{
    /// <summary>
    /// SD卡设备
    /// </summary>
    public class SDCardDevice : AbstractDevice, IFileSystemDevice
    {
        public SDCardDevice()
        {
            DeviceType = EnumDeviceType.SDCard;
            Status = EnumDeviceStatus.None;
        }

        #region ID
        /// <summary>
        /// 设备ID 唯一标识符
        /// </summary>
        public override string ID { get => $"{DeviceType}:{Name}"; set => throw new NotImplementedException(); }
        #endregion

        //#region DeviceType -- 设备类型

        //private EnumDeviceType _DeviceType;

        ///// <summary>
        ///// 设备类型
        ///// </summary>
        //public EnumDeviceType DeviceType
        //{
        //    get { return _DeviceType; }
        //    set
        //    {
        //        _DeviceType = value;
        //        OnPropertyChanged();
        //    }
        //}
        //#endregion

        //#region Status -- 设备状态

        //[NonSerialized]
        //private EnumDeviceStatus _Status;
        ///// <summary>
        ///// 设备状态
        ///// </summary>
        //public EnumDeviceStatus Status
        //{
        //    get { return _Status; }
        //    set
        //    {
        //        _Status = value;
        //        OnPropertyChanged();
        //    }
        //}

        //#endregion

        ///// <summary>
        ///// 设备名称
        ///// </summary>
        //public string Name { get; set; }

        /// <summary>
        /// 设备数据源
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// 设备打开句柄
        /// </summary>
        public IntPtr Handle { get; set; }

        /// <summary>
        /// 当前装载设备句柄
        /// </summary>
        public IntPtr Handle_Flsh { get; set; }

        private List<FileSystemPartition> _parts;

        /// <summary>
        /// 磁盘分区
        /// </summary>
        public List<FileSystemPartition> Parts
        {
            get { return _parts ?? (_parts = new List<FileSystemPartition>()); }
        }

        /// <summary>
        /// 扫描模式
        /// </summary>
        public byte ScanModel { get; set; }

        /// <summary>
        /// 磁盘编号
        /// </summary>
        public byte DiskNumber { get; set; }

        /// <summary>
        /// 磁盘大小
        /// </summary>
        public ulong Size { get; set; }

        /// <summary>
        /// 总扇区数
        /// </summary>
        public ulong TotalSectors { get { return this.Size > 0 ? this.Size / 512 : 0; } }

        /// <summary>
        /// 是否存在分区
        /// </summary>
        public bool HasPartition
        {
            get { return this.Parts.Count != 0; }
        }

        /// <summary>
        /// 芯片类型：
        /// 0xA000 联发科手机芯片(mtk)
        /// 0xA001 展讯手机芯片
        /// 0xA002 诺机亚手机芯片
        /// 0xA003 WindowsPhone手机芯片
        /// 0xA004 晨星芯片(MStar)
        /// </summary>
        public FlshType FlshType { get; set; }

        /// <summary>
        /// 底层设备类型
        /// </summary>
        public DevType DevType { get; set; }

        public override int GetHashCode()
        {
            return string.Format("{0}{1}{2}", DiskNumber, Name, Size).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SDCardDevice);
        }

        public bool Equals(SDCardDevice other)
        {
            if (null == other)
            {
                return false;
            }

            return other.DiskNumber == DiskNumber && other.Name == Name && other.Size == Size;
        }
    }
}
