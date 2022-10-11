using static DxLibDLL.DX;

namespace DXCalculator;

/// <summary>
/// InputType
/// </summary>
public enum JoyPadType
{
    Key_Pad = DX_INPUT_KEY_PAD1,
    Pad1 = DX_INPUT_PAD1,
    Pad2 = DX_INPUT_PAD2,
    Pad3 = DX_INPUT_PAD3,
    Pad4 = DX_INPUT_PAD4,
    Key = DX_INPUT_KEY
}