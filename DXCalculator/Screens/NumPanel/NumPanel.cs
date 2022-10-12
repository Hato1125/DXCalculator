using DXCalculator.GUI;
using DxLibDLL;

namespace DXCalculator;

internal class NumPanel
{
    private const int PaddingX = 30;
    private const int PaddingY = 80;

    private int _fontHandle;

    public void Init()
    {
        _fontHandle = DX.CreateFontToHandle("Yu Gothic UI", 68, 6, DX.DX_FONTTYPE_ANTIALIASING_4X4);
    }

    public void Update()
    {

    }

    public void Draw()
    {
        DX.DrawStringToHandle(PaddingX, PaddingY, AppMain.screenManeger.calculation.text.ToString(), 0xffffff, _fontHandle);
    }

    public void Finish()
    {

    }
}
