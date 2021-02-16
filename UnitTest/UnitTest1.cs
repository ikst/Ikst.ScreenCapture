using System;
using Xunit;
using Ikst.ScreenCapture;
using System.Drawing;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var r1 = ScreenCapture.Capture(100, 100, 100, 200);
            Assert.Equal(100, r1.Width);
            Assert.Equal(200, r1.Height);

            var r2 = ScreenCapture.Capture(50, 50, 50, 60, true);
            Assert.Equal(50, r2.Width);
            Assert.Equal(60, r2.Height);

            Rectangle rect1 = new Rectangle(10, 20, 30, 40);
            Rectangle rect2 = new Rectangle(50, 60, 70, 80);

            var r3 = ScreenCapture.Capture(rect1);
            Assert.Equal(30, r3.Width);
            Assert.Equal(40, r3.Height);

            var r4 = ScreenCapture.Capture(rect2, true);
            Assert.Equal(70, r4.Width);
            Assert.Equal(80, r4.Height);
        }

        [Fact]
        public void Test2()
        {
            Assert.ThrowsAny<ArgumentException>(() =>  ScreenCapture.Capture(0, 0, 0, 0));
            Assert.ThrowsAny<ArgumentException>(() =>  ScreenCapture.Capture(100, 100, 0, 100));
            Assert.ThrowsAny<ArgumentException>(() => ScreenCapture.Capture(100, 100, 100, 0));
        }

        [Fact]
        public void Test3()
        {
            // –ÚŽ‹Šm”F—p
            Bitmap bmp = ScreenCapture.Capture(0, 0, 1920, 1080, true);
            bmp.Save("UnitTestResult.bmp");
        }
    }
}
