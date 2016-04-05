// This is not the full program, but rather a library of static helper functions.
// To test the code... I guess you could save it as a class .cs file and use the
// mouse functions at the bottom of the Helper class or the dll functions
// near the top.

using System;
using System.Runtime.InteropServices;

namespace Helper
{

    public static class Helpers
    {

        [DllImport("gdi32.dll")]
        public static extern int GetPixel(
        IntPtr hDC,
        int x,
        int y);

        [DllImport("User32.dll")]
        public static extern int SendInput(
        uint nInputs,
        INPUT[] pInputs,
        int cbSize);

        [DllImport("user32.dll")]
        private static extern int SendMessage(
        IntPtr hWnd,
        uint Msg,
        long wParam,
        long lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(
        int hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(
        string lpClassName,
        string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool CloseWindow(
        IntPtr iHandle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(
        long dwFlags,
        long dx,
        long dy,
        long cButtons,
        long dwExtraInfo);

        public enum InputType : int
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        };

        public enum MouseFlags : uint
        {
            Move = 0x0001, // MOUSEEVENTF_MOVE - Specifies that movement occurred.
            LeftDown = 0x0002, // MOUSEEVENTF_LEFTDOWN - Specifies that the left button was pressed.
            LeftUp = 0x0004, // MOUSEEVENTF_LEFTUP - Specifies that the left button was released.
            RightDown = 0x0008, // MOUSEEVENTF_RIGHTDOWN - Specifies that the right button was pressed.
            RightUp = 0x0010, // MOUSEEVENTF_RIGHTUP - Specifies that the right button was released.
            Absolute = 0x8000, // MOUSEEVENTF_ABSOLUTE - Specifies that the dx and dy members contain normalized absolute coordinates. If the flag is not set, dxand dy contain relative data (the change in position since the last reported position). This flag can be set, or not set, regardless of what kind of mouse or other pointing device, if any, is connected to the system. For further information about relative mouse motion, see the following Remarks section.
            Wheel = 0x0080, // MOUSEEVENTF_WHEEL Windows NT/2000/XP: Specifies that the wheel was moved, if the mouse has a wheel. The amount of movement is specified in mouseData. 
            MiddleDown = 0x0020, // MOUSEEVENTF_MIDDLEDOWN Specifies that the middle button was pressed.
            MiddleUp = 0x0040, // MOUSEEVENTF_MIDDLEUP Specifies that the middle button was released.
            VirtualDesk = 0x4000, //MOUSEEVENTF_VIRTUALDESK Windows 2000/XP:Maps coordinates to the entire desktop. Must be used with MOUSEEVENTF_ABSOLUTE.
            XDown = 0x0080, // MOUSEEVENTF_XDOWN Windows 2000/XP: Specifies that an X button was pressed.
            XUp = 0x0100, // MOUSEEVENTF_XUP Windows 2000/XP: Specifies that an X button was released.
            HWheel = 0x1000 // MOUSEEVENTF_HWHEEL Windows Vista: Specifies that the wheel was moved horizontally, if the mouse has a wheel. The amount of movement is specified in mouseData. 
        };

        public enum VK : ushort
        {
            SHIFT = 0x10,
            CONTROL = 0x11,
            MENU = 0x12,
            ESCAPE = 0x1B,
            BACK = 0x08,
            TAB = 0x09,
            RETURN = 0x0D,
            PRIOR = 0x21,
            NEXT = 0x22,
            END = 0x23,
            HOME = 0x24,
            LEFT = 0x25,
            UP = 0x26,
            RIGHT = 0x27,
            DOWN = 0x28,
            SELECT = 0x29,
            PRINT = 0x2A,
            EXECUTE = 0x2B,
            SNAPSHOT = 0x2C,
            INSERT = 0x2D,
            DELETE = 0x2E,
            HELP = 0x2F,
            NUMPAD0 = 0x60,
            NUMPAD1 = 0x61,
            NUMPAD2 = 0x62,
            NUMPAD3 = 0x63,
            NUMPAD4 = 0x64,
            NUMPAD5 = 0x65,
            NUMPAD6 = 0x66,
            NUMPAD7 = 0x67,
            NUMPAD8 = 0x68,
            NUMPAD9 = 0x69,
            MULTIPLY = 0x6A,
            ADD = 0x6B,
            SEPARATOR = 0x6C,
            SUBTRACT = 0x6D,
            DECIMAL = 0x6E,
            DIVIDE = 0x6F,
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            OEM_1 = 0xBA, // ',:' for US
            OEM_PLUS = 0xBB, // '+' any country
            OEM_COMMA = 0xBC, // ',' any country
            OEM_MINUS = 0xBD, // '-' any country
            OEM_PERIOD = 0xBE, // '.' any country
            OEM_2 = 0xBF, // '/?' for US
            OEM_3 = 0xC0, // '`~' for US
            MEDIA_NEXT_TRACK = 0xB0,
            MEDIA_PREV_TRACK = 0xB1,
            MEDIA_STOP = 0xB2,
            MEDIA_PLAY_PAUSE = 0xB3,
            LWIN = 0x5B,
            RWIN = 0x5C
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx; // 0 - 65535
            public int dy; // 0 - 65535
            public int mouseData; // if dwFlags = MOUSEEVENTF_WHEEL or MOUSE_EVENTF_HWHEEL, then mouseData specifies the amount of wheel movement. +/- multiples of WHEEL_DELTA which is 120.
            public MouseFlags dwFlags;
            public uint time; // Time stamp for the event, in milliseconds. If this parameter is 0, the system will provide its own time stamp.
            public IntPtr dwExtraInfo; // Specifies an additional value associated with the mouse event. An application calls GetMessageExtraInfo to obtain this extra information.

            public MOUSEINPUT(MouseFlags flags)
            {
                dx = 0;
                dy = 0;
                mouseData = 0;
                time = 0;
                dwExtraInfo = GetMessageExtraInfo();
                dwFlags = flags;
            }

            public MOUSEINPUT(int dx, int dy, MouseFlags flags)
            {
                this.dx = dx;
                this.dy = dy;
                mouseData = 0;
                time = 0;
                dwExtraInfo = GetMessageExtraInfo();
                dwFlags = flags;
            }

            public MOUSEINPUT(int mouseScroll)
            {
                this.dx = 0;
                this.dy = 0;
                mouseData = 120 * mouseScroll; // WHEEL_DELTA = 120
                time = 0;
                dwExtraInfo = GetMessageExtraInfo();
                dwFlags = MouseFlags.Wheel;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public VK wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public InputType type; // INPUT_MOUSE, INPUT_KEYBOARD, INPUT_HARDWARE
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;

            public INPUT(MOUSEINPUT mi)
            {
                this.type = InputType.Mouse;
                ki = new KEYBDINPUT();
                hi = new HARDWAREINPUT();
                this.mi = mi;
            }

            public INPUT(KEYBDINPUT ki)
            {
                this.type = InputType.Keyboard;
                mi = new MOUSEINPUT();
                hi = new HARDWAREINPUT();
                this.ki = ki;
            }

            public INPUT(HARDWAREINPUT hi)
            {
                this.type = InputType.Hardware;
                mi = new MOUSEINPUT();
                ki = new KEYBDINPUT();
                this.hi = hi;
            }
        }

        public static class Mouse
        {
            public static void Move(IntPtr iHandle, int mx, int my)
            {
                INPUT[] i = new INPUT[1];
                i[0] = new INPUT(new MOUSEINPUT(mx, my, MouseFlags.Move | MouseFlags.Absolute));
                SetForegroundWindow(iHandle.ToInt32() | 0x01);
                SendInput(1, i, Marshal.SizeOf(i));
            }

            public static void Move(int mx, int my)
            {
                INPUT[] i = new INPUT[1];
                i[0] = new INPUT(new MOUSEINPUT(mx, my, MouseFlags.Move | MouseFlags.Absolute));
                SendInput(1, i, Marshal.SizeOf(i));
            }

            public static void LeftClick(IntPtr iHandle, int mx, int my)
            {
                INPUT[] i = new INPUT[3];
                i[0] = new INPUT(new MOUSEINPUT(mx, my, MouseFlags.Move | MouseFlags.Absolute));
                i[1] = new INPUT(new MOUSEINPUT(MouseFlags.LeftDown));
                i[2] = new INPUT(new MOUSEINPUT(MouseFlags.LeftUp));
                SendInput(3, i, Marshal.SizeOf(i[0]));
            }

            public static void Wheel( int size)
            {
                INPUT[] i = new INPUT[1];
                i[0] = new INPUT(new MOUSEINPUT(size));
                
            }
        }

    }
}
