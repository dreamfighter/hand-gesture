using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;

namespace Motion_Detection_v2
{
    public class MouseAction
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        private const int SB_VERT = 0x1;
        private const int VK_MENU = 0x12;
        private const uint KEYEVENTF_KEYUP = 0x2;
        private int scrollPos;
        private int currentScrollPos = 0;
        private bool altPressed = false;
        private Point cursor = new Point();
        private Point preCursor = new Point();
        private Point currentCursor = new Point();
        private Point centerCursor = new Point();
        private Size canvas = new Size(320, 240);
        private String mouseEvent = "";
        private Timer mouseTimer = new Timer();

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, Int32 dwData, IntPtr dwExtraInfo);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(int hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        public MouseAction()
        {
            mouseTimer.Tick+=new EventHandler(mouseTimer_Tick);
            mouseTimer.Interval = 30;
        }
        public MouseAction(Size canvas)
        {
            mouseTimer.Tick += new EventHandler(mouseTimer_Tick);
            mouseTimer.Interval = 30;
            this.canvas = canvas;
        }

        public void mouseTimerEnable()
        {
            mouseTimer.Enabled = true;
        }

        public void mouseTimerDisable()
        {
            mouseTimer.Enabled = false;
        }

        public void setAction(string cmd) {
            mouseEvent = cmd;
        }

        public void action()
        {

            GetCursorPos(out currentCursor);
            centerCursor = cursor;

            switch (mouseEvent)
            {
                case "A"://left click
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "B"://right click
                    {
                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "C"://double click 
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        System.Threading.Thread.Sleep(100);
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "D"://drop
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "E":
                    mouse_event(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                    break;
                case "F":
                    //SendKeys.Send("+{TAB}");//shift+tab
                    SendKeys.Send("{LEFT}");
                    Console.WriteLine("LEFT");
                    break;
                case "G"://scrolldown
                    //SendKeys.Send("{TAB}");//tab
                    SendKeys.Send("{RIGHT}");
                    Console.WriteLine("RIGHT");
                    break;
                case "H"://scrollup;
                    //scrollPos = 60;
                    //scrollDown();
                    Console.WriteLine("UP");
                    SendKeys.Send("{DOWN}");
                    break;
                case "I":// ALT Keydown
                    //scrollPos = 60;
                    //scrollUp();
                    Console.WriteLine("DOWN");
                    SendKeys.Send("{UP}");
                    break;
                case "J":
                    /*
                    if (!altPressed)
                        keybd_event((byte)VK_MENU, 0, 0, 0);
                    else
                        keybd_event((byte)VK_MENU, 0, KEYEVENTF_KEYUP, 0);
                    altPressed = !altPressed;
                     * */
                    break;
            }
        }

        public void action(String cmd)
        {
            GetCursorPos(out currentCursor);
            centerCursor = cursor;

            switch (cmd)
            {
                case "A"://left click
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "B"://right click
                    {
                        mouse_event(MOUSEEVENTF_RIGHTDOWN| MOUSEEVENTF_RIGHTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "C"://double click 
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        System.Threading.Thread.Sleep(100);
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "D"://drop
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "E":
                    mouse_event(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                    break;
                case "F":
                    SendKeys.Send("{LEFT}");
                    //SendKeys.Send("+{TAB}");//shift+tab
                    break;
                case "G"://scrolldown
                    SendKeys.Send("{RIGHT}");
                    //SendKeys.Send("{TAB}");//tab
                    break;
                case "H"://scrollup;
                    //scrollPos = 60;
                    //scrollUp();
                    SendKeys.Send("{DOWN}");
                    break;
                case "I":// ALT Keydown
                    //scrollPos = 60;
                    //scrollDown();
                    SendKeys.Send("{UP}");
                    break;
                case "J":
                    /*
                    if (!altPressed)
                        keybd_event((byte)VK_MENU, 0, 0, 0);
                    else
                        keybd_event((byte)VK_MENU, 0, KEYEVENTF_KEYUP, 0);
                    altPressed = !altPressed;
                     * */
                    break;
            }
        }

        public void scrollDown()
        {
            if (scrollPos - currentScrollPos > 0)
            {
                int y = (scrollPos - currentScrollPos) / 10;
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -y, new System.IntPtr());
                currentScrollPos++;
                scrollDown();
            }
            currentScrollPos = 0;
        }

        public void scrollUp()
        {
            if (scrollPos - currentScrollPos > 0)
            {
                int y = (scrollPos - currentScrollPos) / 20;
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, y, new System.IntPtr());
                currentScrollPos++;
                scrollUp();
            }
            currentScrollPos = 0;
        }

        public void setCursor(Point p)
        {
            cursor = p;
            /*
            try
            {
                cursor.X = currentCursor.X + cursor.X - centerCursor.X;
                cursor.Y = currentCursor.Y + cursor.Y - centerCursor.Y;

            }
            catch (Exception ex)
            {
                centerCursor = cursor;
            }

            GetCursorPos(out preCursor);

            int x = preCursor.X + (int)(1.0 * (cursor.X - preCursor.X) / 3);
            int y = preCursor.Y + (int)(1.0 * (cursor.Y - preCursor.Y) / 3);
            if (Math.Abs(cursor.X - preCursor.X) > 20)
                SetCursorPos(x, y);

            if (Math.Abs(cursor.Y - preCursor.Y) > 10)
                SetCursorPos(cursor.X, cursor.Y);
            preCursor = p;
            * */ 
        }

        private void mouseTimer_Tick(object sender, EventArgs e)
        {
            /*
            int centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            int centerY = Screen.PrimaryScreen.Bounds.Height / 2;
            Point cur = new Point();
            GetCursorPos(out cur);

            if (Math.Abs(centerX - cursor.X) > 50)
            {
                cur.X = cur.X + (cursor.X - centerX) / 20;
            } 
            if (Math.Abs(centerY - cursor.Y) > 50)
            {
                cur.Y = cur.Y + (cursor.Y - centerY) / 20;
            }
            SetCursorPos(cur.X, cur.Y);
            */

            int centerX = canvas.Width / 2;
            int centerY = canvas.Height / 2;
            Point cur = new Point();
            GetCursorPos(out cur);

            if (Math.Abs(centerX - cursor.X) > 40)
            {
                cur.X = cur.X + (cursor.X - centerX) / 7;
            }
            if (Math.Abs(centerY - cursor.Y) > 30)
            {
                cur.Y = cur.Y + (cursor.Y - centerY) / 7;
            }
            SetCursorPos(cur.X, cur.Y);
        }
    }
}
