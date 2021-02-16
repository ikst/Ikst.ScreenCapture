using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Ikst.ScreenCapture
{
    /// <summary>
    /// スクリーンキャプチャー
    /// </summary>
    public static class ScreenCapture
    {

        /// <summary></summary>
        private const Int32 CURSOR_SHOWING = 0x0001;

        /// <summary></summary>
        private const Int32 DI_NORMAL = 0x0003;


        /// <summary>
        /// スクリーンショットを取得します。
        /// </summary>
        /// <param name="rect">キャプチャ位置／サイズを格納したRectangle</param>
        /// <param name="includeMouse">スクリーンショットにマウスカーソルの画像を含めるかどうか</param>
        /// <returns>スクリーンショットのBitmap</returns>
        public static Bitmap Capture(Rectangle rect, bool includeMouse = false)
        {
            return Capture(rect.X, rect.Y, rect.Width, rect.Height, includeMouse);
        }

        /// <summary>
        /// スクリーンキャプチャを取得します。
        /// </summary>
        /// <param name="x">開始位置X</param>
        /// <param name="y">開始位置Y</param>
        /// <param name="width">横幅</param>
        /// <param name="height">縦幅</param>
        /// <param name="includeMouse">スクリーンショットにマウスカーソルの画像を含めるかどうか</param>
        /// <returns>スクリーンキャプチャのBitmap</returns>
        public static Bitmap Capture(int x, int y, int width, int height, bool includeMouse = false)
        {

            try
            {
                Bitmap result = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                using (Graphics g = Graphics.FromImage(result))
                {

                    // キャプチャ
                    IntPtr zero = IntPtr.Zero;
                    IntPtr hDestDC = IntPtr.Zero;
                    try
                    {

                        zero = NativeMethods.GetDC(IntPtr.Zero);
                        hDestDC = g.GetHdc();
                        NativeMethods.BitBlt(hDestDC, 0, 0, width, height, zero, x, y, 0x40cc0020);

                    }
                    finally
                    {
                        if (zero != IntPtr.Zero)
                        {
                            NativeMethods.DeleteDC(zero);
                        }
                        if (hDestDC != IntPtr.Zero)
                        {
                            g.ReleaseHdc(hDestDC);
                        }
                    }


                    // マウスポインタを重ねる
                    if (includeMouse)
                    {

                        NativeMethods.CURSORINFO pci;
                        NativeMethods.ICONINFO icoInfo;
                        IntPtr hdc = IntPtr.Zero;

                        try
                        {
                            pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.CURSORINFO));
                            if (NativeMethods.GetCursorInfo(out pci))
                            {
                                if (NativeMethods.GetIconInfo(pci.hCursor, out icoInfo))
                                {
                                    if (pci.flags == CURSOR_SHOWING)
                                    {
                                        hdc = g.GetHdc();
                                        NativeMethods.DrawIconEx(hdc, pci.ptScreenPos.x - x - icoInfo.xHotspot, pci.ptScreenPos.y - y - icoInfo.yHotspot, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                    }

                                    NativeMethods.DeleteObject(icoInfo.hbmColor);
                                    NativeMethods.DeleteObject(icoInfo.hbmMask);
                                }
                            }

                        }
                        finally
                        {
                            if (hdc != IntPtr.Zero)
                            {
                                g.ReleaseHdc(hdc);
                            }
                        }
                    }
                }

                return result;

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
