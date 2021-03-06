﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLY.SF.Framework.Core.Base.ViewModel
{
    /// <summary>
    /// 任务进度报告器。
    /// </summary>
    public class DefaultSingleTaskReporter : SingleTaskReporterBase
    {
        #region Constructors

        /// <summary>
        /// 初始化类型 ProgressReporter 实例。
        /// </summary>
        public DefaultSingleTaskReporter()
        {
        }

        /// <summary>
        /// 初始化类型 ProgressReporter 实例。
        /// </summary>
        /// <param name="taskId">任务Id。</param>
        public DefaultSingleTaskReporter(String taskId)
            : base(taskId)
        {

        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// 报告开始。
        /// </summary>
        ///<param name="message">消息。</param>
        public override void Start(String message = null)
        {
            if (State != TaskState.Idle) return;
            State = TaskState.Starting;
            Progress = 0;
            OnProgressChanged(new TaskProgressEventArgs(Id, 0, message));
        }

        /// <summary>
        /// 报告进度。
        /// </summary>
        /// <param name="progress">进度。</param>
        /// <param name="message">消息。</param>
        public override void ChangeProgress(Double progress, String message = null)
        {
            if (((Int32)State & 0xFF00) == 0x0100)
            {
                State = TaskState.Running;
                Progress = progress;
                OnProgressChanged(new TaskProgressEventArgs(Id, progress, message));
            }
        }

        /// <summary>
        /// 报告完成。
        /// </summary>
        /// <param name="message">消息。</param>
        public override void Finish(String message = null)
        {
            if (State != TaskState.Running) return;
            State = TaskState.Completed;
            Progress = 100;
            OnTerminate(new TaskTerminateEventArgs(Id, true, message));
        }

        /// <summary>
        /// 报告停止。
        /// </summary>
        /// <param name="message">消息。</param>
        public override void Stop(String message = null)
        {
            if (((Int32)State & 0xFF00) == 0x0100)
            {
                State = TaskState.Stopping;
                OnTerminate(new TaskTerminateEventArgs(Id, false, message));
            }
        }

        /// <summary>
        /// 报告失败。
        /// </summary>
        /// <param name="ex">异常信息。</param>
        /// <param name="message">消息。</param>
        public override void Defeat(Exception ex, String message = null)
        {
            if (State != TaskState.Running) return;
            State = TaskState.Failed;
            OnTerminate(new TaskTerminateEventArgs(Id, ex, message));
        }

        /// <summary>
        /// 重置。
        /// </summary>
        public override void Reset()
        {
            //枚举值高字节如果为0x02，表示结束
            if (((Int32)State & 0xFF00) != 0x0200)
            {
                return;
            }
            State = TaskState.Idle;
            Progress = 0;
        }

        #endregion

        #endregion
    }
}
