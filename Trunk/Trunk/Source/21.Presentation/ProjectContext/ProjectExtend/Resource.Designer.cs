﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XLY.SF.Project.Extension {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("XLY.SF.Project.Extension.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;LanguageResource&gt;
        ///  &lt;ViewLanguage Prompt=&quot;界面显示语言&quot;&gt;
        ///    &lt;!--登录、加载--&gt;
        ///    &lt;View_Loading&gt;系统加载中，请稍等&lt;/View_Loading&gt;
        ///    &lt;View_Login&gt;登录&lt;/View_Login&gt;
        ///    &lt;View_Login_ExitSys&gt;退出&lt;/View_Login_ExitSys&gt;
        ///    &lt;View_Login_UserName&gt;帐  号&lt;/View_Login_UserName&gt;
        ///    &lt;View_Login_Password&gt;密  码&lt;/View_Login_Password&gt;
        ///    &lt;View_Login_Company&gt;效率源信息安全股份有限公司&lt;/View_Login_Company&gt;
        ///    &lt;View_Login_Version&gt;版本 1.0&lt;/View_Login_Version&gt;
        ///
        ///    &lt;!--主页--&gt;
        ///    &lt;View_MainWinTitle&gt;SPF9139智能手机数据恢 [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string Language_Cn {
            get {
                return ResourceManager.GetString("Language_Cn", resourceCulture);
            }
        }
    }
}
