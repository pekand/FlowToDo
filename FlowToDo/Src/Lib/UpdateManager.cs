using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FlowToDo
{

    public class UpdateManager
    {
        private static readonly HttpClient http = new HttpClient();

        public static string getAppVersion()
        {
            string version = "unknown";
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("FlowToDo.version.txt"))
            using (StreamReader reader = new StreamReader(stream))
            {
                version = reader.ReadToEnd().Trim();
            }

            return version;
        }

        public static async Task CheckAndInstallUpdateAsync(string updateXmlUrl, IWin32Window owner = null)
        {
            try
            {
                var updateInfo = await GetUpdateInfoAsync(updateXmlUrl);
                if (updateInfo == null)
                {                    
                    return;
                }

                var currentVersion = ReadLocalVersion();
                if (IsNewer(updateInfo.Version, currentVersion))
                {
                    var resp = MessageBox.Show(owner, $"New version available: {updateInfo.Version}\nCurrent: {currentVersion}\nDo you want to download and install?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp != DialogResult.Yes) return;

                    string tempFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(new Uri(updateInfo.InstallerUrl).LocalPath));
                    await DownloadFileAsync(updateInfo.InstallerUrl, tempFile);

                    string sha = await ComputeFileSha256Async(tempFile);
                    if (!string.Equals(sha, updateInfo.Sha256, StringComparison.OrdinalIgnoreCase))
                    {
                        FileDeleteSafe(tempFile);                        
                        return;
                    }

                    try
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = tempFile,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                    catch (Exception ex)
                    {
                        FileDeleteSafe(tempFile);
                        return;
                    }
                }                
            }
            catch (Exception ex)
            {
                
            }
        }

        private static async Task<UpdateInfo> GetUpdateInfoAsync(string url)
        {
            var s = await http.GetStringAsync(url);
            var doc = XDocument.Parse(s);
            var root = doc.Root;
            if (root == null) return null;
            var version = root.Element("version")?.Value?.Trim();
            var installer = root.Element("installerUrl")?.Value?.Trim();
            var sha = root.Element("sha256")?.Value?.Trim();
            if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(installer) || string.IsNullOrEmpty(sha)) return null;
            return new UpdateInfo { Version = version, InstallerUrl = installer, Sha256 = sha };
        }

        private static string ReadLocalVersion()
        {
            try
            {                
                return UpdateManager.getAppVersion();
            }
            catch
            {
                return "0.0.0";
            }
        }

        private static bool IsNewer(string remoteVersion, string localVersion)
        {
            if (Version.TryParse(remoteVersion, out var rv) && Version.TryParse(localVersion, out var lv))
                return rv > lv;
            return !string.Equals(remoteVersion, localVersion, StringComparison.OrdinalIgnoreCase);
        }

        private static async Task DownloadFileAsync(string url, string destinationPath)
        {
            using var resp = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            resp.EnsureSuccessStatusCode();
            using var fs = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await resp.Content.CopyToAsync(fs);
        }

        private static async Task<string> ComputeFileSha256Async(string path)
        {
            using var stream = File.OpenRead(path);
            using var sha = SHA256.Create();
            var hash = await sha.ComputeHashAsync(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private class UpdateInfo
        {
            public string Version { get; set; }
            public string InstallerUrl { get; set; }
            public string Sha256 { get; set; }
        }

        private static void FileDeleteSafe(string path)
        {
            try { if (File.Exists(path)) File.Delete(path); }
            catch { }
        }
    }
}
