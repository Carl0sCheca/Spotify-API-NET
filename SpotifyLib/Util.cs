using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace SpotifyLib
{
    public class Util
    {
        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        public static string Base64Encode(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string Base64Decode(string data)
        {
            var base64EncodedBytes = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        // TODO: cambiar esto mas tarde
        public static string readClientId()
        {
            string[] lines = File.ReadAllLines("key.txt");
            //foreach (string line in lines)
            //{
            //    Console.WriteLine(line);
            //}
            return lines[0];
        }
    }
}
