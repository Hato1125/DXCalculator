using Windows.UI;
using DxLibDLL;
using Windows.Graphics;

namespace DXCalculator;

internal class AppMain
{
    public static TitleBar titleBar = new();
    public static Windows.UI.Color BackColor;

    public void Running()
    {
        Init();
        Loop();
        End();
    }

    private void Init()
    {
        DX.SetOutApplicationLogValidFlag(DX.FALSE);
        DX.ChangeWindowMode(DX.TRUE);
        DX.SetGraphMode(900, 1600, 32);
        DX.SetWindowSize(400, 800);
        DX.SetWindowSizeChangeEnableFlag(DX.TRUE, DX.FALSE);
        DX.DxLib_Init();
        DX.SetDrawScreen(DX.DX_SCREEN_BACK);
        titleBar.InitTitleBar();
        BackColor = Windows.UI.Color.FromArgb(255, 30, 30, 35);
        titleBar.SetTitleBarColor(BackColor);
        titleBar._mainWindow.ResizeClient(new SizeInt32(400, 800));
        DX.SetBackgroundColor(BackColor.R, BackColor.G, BackColor.B);
    }

    private void Loop()
    {
        while (DX.ProcessMessage() != -1)
        {
            DX.ClearDrawScreen();



            DX.ScreenFlip();
        }
    }

    private void End()
    {
        DX.DxLib_End();
    }
}
