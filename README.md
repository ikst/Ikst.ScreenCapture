# Ikst.ScreenCapture
This is a screen capture using Win32API.  
The mouse cursor can be included in the capture.  

## usage
Static method.  
The last argument specifies whether to include the mouse cursor.
```c#
Bitmap bmp = ScreenCapture.Capture(0, 0, 1920, 1080, true);
```

## nuget
https://www.nuget.org/packages/Ikst.ScreenCapture/
