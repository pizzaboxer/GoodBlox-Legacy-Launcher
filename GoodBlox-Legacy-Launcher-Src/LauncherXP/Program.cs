using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;
using RobloxLib;

namespace LauncherXP 
{
    class Program
    {
        static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Environment.Exit(0);
        }

        static void Join(string JoinScriptUrl)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Starting GoodBlox...");
            App robloxapp = new App();
            Workspace workspace = robloxapp.CreateGame("44340105256");
            try { workspace.ExecUrlScript(JoinScriptUrl, "", "", "", ""); }
            catch (Exception e) { Error(e.Message); }
        }

        static void Main(string[] args)
        {
            Console.Title = "GoodBlox Launcher [Legacy]";

            if (args.Length == 0) { Error("Invalid command line!"); }
            string token = args[0].Substring(11);
            if (token.EndsWith("/")) { token = token.Remove(token.Length - 1); }

            if (args.Length > 1 && args[1] == "/debug") //skips token verification
            {
                Join("http://goodblox.xyz/goodblox/api/scripts/games/join/" + token);
            }
            else
            {
                WebClient client = new WebClient();
                string response = client.DownloadString("http://goodblox.xyz/goodblox/api/scripts/games/verifyToken.php?token=" + token);

                //json was not a thing in 2005 so time 2 parse

                string response_code = response.Substring(8, 3);
                string response_jointoken = response.Remove(response.Length - 2).Substring(21);

                if (response_code != "200")
                {
                    string response_message = response.Remove(response.Length - 2).Substring(23);
                    Error(response_message);
                }

                Join("http://goodblox.xyz/goodblox/api/scripts/games/join/" + response_jointoken);
            }
        }
    }
}
