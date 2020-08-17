using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using Microsoft.Win32;

namespace RegisterXP
{
    class Program
    {
        static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to register! "+msg);
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            if (!File.Exists("GoodbloxClient.exe")) { Error("Unable to find GoodbloxClient.exe"); }
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator)) { Error("Please run RegisterXP.exe with elevated privilges"); }
            }
            string classRoot = "HKEY_CLASSES_ROOT\\goodblox";
            string currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start("GoodbloxClient.exe", "/regserver");
            Registry.ClassesRoot.CreateSubKey("goodblox\\Shell\\open\\command");
            Registry.SetValue(classRoot, "", "URL:GoodBlox Protocol");
            Registry.SetValue(classRoot, "URL Protocol", "");
            Registry.SetValue(classRoot + "\\Shell\\open\\command", "", currentFolder+"LauncherXP.exe %1");
        }
    }
}
