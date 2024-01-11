using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using Open.Nat;
using WinForms = System.Windows.Forms;

namespace TCPCommander
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WinForms.Timer ScreenshotTimer = new WinForms.Timer();

        IPEndPoint TCP_ipEP;
        public static TcpClient TCP_client;
        public static TcpListener TCP_listener;

        IPEndPoint UDP_ipEP;
        public static UdpClient UDP_client;

        public static string IPstring;
        
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int X, int Y);


        ImageCodecInfo jpgEncoder;
        System.Drawing.Imaging.Encoder encoder;
        EncoderParameters encoderparams;
        EncoderParameter encoderparam;

        Form3 f3;
        private void Form1_Load(object sender, EventArgs e)
        {

            StartButton.Enabled = false;
            IPLabel.Text = "";
            TCP_PortLabel.Text = "Port: (Not Selected)";

            f2 = new Form2();
            f3 = new Form3();

            f2.Hide();
            f2.Text = "Server Status: Offline";
            f2.IPLabel.Text = "Server Offline";
            f2.LogTextBox.Location = new Point((this.Width) - (f2.LogTextBox.Size.Width / 2), f2.LogTextBox.Location.Y);
            f2.IPLabel.Location = new Point((this.Width) - (f2.IPLabel.Size.Width / 2), f2.IPLabel.Location.Y);
            f2.SessionTimeLabel.Location = new Point((this.Width) - (f2.IPLabel.Size.Width / 2), f2.SessionTimeLabel.Location.Y);

            f2.SessionTimeLabel.Text = "00:00:00";

            f2.LogTextBox.ReadOnly = true;
            f2.LogTextBox.BackColor = System.Drawing.SystemColors.Window;


            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            f2.MinimumSize = f2.Size;
            f2.MaximumSize = f2.Size;
            f3.Opacity = 0;
            f3.Show();
            f3.Hide();


            Initialize();
            timer1.Interval = 1000;
            timer1.Enabled = true;



            jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            encoder = System.Drawing.Imaging.Encoder.Quality;
            encoderparams = new EncoderParameters(1);
            encoderparam = new EncoderParameter(encoder, 100L);

            encoderparams.Param[0] = encoderparam;
        }

        bool TCP_clientIsOpen;
        bool UDP_clientIsOpen;
        byte[] buffer = new byte[1];

        int SessionTime = 0;
        bool ServerStatusChanged;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (StartServer)
            {
                SessionTime++;
            }
            else if (SessionTime > 0)
            {
                LogAdd("Session Ended @ " + DateTime.Now.ToString("MM/dd/yy") + " - " + DateTime.Now.ToString("HH:mm:ss"));
                LogAdd("Session Lasted: " + Hour + " Hours : " + Minute + " Minutes : " + Second + " Seconds");
                SessionTime = 0;
            }

            if (!(ServerStatusChanged == StartServer && !StartServer))
            {
                f2.SessionTimeLabel.Text = GetSessionTime();
            }

            ServerStatusChanged = StartServer;

            if (TCP_client != null)
            {
                if (TCP_clientIsOpen)
                {
                    if (TCP_client.Client.Poll(0, SelectMode.SelectRead))
                    {
                        try
                        {

                            if (TCP_client.Client.Receive(buffer, SocketFlags.Peek) == 0)
                            {
                                ResetServer(StartServer);
                            }
                        } catch (Exception ex)
                        {
                            Debug.WriteLine("EXCEPTION OCCURED: Type: " + ex.GetType().ToString() + "Message: " + ex.Message);
                            ResetServer(StartServer);
                            LogAdd("Exception Thrown: " + ex.GetType() + ":" + ex.Message);
                        }
                    }
                }
            }
            
        }

        public async void PortForward(bool forward, bool alert = false)
        {
            if (!forward)
            {
                if (alert)
                    MessageBox.Show("Alert: TCPCommander Server currently has UPnP disabled, so you cannot connect from outside the network", "TCPCommander Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            try
            {
                var discoverer = new NatDiscoverer();
                var device = await discoverer.DiscoverDeviceAsync();
                await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, int.Parse(TCP_PortTextBox.Text), int.Parse(TCP_PortTextBox.Text), "TCPCommander Server Map"));//"Inner Router"));

                var sb = new StringBuilder();
                var ip = await device.GetExternalIPAsync();

                sb.AppendFormat("\n+------+-------------------------------+--------------------------------+------------------------------------+-------------------------+");
                sb.AppendFormat("\n| PROT | PUBLIC (Reacheable)           | PRIVATE (Your computer)        | Description                        |                         |");
                sb.AppendFormat("\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
                sb.AppendFormat("\n|      | IP Address           | Port   | IP Address            | Port   |                                    | Expires                 |");
                sb.AppendFormat("\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
                foreach (var mapping in await device.GetAllMappingsAsync())
                {
                    sb.AppendFormat("\n|  {5} | {0,-20} | {1,6} | {2,-21} | {3,6} | {4,-35}|{6,25}|",
                        ip, mapping.PublicPort, mapping.PrivateIP, mapping.PrivatePort, mapping.Description, mapping.Protocol == Protocol.Tcp ? "TCP" : "UDP", mapping.Expiration.ToLocalTime());
                }
                sb.AppendFormat("\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
                Console.WriteLine(sb.ToString());
            }
            catch (MappingException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        public void ResetServer(bool ServerIsStarted)
        {
            if (ServerIsStarted)
            {
               TCP_listener.Stop();

               TCP_client.Close();
               TCP_clientIsOpen = false;

                UDP_client.Close();
                UDP_clientIsOpen = false;

                Console.WriteLine(@"  
===================================================  
Client has been detected as being disconnected!
===================================================");
                worker.WorkerSupportsCancellation = true;
                worker.CancelAsync();

                worker.Dispose(); 
                LogAdd("Client Disconnected");
                LogAdd("Restarting Server");
            }
            else
            {
               TCP_listener.Stop();
                
                try
                { 
                    if (TCP_client != null)
                    {
                       TCP_client.GetStream().Close();
                       TCP_client.Close();
                       TCP_client = null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception Thrown: " + ex.GetType() + ":" + ex.Message);
                    LogAdd("Exception Thrown: " + ex.GetType() + ":" + ex.Message);
                }

               TCP_clientIsOpen = false;
                Console.WriteLine(@"  
==========================================================  
Server has been shutdown by the user and is now offline!
==========================================================");
                worker.WorkerSupportsCancellation = true;
                worker.CancelAsync();


                worker.DoWork -= ListenToClient;
                worker.RunWorkerCompleted -= Worker_RunWorkerCompleted;

                worker.Dispose(); 
                LogAdd("Server Shut Down By Host");
                LogAdd("Worker Temporarily Deactivated");
            }
        }

        BackgroundWorker worker;
        public /*async*/ void Initialize()
        {
            try
            {
                IPstring = GetLocalNetworkIPV4();
                IPLabel.Text = "IP: " + IPstring;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Occured: " + ex.GetType().ToString() + " : " + ex.Message);
                LogAdd("Exception Thrown: " + ex.GetType() + ":" + ex.Message);
        }



        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            worker.DoWork -= ListenToClient;
            worker.RunWorkerCompleted -= Worker_RunWorkerCompleted;
            worker.Dispose();
            Debug.WriteLine("The Worker has stopped");
            LogAdd("Worker Stopping And Restarting - Connection Temporarily Disabled");
            worker = new BackgroundWorker();
            worker.DoWork += ListenToClient;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        bool PCLocked = false;
        public void ListenToClient(object sender, DoWorkEventArgs e)
        {
            LogAdd("Listener Started - Connection Enabled");
            try
            {
               TCP_listener.Start();
                Console.WriteLine(@"  
===================================================  
Started listening requests at: {0}:{1}  
===================================================",
                TCP_ipEP.Address, TCP_ipEP.Port);
                TCP_client = TCP_listener.AcceptTcpClient();
                TCP_clientIsOpen = true;

                

                Console.WriteLine("Connected toTCP_client!" + " \n");


                Console.WriteLine("Client: " + (TCP_client.Client.RemoteEndPoint as IPEndPoint).Address + ":" + (TCP_client.Client.RemoteEndPoint as IPEndPoint).Port);
                LogAdd("Client: " + (TCP_client.Client.RemoteEndPoint as IPEndPoint).Address + ":" + (TCP_client.Client.RemoteEndPoint as IPEndPoint).Port + " Connected to the Server");
            }
            catch (Exception ex)
            {
                if (ex.Message == "A blocking operation was interrupted by a call to WSACancelBlockingCall")
                {
                    Debug.WriteLine("Alert: An exception has occured that is thrown when waiting for theTCP_client to accept is stopped by the server.\nThis error is normal and shouldn't impact performance, but is possible to fix with another method");
                    Debug.WriteLine("Same Thing sortof goes for the read in the whileTCP_listener");
                }

                Debug.WriteLine(ex.Message);
                LogAdd("Exception Thrown: " + ex.GetType() + ":" + ex.Message);
            }

            while (TCP_client.Connected)
            {
                try
                {
                    int bytesize = 1024;
                    byte[] buffer = new byte[bytesize];
                    string networkRead =TCP_client.GetStream().Read(buffer, 0, bytesize).ToString();
                    string data = ASCIIEncoding.ASCII.GetString(buffer);
                    Debug.WriteLine(data);

                    LogAdd("Incoming Data: " + data);

                    if (data.ToUpper().Contains("{TEST}"))
                    {
                        sendData(getBytes("{TEST_RESPOND}"),TCP_client.GetStream());
                        Console.WriteLine("DETECTED \"TEST\"");
                    }
                    else if (data.ToUpper().Contains("{SHUTDOWN}"))
                    {
                        Shutdown();
                        LogAdd("Shutting Down");
                    }
                    else if (data.ToUpper().Contains("{MONITOR_ON}"))
                    {
                        Debug.WriteLine("the monitor has been turned on");

                        SetMonitorState(MonitorState.ON);

                        mouse_event(MOUSEEVENTF_MOVE, 0, 0, 0, UIntPtr.Zero);

                        sendData(getBytes("{MONITOR_TURNED_ON}"),TCP_client.GetStream());
                        LogAdd("Turning Monitor ON");
                    }
                    else if (data.ToUpper().Contains("{MONITOR_OFF}"))
                    {
                        Debug.WriteLine("the monitor has been turned off");

                        SetMonitorState(MonitorState.OFF);

                        sendData(getBytes("{MONITOR_TURNED_OFF}"),TCP_client.GetStream());

                        LogAdd("Turning Mointor OFF");
                    }
                    else if (data.ToUpper().Contains("{TAKE_SCREENSHOT}"))
                    {
                        ScreenshotTimer.Interval = 1000;
                        ScreenshotTimer.Tick += SendScreenshotTick;

                    }
                    else if (data.ToString().Contains("{SPEED_TEST}") && data.ToString().Contains("{/SPEED_TEST}"))
                    {
                        //sendData(new byte[1024 * 1024 * 5],TCP_client.GetStream());
                        Console.WriteLine("Speed Test: Size = " + data.Length);
                        LogAdd("Completing Speed Test");
                    }
                    else if (data.ToString().Contains("{LOCK_PC}"))
                    {

                        if (PCLocked)
                        {
                            f3.Invoke(new Action(() => f3.UnlockPC()));
                        }
                        else
                        {
                            f3.Invoke(new Action(() => f3.LockPC()));
                        }

                        PCLocked = !PCLocked;

                        sendData(getBytes("{PC_LOCKED}"),TCP_client.GetStream());
                        LogAdd("PC Locked Set -> " + PCLocked);
                    }
                    else if (data.ToString().Contains("{PC_LOCK_STATUS}"))
                    {
                        sendData(getBytes("{PC_LOCK_STATUS}" + PCLocked.ToString().ToLower() + "{/PC_LOCK_STATUS}"),TCP_client.GetStream());
                        LogAdd("Getting & Sending PC Lock Status");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception WHILE Listening: " + ex.Message);
                   TCP_client.GetStream().Close(); 
                   TCP_client.Close();
                   TCP_clientIsOpen = false;
                    LogAdd("Exception Thrown: " + ex.GetType() + ":" + ex.Message);
                }
            }
        }

        public void SendScreenshotTick(object sender, EventArgs e)
        {
            
            Debug.WriteLine("a screenshot was taken");

            var bitmap = SaveScreenshotWithMousePointer();
            bitmap = new Bitmap(bitmap, new Size(1280, 720));

            var stream = new MemoryStream();
            bitmap.Save(stream, jpgEncoder, encoderparams);
            Debug.WriteLine("Getting stream size: " + stream.Length);

            sendData(stream.ToArray(),TCP_client.GetStream());

            bitmap.Dispose();
            LogAdd("Taking & Sending Screenshot");
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        void Shutdown()
        {
            sendData(getBytes("{BEGAN_SHUTDOWN}"),TCP_client.GetStream());
            ProcessStartInfo psi = new ProcessStartInfo("shutdown", "/s /f /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        #region Screenshot Capture
        public static class User32
        {
            public const Int32 CURSOR_SHOWING = 0x00000001;

            [StructLayout(LayoutKind.Sequential)]
            public struct ICONINFO
            {
                public bool fIcon;
                public Int32 xHotspot;
                public Int32 yHotspot;
                public IntPtr hbmMask;
                public IntPtr hbmColor;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public Int32 x;
                public Int32 y;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct CURSORINFO
            {
                public Int32 cbSize;
                public Int32 flags;
                public IntPtr hCursor;
                public POINT ptScreenPos;
            }

            [DllImport("user32.dll")]
            public static extern bool GetCursorInfo(out CURSORINFO pci);

            [DllImport("user32.dll")]
            public static extern IntPtr CopyIcon(IntPtr hIcon);

            [DllImport("user32.dll")]
            public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

            [DllImport("user32.dll")]
            public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);
        }

        Bitmap SaveScreenshotWithMousePointer()
        {
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            // Create a graphics object from the bitmap.  
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right  
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            User32.CURSORINFO cursorInfo;
            cursorInfo.cbSize = Marshal.SizeOf(typeof(User32.CURSORINFO));

            if (User32.GetCursorInfo(out cursorInfo))
            {
                // if the cursor is showing draw it on the screen shot
                if (cursorInfo.flags == User32.CURSOR_SHOWING)
                {
                    // we need to get hotspot so we can draw the cursor in the correct position
                    var iconPointer = User32.CopyIcon(cursorInfo.hCursor);
                    User32.ICONINFO iconInfo;
                    int iconX, iconY;

                    if (User32.GetIconInfo(iconPointer, out iconInfo))
                    {
                        // calculate the correct position of the cursor
                        iconX = cursorInfo.ptScreenPos.x - ((int)iconInfo.xHotspot);
                        iconY = cursorInfo.ptScreenPos.y - ((int)iconInfo.yHotspot);

                        // draw the cursor icon on top of the captured screen image
                        User32.DrawIcon(gfxScreenshot.GetHdc(), iconX, iconY, cursorInfo.hCursor);

                        // release the handle created by call to g.GetHdc()
                        gfxScreenshot.ReleaseHdc();
                    }
                }
            }

            return bmpScreenshot;
        }

        public Bitmap SaveScreenshot()
        {
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            // Create a graphics object from the bitmap.  
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right  
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return bmpScreenshot;
        }
        #endregion

        [DllImport("user32.dll")]
        static extern void mouse_event(Int32 dwFlags, Int32 dx, Int32 dy, Int32 dwData, UIntPtr dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;

        #region Monitor Command
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private int SC_MONITORPOWER = 0xF170;
        private int WM_SYSCOMMAND = 0x0112;

        public enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }

        public void SetMonitorState(MonitorState state)
        {
           this.Invoke(new Action(() => SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, (int)state)));
        }
        #endregion

        byte[] getBytes(string input)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            return bytes;
        }

        void sendData(byte[] data, NetworkStream stream)
        {
            int bufferSize = 1024;
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            stream.Write(dataLength, 0, 4); //send to the other side the size of data!
            int bytesSent = 0;
            int bytesLeft = data.Length;
            while (bytesLeft > 0)
            {
                int curDataSize = Math.Min(bufferSize, bytesLeft);
                stream.Write(data, bytesSent, curDataSize);
                bytesSent += curDataSize;
                bytesLeft -= curDataSize;
            }
        }

        void sendScreenshot(byte[] data, NetworkStream stream)
        {
            int bufferSize = (1024 * 1024) / 2;
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            stream.Write(dataLength, 0, 4); //send to the other side the size of data!
            int bytesSent = 0;
            int bytesLeft = data.Length;
            while (bytesLeft > 0)
            {
                int curDataSize = Math.Min(bufferSize, bytesLeft);
                stream.Write(data, bytesSent, curDataSize);
                bytesSent += curDataSize;
                bytesLeft -= curDataSize;
            }
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public static string GetLocalNetworkIPV4()
        {
            string localIP = "";
            bool OpenPort = false;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

                for (int i = 60000; i < 65535; i++)
                {
                    if (OpenPort)
                    {
                        Debug.WriteLine("Working Port Found");
                        break;
                    }

                    foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
                    {
                        if (tcpi.LocalEndPoint.Port == i)
                        {
                            Debug.WriteLine(i + " Is In Use");
                            break;
                        }
                        else
                        {
                            OpenPort = true;
                            socket.Connect("8.8.8.8", i);
                            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                            localIP = endPoint.Address.ToString();
                            break;
                        }
                    }

                }
            }

            return localIP;
        }

        Form2 f2;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (TCP_client != null)
                {
                   TCP_client.GetStream().Close();
                   TCP_client.Close();
                }

                if (UDP_client != null)
                {
                    UDP_client.Close();
                }
            }
            catch (Exception) { }

            if (TCP_listener != null)
            {
                if (TCP_listener.Server.IsBound)
                {
                   TCP_listener.Stop();
                }
            }
        }

        bool StartServer = false;
        private async void StartButton_Click(object sender, EventArgs e)
        {
            StartServer = !StartServer;

            if (StartServer)
            {
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

                foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
                {
                    if (tcpi.LocalEndPoint.Port == int.Parse(TCP_PortTextBox.Text))
                    {
                        MessageBox.Show("Could not open port \"" + TCP_PortTextBox.Text + "\" as it is already in use!", "Error Opening Port", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        StartServer = false;
                        return;
                    } else if (tcpi.LocalEndPoint.Port == int.Parse(UDP_PortTextBox.Text))
                    {
                        MessageBox.Show("Could not open port \"" + TCP_PortTextBox.Text + "\" as it is already in use!", "Error Opening Port", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        StartServer = false;
                        return;
                    }
                }

                TCP_PortTextBox.ReadOnly = true;
                TCP_PortTextBox.BackColor = System.Drawing.SystemColors.Window;

                StartButton.Text = "Stop Server";

                PortForward(false, true);

                f2.Text = "Server Status: Online";
                f2.IPLabel.Text = "Connect: " + IPstring + ", " + TCP_PortTextBox.Text + ", " + UDP_PortTextBox.Text;
                f2.SessionTimeLabel.Text = GetSessionTime();
                LogAdd("Session Started @ " + DateTime.Now.ToString("MM/dd/yy") + " - " + DateTime.Now.ToString("HH:mm:ss"));

                TCP_ipEP = new IPEndPoint(IPAddress.Parse(IPstring), int.Parse(TCP_PortTextBox.Text)); //allow a way to set the port in the future
                TCP_listener = new TcpListener(TCP_ipEP);

                UDP_ipEP = new IPEndPoint(IPAddress.Parse(IPstring), int.Parse(UDP_PortTextBox.Text));
                UDP_client = new UdpClient(UDP_ipEP);

                worker = new BackgroundWorker();
                worker.DoWork += ListenToClient;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
                TCP_PortLabel.Text = TCP_PortTextBox.Text;

                //await Task.Run(() => SetUpPortMap());
            }
            else
            {
                TCP_PortTextBox.ReadOnly = false;
                StartButton.Text = "Start Server";
                TCP_PortLabel.Text = "Port: (Not Selected)";
                f2.Text = "Server Status: Offline";
                f2.IPLabel.Text = "Server Offline";
                f2.SessionTimeLabel.Text = "00:00:00";
                ResetServer(StartServer);
            }
        }

        private void TCP_PortTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TCP_PortTextBox.TextLength >= 5 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)8))
            {
                e.Handled = true;
            }
        }

        private void TCP_PortTextBox_KeyUp(object sender, EventArgs e)
        {

            if (TCP_PortTextBox.TextLength > 0 && UDP_PortTextBox.TextLength > 0)
            {
                if (int.Parse(TCP_PortTextBox.Text) > 0 && int.Parse(TCP_PortTextBox.Text) < 65536 && int.Parse(UDP_PortTextBox.Text) > 0 && int.Parse(UDP_PortTextBox.Text) < 65536)
                {
                    StartButton.Enabled = false;
                }
                else
                    StartButton.Enabled = true;
            }
            else
                StartButton.Enabled = false;
        }

        private void UDP_PortTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (UDP_PortTextBox.TextLength >= 5 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)8))
            {
                e.Handled = true;
            }
        }

        private void UDP_PortTextBox_KeyUp(object sender, EventArgs e)
        {
            
            if (TCP_PortTextBox.TextLength > 0 && UDP_PortTextBox.TextLength > 0)
            {
                if (int.Parse(TCP_PortTextBox.Text) > 0 && int.Parse(TCP_PortTextBox.Text) < 65536 && int.Parse(UDP_PortTextBox.Text) > 0 && int.Parse(UDP_PortTextBox.Text) < 65536)
                {
                    StartButton.Enabled = true;
                }
                else
                    StartButton.Enabled = false;
            }
            else
                StartButton.Enabled = false;
        }


        public static bool DebugButtonEnabled = false;
        private void DebugButton_Click(object sender, EventArgs e)
        {
            DebugButtonEnabled = !DebugButtonEnabled;
            if (DebugButtonEnabled)
            {
                f2.BringToFront();
                f2.Show();
            }
            else
            {
                f2.Hide();
            }
        }

        int Hour;
        int Minute;
        int Second;
        public string GetSessionTime()
        {
            Hour = SessionTime / 3600;
            Minute = (SessionTime % 3600) / 60;
            Second = SessionTime % 60;
            return String.Format("{0:D2}:{1:D2}:{2:D2}", Hour, Minute, Second);
        }

        public void LogAdd(string input)
        {
            if (f2.LogTextBox.InvokeRequired)
            {
                f2.LogTextBox.Invoke(new Action(() => {
                    f2.LogTextBox.Text += DateTime.Now.ToString("[MM/dd - HH:mm:ss]") + " - " + input + "\r\n\r\n";
                    f2.LogTextBox.SelectionStart = f2.LogTextBox.TextLength;
                    f2.LogTextBox.ScrollToCaret();
                }));
            }
            else
            {
                f2.LogTextBox.Text += DateTime.Now.ToString("[MM/dd - HH:mm:ss]") + " - " + input + "\r\n\r\n";
                f2.LogTextBox.SelectionStart = f2.LogTextBox.TextLength;
                f2.LogTextBox.ScrollToCaret();
            }
            //f2.LogTextBox.ScrollToCaret();
        }
    }
}
