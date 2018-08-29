using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace zSpaceWinApp.Processor
{
    public static class LocUtil
    {
        /// <summary>
        /// Get application name from an element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static string getAppName(FrameworkElement element)
        {
            var elType = element.GetType().ToString();
            var elNames = elType.Split('.');

            return elNames[0];
        }

        /// <summary>
        /// Generate a name from an element base on its class name
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static string getElementName(FrameworkElement element)
        {
            var elType = element.GetType().ToString();
            var elNames = elType.Split('.');

            var elName = "";
            if (elNames.Length >= 2)
                elName = elNames[elNames.Length - 1];

            return elName;
        }

        /// <summary>
        /// Get current culture info name base on previously saved setting if any,
        /// otherwise get from OS language
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetCurrentCultureName(FrameworkElement element)
        {
            RegistryKey curLocInfo = Registry.CurrentUser.OpenSubKey("zSpace" + @"\" + getAppName(element), false);

            var cultureName = CultureInfo.CurrentUICulture.Name; //Console.WriteLine(cultureName);
            if (curLocInfo != null)
            {
                //cultureName = curLocInfo.GetValue(getElementName(element) + ".localization", "en-US").ToString();
                cultureName = curLocInfo.GetValue("Resources.localization", "en-US").ToString();
            }

            return cultureName;
        }

        /// <summary>
        /// Set language based on previously save language setting,
        /// otherwise set to OS lanaguage
        /// </summary>
        /// <param name="element"></param>
        public static void SetDefaultLanguage(FrameworkElement element)
        {
            SetLanguageResourceDictionary(element, GetLocXAMLFilePath(GetCurrentCultureName(element)));
        }

        /// <summary>
        /// Dynamically load a Localization ResourceDictionary from a file
        /// </summary>
        public static void SwitchLanguage(FrameworkElement element, string inFiveCharLang)
        {
            /////// Get previously saved localization info
            //var elType = element.GetType().ToString();
            //var elNames = elType.Split('.');
            //var appName = elNames[0];
            //var elName = elNames[elNames.Length - 1];

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(inFiveCharLang);

            SetLanguageResourceDictionary(element, GetLocXAMLFilePath(inFiveCharLang));

            // Save new culture info to registry
            RegistryKey UserPrefs = Registry.CurrentUser.OpenSubKey("zSpace" + @"\" + getAppName(element), true);

            if (UserPrefs == null)
            {
                // Value does not already exist so create it
                RegistryKey newKey = Registry.CurrentUser.CreateSubKey("zSpace");
                UserPrefs = newKey.CreateSubKey(getAppName(element));
            }

            //UserPrefs.SetValue(getElementName(element) + ".localization", inFiveCharLang);
            UserPrefs.SetValue("Resources.localization", inFiveCharLang);
        }

        /// <summary>
        /// Returns the path to the ResourceDictionary file based on the language character string.
        /// </summary>
        /// <param name="inFiveCharLang"></param>
        /// <returns></returns>
        public static string GetLocXAMLFilePath(string inFiveCharLang)
        {
            string locXamlFile = "Resources." + inFiveCharLang + ".xaml";
            string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(directory, "Resources", locXamlFile);
        }

        /// <summary>
        /// Sets or replaces the ResourceDictionary by dynamically loading
        /// a Localization ResourceDictionary from the file path passed in.
        /// </summary>
        /// <param name="inFile"></param>
        private static void SetLanguageResourceDictionary(FrameworkElement element, String inFile)
        {
            if (File.Exists(inFile))
            {
                // Read in ResourceDictionary File
                var languageDictionary = new ResourceDictionary();
                languageDictionary.Source = new Uri(inFile);

                // Remove any previous Localization dictionaries loaded
                int langDictId = -1;
                for (int i = 0; i < element.Resources.MergedDictionaries.Count; i++)
                {
                    var md = element.Resources.MergedDictionaries[i];
                    //Console.WriteLine("MergedDictionaries[i] : " + md["ResourceDictionaryName"].ToString());

                    // Make sure your Localization ResourceDictionarys have the ResourceDictionaryName
                    // key and that it is set to a value starting with "Loc-".
                    if (md.Contains("ResourceDictionaryName"))
                    {
                        if (md["ResourceDictionaryName"].ToString().StartsWith("zSpace-"))
                        {
                            langDictId = i;
                            break;
                        }
                    }
                }
                if (langDictId == -1)
                {
                    // Add in newly loaded Resource Dictionary
                    element.Resources.MergedDictionaries.Add(languageDictionary);
                }
                else
                {
                    // Replace the current langage dictionary with the new one
                    element.Resources.MergedDictionaries[langDictId] = languageDictionary;
                }
            }
            else
            {
                MessageBox.Show("'" + inFile + "' not found.");
            }
        }

        /// <summary>
        /// Replace string into a file
        /// </summary>
        /// <param name="filename">full path</param>
        /// <param name="oldValue">the current value</param>
        /// <param name="newValue">the new value</param>
        public static void ReplaceString(string filename, string oldValue, string newValue)
        {
            StreamReader sr = new StreamReader(filename);
            string[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            StreamWriter sw = new StreamWriter(filename);
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains(oldValue))
                {
                    rows[i] = rows[i].Replace(oldValue, newValue);
                }
                sw.WriteLine(rows[i]);
            }
            sw.Close();
        }
    }
}
