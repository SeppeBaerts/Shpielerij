using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wietse_Agenda_Tryout.Properties;

namespace Wietse_Agenda_Tryout
{
    internal static class SettingStatic
    {
        public static TextBox TxtNaam { get; set; }
        public static TextBox TxtExtraContent { get; set; }
        public static Label CurrentLable { get; set; }
        public static List<DragBox> Dragboxes { get; set; } = new List<DragBox>();
        public static Dictionary<string, DragBox> DragBoxesDictionary { get; set; } = new Dictionary<string, DragBox>();
        private static bool hasBeenCaught = false;
        private static string[] settings;
        private static Dictionary<string, string> settingsPair = new Dictionary<string, string>();
        private static int uids = 0;
        public static int Uid
        {
            get { return uids++; }
        }
        public static void SaveSetting()
        {
            foreach(KeyValuePair<string, string> pair in settingsPair)
            {
                using (StreamWriter sw = new StreamWriter("../../SettingsFile",false))
                {
                    sw.WriteLine($"{pair.Key};{pair.Value}\n");
                }
            }
        }
        /// <summary>
        /// returns null when setting cannot be found.
        /// </summary>
        public static object GetSetting(string value)
        {
            LoadSettings();
            if (value.StartsWith("::"))
            {
                return GetBoolSetting(value);
            }
            else if (value.StartsWith("--"))
            {
                return settingsPair[value];
            }
            else if (value.StartsWith(",,"))
            {
                return GetIntSetting(value);
            }
            else
                throw new ArgumentOutOfRangeException(@"Setting should start with ""::"", ""--"" or "",,"" ");

        }
        private static void LoadSettings()
        {
            settingsPair.Clear();
            using (StreamReader sr = new StreamReader("../../SettingsFile.txt"))
            {
                while (!sr.EndOfStream)
                {
                    settings = sr.ReadLine().Split(';');
                    settingsPair.Add(settings[0], settings[1]);
                }
            }
        }
        private static bool GetBoolSetting(string value)
        {
            return settingsPair[value].ToLower() == "true";

        }
        private static int GetIntSetting(string value)
        {
            if (int.TryParse(settingsPair[value], out int result))
                return result;
            else
                throw new ArgumentOutOfRangeException(@"Settings starting with "",,"" must have a parseble seccond value");
        }

    }
}
