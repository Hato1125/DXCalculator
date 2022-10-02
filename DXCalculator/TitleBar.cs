using DxLibDLL;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using System.Drawing;
using System.Windows.Forms;

namespace DXCalculator;

internal class TitleBar
{
    private IntPtr _handle = DX.GetMainWindowHandle();
    public Microsoft.UI.Windowing.AppWindow? _mainWindow;

    public void InitTitleBar()
    {
        WindowId windowId = Win32Interop.GetWindowIdFromWindow(_handle);
        _mainWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
    }

    public void SetTitleBarColor(Windows.UI.Color color)
    {
        if (_mainWindow == null) return;

        _mainWindow.TitleBar.ForegroundColor = color;
        _mainWindow.TitleBar.BackgroundColor = color;

        _mainWindow.TitleBar.InactiveForegroundColor = color;
        _mainWindow.TitleBar.InactiveBackgroundColor = color;

        _mainWindow.TitleBar.ButtonBackgroundColor = color;
        _mainWindow.TitleBar.ButtonForegroundColor = Colors.White;

        _mainWindow.TitleBar.ButtonInactiveBackgroundColor = color;
        _mainWindow.TitleBar.ButtonInactiveForegroundColor = Colors.White;

        _mainWindow.TitleBar.ButtonHoverBackgroundColor = color;
        _mainWindow.TitleBar.ButtonHoverForegroundColor = Colors.White;

        _mainWindow.TitleBar.ButtonPressedBackgroundColor = color;
        _mainWindow.TitleBar.ButtonPressedForegroundColor = color;
    }
}
