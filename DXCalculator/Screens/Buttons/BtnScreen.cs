using DxLibDLL;
using DXCalculator.GUI;
using ABI.Microsoft.UI.Xaml.Controls;

namespace DXCalculator;

internal class BtnScreen
{
    private const float Padding = 5;
    private GUI.Panel? panel = null;
    private GUI.Button[]? button = null;
    private string[] texts = { "7", "8", "9", "4", "5", "6", "1", "2", "3" };

    public void Init()
    {
        panel = new(400, 200);
        button = new GUI.Button[9];
        for (var i = 0; i < 9; i++)
        {
            button[i] = new(90, 100, "Yu Gothic UI", 29, 6);
            button[i].BackColor = Color.FromArgb(39, 39, 44);
            button[i].EdgeColor = Color.FromArgb(45, 45, 50);
            button[i].ForeColor = Color.White;
            button[i].Text = texts[i];
            panel.ChildControl?.Add(button[i]);
        }
        panel.Opacity = 0;
    }

    public void Update()
    {
        if (panel == null) return;

        DX.GetWindowSize(out int width, out int height);
        panel.Build(width, height / 2);

        int cheight = -1;
        for (var i = 0; i < panel.ChildControl?.Count(); i++)
        {
            if (i % 3 == 0 || i % 6 == 0)
                cheight++;

            int w = (int)(3 * panel.ChildControl[0].Size.Width + (3 * Padding));

            panel.ChildControl[i].Position = (
                (int)(i * panel.ChildControl[i].Size.Width + (i * Padding)) - cheight * w + (int)(Padding * 2),
                (int)(cheight * (panel.ChildControl[i].Size.Height + Padding))
            );

            panel.ChildControl[i].Build(
                (int)((width / 3)) - (int)Padding * 2,
                100
            );
        }

        panel.Position = (0, height / 2);
        panel.Update(0, 0);
    }

    public void Draw()
    {
        panel.Draw();
        //DX.GetWindowSize(out int width, out int height);
        //DX.DrawString(0, 0, $"X {width} Y {height}", 0xffffff);
        //DX.DrawString(0, 20, $"X {panel.Size.Width} Y {panel.Size.Height}", 0xffffff);
    }

    public void Finish()
    {
        
    }
}
