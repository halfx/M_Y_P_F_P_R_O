﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using XLY.SF.Framework.Language;

namespace XLY.SF.Project.DataExtraction.Language
{
    internal static class LanguageHelper
    {
        static LanguageHelper()
        {
            LanguageManager lm = LanguageManager.Current;
            LanguageProvider = new XmlDataProvider
            {
                XPath = "LanguageResource",
                Document = lm.Document
            };
            lm.Switched += (a, b) =>
            {
                LanguageProvider.Document = LanguageManager.Document;
            };
            LanguageManager = lm;
        }

        public static LanguageManager LanguageManager { get; }

        public static XmlDataProvider LanguageProvider { get; }
    }
}
