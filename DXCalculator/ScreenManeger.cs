using DxLibDLL;

namespace DXCalculator;

internal class ScreenManeger
{
    public BtnScreen BtnScreen = new();
    public Calculation calculation = new();
    public NumPanel numPanel = new();

    public void Init()
    {
        BtnScreen.Init();
        calculation.Init();
        numPanel.Init();
    }

    public void Update()
    {
        BtnScreen.Update();
        calculation.Calculating();
        numPanel.Update();
    }

    public void Draw()
    {
        BtnScreen.Draw();
        numPanel.Draw();
    }

    public void Finish()
    {
        BtnScreen.Finish();
        numPanel.Finish();
    }
}
