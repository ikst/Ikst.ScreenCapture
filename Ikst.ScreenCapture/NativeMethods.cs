using System;
using System.Runtime.InteropServices;

namespace Ikst.ScreenCapture
{
    internal class NativeMethods
    {

        [StructLayout(LayoutKind.Sequential)]
        internal struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct POINTAPI
        {
            public int x;
            public int y;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct ICONINFO
        {
            public bool fIcon;
            public Int32 xHotspot;
            public Int32 yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.Dll")]
        internal static extern IntPtr GetDC(IntPtr hwnd);


        [DllImport("gdi32.dll")]
        internal static extern bool DeleteDC(IntPtr handle);


        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);


        [DllImport("gdi32.dll")]
        internal static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);


        [DllImport("user32.dll")]
        internal static extern bool GetCursorInfo(out CURSORINFO pci);


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);


        [DllImport("user32.dll")]
        internal static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);


    }
}
