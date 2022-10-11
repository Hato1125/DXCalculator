using DxLibDLL;

namespace DXCalculator;

internal class ScreenManeger
{
    private BtnScreen BtnScreen = new();

    public void Init()
    {
        BtnScreen.Init();
    }

    public void Update()
    {
        BtnScreen.Update();
    }

    public void Draw()
    {
        BtnScreen.Draw();
    }

    public void Finish()
    {
        BtnScreen.Finish();
    }
}
