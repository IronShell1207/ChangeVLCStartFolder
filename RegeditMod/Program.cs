using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegeditMod
{
    class Program
    {
        string rootFolder = "HKEY_CLASSES_ROOT";
        static void Main(string[] args)
        {
            var sr = Registry.ClassesRoot.GetSubKeyNames();
            string[] comms = new string[] { "\\shell\\AddToPlaylistVLC\\command", "\\shell\\Open\\command", "\\shell\\PlayWithVLC\\command" };
            string[] values = new string[] { "\"C:\\Program Files\\VVLC\\vlc.exe\" --started-from-file --playlist-enqueue \" % 1\"",
                "\"C:\\Program Files\\VVLC\\vlc.exe\" --started-from-file \"%1\"",
                "\"C:\\Program Files\\VVLC\\vlc.exe\" --started-from-file --no-playlist-enqueue \"%1\""
            };

            string[] vlcss = new string[] { "VLC.mp4", "VLC.mov", "VLC.mkv", "VLC.avi", "VLC.wmv" };
            List<string> vlckeys = new List<string>() { };
            foreach (string s in sr)
            {
                foreach (string vl in vlcss)
                    if (s.Contains(vl))
                    {
                        Console.WriteLine(s);
                        vlckeys.Add(s);
                    }
            }
            foreach (string s in vlckeys)
            {
                for (int i = 0; i < comms.Length; i++)
                {
                    var k = Registry.ClassesRoot.OpenSubKey(s + comms[i], true);
                    k.SetValue("", values[i]);
                }
            }
            Console.ReadLine();

        }
    }
}
