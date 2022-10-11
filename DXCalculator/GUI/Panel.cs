using DxLibDLL;

namespace DXCalculator.GUI;

internal class Panel : Control
{
    public float Radius { get; set; } = 0.0f;
    public Color BackColor { get; set; } = Color.Black;

    public Panel(int width, int height) : base(width, height)
    {
        Build(width, height);
    }

    public override void Update(int pointx, int pointy)
    {
        base.Update(pointx, pointy);
    }

    public override void Draw()
    {
        DrawAction = () =>
        {
            DrawPanel(Size.Width, Size.Height);
        };

        base.Draw();
    }

    private void DrawPanel(int width, int height)
    {
        uint color = DX.GetColor(BackColor.R, BackColor.G, BackColor.B);

        DX.DrawRoundRectAA(0, 0, width, height, Radius, Radius, byte.MaxValue, color, DX.TRUE);
    }

    /*
    private void CreateNoise(int noiseLevel, int width, int height)
    {
        _buttonHandle = DX.MakeARGB8ColorSoftImage(width, height);

        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                int rand = new Random().Next(0, noiseLevel);
                int r = rand == 2 ? BackColor.R - new Random().Next(0, noiseLevel) : BackColor.R + new Random().Next(0, noiseLevel);
                int g = rand == 2 ? BackColor.G - new Random().Next(0, noiseLevel) : BackColor.G + new Random().Next(0, noiseLevel);
                int b = rand == 2 ? BackColor.B - new Random().Next(0, noiseLevel) : BackColor.B + new Random().Next(0, noiseLevel);

                if (rand == 0 || rand == 1)
                    DX.DrawPixelSoftImage(
                        _buttonHandle,
                        x,
                        y,
                        BackColor.R > noiseLevel ? r : BackColor.R + new Random().Next(0, noiseLevel),
                        BackColor.G > noiseLevel ? g : BackColor.G + new Random().Next(0, noiseLevel),
                        BackColor.B > noiseLevel ? b : BackColor.B + new Random().Next(0, noiseLevel),
                        255
                        );
            }
    }
    */
}
