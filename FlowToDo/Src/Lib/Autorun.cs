using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlowToDo
{
    public class Autorun
    {
        static string GetAppName()
        {
            return Program.appName;
        }

        static string GetExePath()
        {
            return $"\"{Process.GetCurrentProcess().MainModule.FileName}\"";
        }

        public static bool AddCurrentAppToAutorun()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key == null) return false;
                key.SetValue(GetAppName(), GetExePath());
                return true;
            }
            catch { return false; }
        }

        public static bool RemoveCurrentAppFromAutorun()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key == null) return false;
                if (Array.IndexOf(key.GetValueNames(), GetAppName()) >= 0)
                {
                    key.DeleteValue(GetAppName());
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public static bool IsCurrentAppInAutorun()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", false);
                if (key == null) return false;
                var val = key.GetValue(GetAppName()) as string;
                if (string.IsNullOrEmpty(val)) return false;
                var exe = GetExePath().Trim('"');
                var stored = val.Trim('"');
                return string.Equals(Path.GetFullPath(stored), Path.GetFullPath(exe), StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
        }
    }
}
