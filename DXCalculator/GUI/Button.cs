using DxLibDLL;

namespace DXCalculator.GUI;

internal class Button : Control
{
    private int _fontHandle;
    private const int radius = 4;
    private const float edgeSize = 1.2f;
    private double opacityCounter;
    private double opacity;
    private double clickOpacityCounter;
    private double clickOpacity;

    public string Text { get; set; } = string.Empty;
    public Color BackColor { get; set; } = Color.Empty;
    public Color EdgeColor { get; set; } = Color.Empty;
    public Color ForeColor { get; set; } = Color.Empty;

    public Button(int width, int height, string fontName, int fontSize, int fontThick, bool isEdge = false, int edgeSize = 0)
        : base(width, height)
    {
        Build(width, height, fontName, fontSize, fontThick, isEdge, edgeSize);
    }

    public void Build(int width, int height, string fontName, int fontSize, int fontThick, bool isEdge = false, int edgeSize = 0)
    {
        Build(width, height);

        if (_fontHandle != 0 || _fontHandle != -1)
            DX.DeleteFontToHandle(_fontHandle);

        if (isEdge)
            _fontHandle = DX.CreateFontToHandle(fontName, fontSize, fontThick, DX.DX_FONTTYPE_ANTIALIASING_EDGE_4X4, edgeSize);
        else
            _fontHandle = DX.CreateFontToHandle(fontName, fontSize, fontThick, DX.DX_FONTTYPE_ANTIALIASING_4X4);
    }

    public override void Update(int pointx, int pointy)
    {
        base.Update(pointx, pointy);

        ClickAnime();
        Opacity = 255 - (int)clickOpacity;
    }

    public override void Draw()
    {
        DrawAction = () =>
        {
            DrawButton(Size.Width, Size.Height);
            
        };

        DrawOpacityAction = () =>
        {
            DrawText(Text);
            DrawEdge(Size.Width, Size.Height);
        };

        base.Draw();
    }

    private void ClickAnime()
    {
        if (IsHovering())
        {
            opacityCounter += AppMain.time.TotalSeconds * 1600;

            if (opacityCounter > 90)
                opacityCounter = 90;
        }
        else
        {
            opacityCounter -= AppMain.time.TotalSeconds * (1600 / 2);

            if (opacityCounter < 0)
                opacityCounter = 0;
        }

        opacity = Math.Sin(opacityCounter * Math.PI / 180) * 3.5;

        if (IsClicking())
        {
            clickOpacityCounter += AppMain.time.TotalSeconds * 1100;

            if (clickOpacityCounter > 90)
                clickOpacityCounter = 90;
        }
        else
        {
            clickOpacityCounter -= AppMain.time.TotalSeconds * 1100;

            if (clickOpacityCounter < 0)
                clickOpacityCounter = 0;
        }

        clickOpacity = Math.Sin(clickOpacityCounter * Math.PI / 180) * 45;
    }

    private void DrawButton(int width, int height)
    {
        uint color = DX.GetColor(BackColor.R, BackColor.G, BackColor.B);
        uint clickColor = DX.GetColor(255, 255, 255);

        if (opacity < 255)
            DX.DrawRoundRectAA(edgeSize, edgeSize, width - edgeSize, height - edgeSize, radius - 1, radius - 1, byte.MaxValue, color, DX.TRUE);

        if (opacity > 0)
        {
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, (int)opacity);
            DX.DrawRoundRectAA(edgeSize, edgeSize, width - edgeSize, height - edgeSize, radius - 1, radius - 1, byte.MaxValue, clickColor, DX.TRUE);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
        }
    }

    private void DrawEdge(int width, int height)
    {
        uint edgeColor = DX.GetColor(EdgeColor.R, EdgeColor.G, EdgeColor.B);
        DX.DrawRoundRectAA(edgeSize, edgeSize, width - edgeSize, height - 1.5f, radius + 1.5f, radius + edgeSize, byte.MaxValue, edgeColor, DX.FALSE, edgeSize);
    }

    private void DrawText(string text)
    {
        if (Text == string.Empty || Text == "") return;

        int width = DX.GetDrawStringWidthToHandle(Text, Text.Length, _fontHandle);
        int height = DX.GetFontSizeToHandle(_fontHandle);

        DX.DrawStringFToHandle(
            (Size.Width - width) / 2,
            (Size.Height - height) / 2,
            Text,
            DX.GetColor(ForeColor.R - (int)(clickOpacity * 1.1f), ForeColor.G - (int)(clickOpacity * 1.1f), ForeColor.B - (int)(clickOpacity * 1.1f)),
            _fontHandle
        );
    }
}