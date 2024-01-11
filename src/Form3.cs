using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading;
using System.Management;
using System.IO;
using Microsoft.Win32;
using KeyboardHookMain;
using System.Security.Permissions;
//This project is using JSON.NET
namespace TCPCommander //need to make login form (form1), form for keybinding (form3), form for attempts and at what time 
{
    public partial class Form3 : Form
    {
        public static bool isLocked = false;

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

        [DllImport("user32.dll")]
        public static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);

        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern bool BlockInput(bool fBlockIt);

        protected override void WndProc(ref Message m)
        {
            const int WM_QUERYENDSESSION = 0x0011;
            const int WM_ENDSESSION = 0x0016;

            if (m.Msg == WM_QUERYENDSESSION || m.Msg == WM_ENDSESSION)
                return;

            base.WndProc(ref m);
        }

        public Form3()
        {
            InitializeComponent();
        }

        Form[] z;
        private void Form1_Load(object sender, EventArgs e)
        {

            //BlockInput(true);
            AutoScaleMode = AutoScaleMode.Font;
            WindowState = FormWindowState.Maximized;
            notifyIcon1.Icon = SystemIcons.Shield;

            SystemEvents.DisplaySettingsChanging += SystemEvents_DisplaySettingsChanging;
        }

        int screenCount;
        private void SystemEvents_DisplaySettingsChanging(object sender, EventArgs e)
        {
            if (isLocked)
            {
                screenCount = Screen.AllScreens.Length;

                for (int a = 0; a < z.Length; a++)
                {
                    if (z[a] != null)
                    {
                        z[a].Close();
                        z[a].Dispose();
                    }
                }
                z = new Form[screenCount - 1];
                for (int a = 0; a < z.Length; a++)
                {
                    z[a] = new Form();
                    z[a].FormBorderStyle = FormBorderStyle.None;
                    z[a].BackColor = Color.Black;
                    z[a].Location = Screen.AllScreens[a + 1].WorkingArea.Location;

                    z[a].Show();

                    Rectangle bounds = Screen.AllScreens[a + 1].Bounds;
                    z[a].SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                }
            }
        }

        private void MenuItem4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Timer1_Tick(object sender, EventArgs e) 
        {
        
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x201 || m.Msg == 0x202 || m.Msg == 0x203) return true;
            if (m.Msg == 0x204 || m.Msg == 0x205 || m.Msg == 0x206) return true;
            return false;
        }

        public void LockPC()
        {
            this.Show();
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS); 

            Cursor.Hide();



            KeyboardHookMain.KeyboardHook.EngageFullKeyboardLockdown();
            //mmf.DisableMouse();

            StopShutdown("(Shutdown Disabled)\nReason: Lock Exploitation Protection");
            
            this.Opacity = 1;

            notifyIcon1.Visible = true; 
            notifyIcon1.BalloonTipTitle = ("Computer Lock");
            notifyIcon1.BalloonTipText = ("Your computer has been locked.");
            notifyIcon1.ShowBalloonTip(5000);
            isLocked = true;

            screenCount = Screen.AllScreens.Length;
            z = new Form[screenCount - 1];
            if (screenCount > 1)
            {
                for (int a = 0; a < z.Length; a++)
                {
                    Debug.WriteLine(Application.OpenForms.Count);

                    z[a] = new Form();
                    z[a].FormBorderStyle = FormBorderStyle.None;
                    z[a].BackColor = Color.Black;
                    z[a].Location = Screen.AllScreens[a + 1].WorkingArea.Location;

                    z[a].Show();

                    Rectangle bounds = Screen.AllScreens[a + 1].Bounds;
                    z[a].SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                }
            }
        }

        public void UnlockPC()
        {
            this.Hide();
            SetWindowPos(this.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            Cursor.Show();
            ResetShutdown();
            KeyboardHookMain.KeyboardHook.ReleaseFullKeyboardLockdown();
            //mmf.EnableMouse();

            this.Opacity = 0.0;

            notifyIcon1.BalloonTipTitle = "Computer Lock";
            notifyIcon1.BalloonTipText = "Your computer has been unlocked.";
            notifyIcon1.ShowBalloonTip(5000);
            isLocked = false;

            for (int a = 0; a < z.Length; a++)
            {
                z[a].Close();
                z[a].Dispose();
            }

        }
        
        private void StopShutdown(string strMsg)
        {
            try
            {
                if (ShutdownBlockReasonCreate(this.Handle, strMsg))
                {
                    Debug.WriteLine("Shutdown aborted.");
                }
                else
                {
                    Debug.WriteLine("Shutdown couldn't be aborted.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("StopShutdown Error: " + e.Message + " " + e.StackTrace);
            }
        }

        private void ResetShutdown()
        {
            try
            {
                if (ShutdownBlockReasonDestroy(this.Handle))
                {
                    Debug.WriteLine("Shutdown Reset");
                }
                else
                {
                    Debug.WriteLine("Shutdown couldn't be reset");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ResetShutdown Error: " + e.Message + " " + e.StackTrace);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.White); /
            p.Width = 5;
            Graphics g = e.Graphics;
            g.DrawRectangle(p, new Rectangle(label1.Location.X - 5, label1.Location.Y - 5, label1.Width + 10, label1.Height + 10));
            g.DrawRectangle(p, new Rectangle(label2.Location.X - 5, label2.Location.Y - 5, label2.Width + 10, label2.Height + 10));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.BalloonTipClosed += (s, args) =>
            {
                var thisIcon = (NotifyIcon)sender;
                thisIcon.Visible = false;
                thisIcon.Dispose();
                SystemEvents.DisplaySettingsChanging -= SystemEvents_DisplaySettingsChanging;
            };
        }
    }
}
