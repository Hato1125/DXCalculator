using DxLibDLL;
using DXCalculator.GUI;
using ABI.Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace DXCalculator;

internal class BtnScreen
{
    private const int Padding = 1;

    private GUI.Panel? panel = null;
    public Dictionary<string, GUI.Button>? Buttons = null;
    private string[] texts =
    {
        "0", "1", "2", "3", "4", "5",
        "6", "7", "8", "9", "+", "-",
        "x", "÷", "%", "=", ".", "c",
        "x²" ,"+/-"
    };

    public void Init()
    {
        const string fontName = "Yu Gothic UI";
        const int fontSize = 19;
        const int fontThick = 8;

        panel = new(400, 200);
        panel.Opacity = 0;

        Buttons = new()
        {
            { "0", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "1", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "2", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "3", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "4", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "5", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "6", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "7", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "8", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "9", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "+", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "-", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "x", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "/", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "%", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "=", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { ".", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "c", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "x2", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
            { "+/-", new GUI.Button(0, 0, fontName, fontSize, fontThick) },
        };

        int index = 0;
        foreach(var item in Buttons)
        {
            item.Value.BackColor = Color.FromArgb(39, 39, 46);
            item.Value.EdgeColor = Color.FromArgb(46, 46, 53);
            item.Value.ForeColor = Color.White;
            item.Value.Text = texts[index];

            index++;
        }

        // 並べる
        var childs = panel.ChildControl;
        childs.Add(Buttons["%"]);
        childs.Add(Buttons["c"]);
        childs.Add(Buttons["x2"]);
        childs.Add(Buttons["/"]);
        childs.Add(Buttons["7"]);
        childs.Add(Buttons["8"]);
        childs.Add(Buttons["9"]);
        childs.Add(Buttons["x"]);
        childs.Add(Buttons["4"]);
        childs.Add(Buttons["5"]);
        childs.Add(Buttons["6"]);
        childs.Add(Buttons["-"]);
        childs.Add(Buttons["1"]);
        childs.Add(Buttons["2"]);
        childs.Add(Buttons["3"]);
        childs.Add(Buttons["+"]);
        childs.Add(Buttons["+/-"]);
        childs.Add(Buttons["0"]);
        childs.Add(Buttons["."]);
        childs.Add(Buttons["="]);
    }

    public void Update()
    {
        if (panel == null)
            return;

        DX.GetWindowSize(out int width, out int height);
        int x = (width - panel.Size.Width) / 2;
        int y = height / 2 + Padding;

        panel.Position = (x, y);
        panel.Update(x, y);
        ReSize();
    }

    public void Draw()
    {
        if (panel == null)
            return;

        panel.Draw();
    }

    public void Finish()
    {
        
    }

    void ReSize()
    {
        if (panel == null)
            return;

        DX.GetWindowSize(out int width, out int height);
        panel?.Build(width - Padding * 2, height / 2 - Padding * 2);

        var childs = panel.ChildControl;
        int heightCount = -1;
        for (var i = 0; i < childs?.Count(); i++)
        {
            // NOTE: 1は誤差
            int cWidth = (panel.Size.Width / 4) - Padding + 1;
            int cHeight = (panel.Size.Height / 5) - Padding + 1;

            childs[i].Build(cWidth, cHeight);

            // 座標を計算する
            if(i % 4 == 0 || i % 8 == 0)
                heightCount++;

            int addX = ((cWidth * 4) + (4 * Padding)) * heightCount;
            int x = (cWidth * i) + (i * Padding) - addX;
            int y = (cHeight * heightCount) + (heightCount * Padding);

            childs[i].Position = (
                x, y
            );
        }
    }
}
