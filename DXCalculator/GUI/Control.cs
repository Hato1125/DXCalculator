using DxLibDLL;

namespace DXCalculator.GUI;

internal class Control
{
    private int _screenHandle;
    private bool _isJudge;
    private Mouse _mouse;
    private (int PointX, int PointY) ControlPoint;

    /// <summary>
    /// 位置
    /// </summary>
    public (int X, int Y) Position { get; set; }

    /// <summary>
    /// サイズ
    /// </summary>
    public (int Width, int Height) Size { get; set; }
    
    /// <summary>
    /// 描画用コールバックアクション
    /// </summary>
    public Action? DrawAction { get; set; } = null;

    /// <summary>
    /// 子おコントロール
    /// </summary>
    public List<Control>? ChildControl { get; set; } = null;

    /// <summary>
    /// コントロールを初期化する
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">高さ</param>
    public Control(int width, int height)
    {
        _mouse = new();
        _isJudge = true;
        ChildControl = new();
        Build(width, height);
    }

    /// <summary>
    /// コントロールを生成する
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">高さ</param>
    public void Build(int width, int height)
    {
        if (_screenHandle != 0 || _screenHandle != -1)
            DX.DeleteGraph(_screenHandle);

        _screenHandle = DX.MakeScreen(width, height, DX.TRUE);
    }

    /// <summary>
    /// コントロールを更新する
    /// </summary>
    public virtual void Update()
    {
        _mouse.Update();

        if (ChildControl != null)
            foreach (var item in ChildControl)
            {
                // 子コントロールがホバーしていたら親は判定しない
                if (item.IsHovering())
                    _isJudge = false;
                else
                    _isJudge = true;
            }
    }

    /// <summary>
    /// コントロールを描画する
    /// </summary>
    public virtual void Draw()
    {
        var screen = DX.GetDrawScreen();

        DX.SetDrawScreen(_screenHandle);
        DX.ClearDrawScreen();

        if (DrawAction != null)
            DrawAction();

        if (ChildControl != null)
            foreach (var item in ChildControl)
                item.Draw();

        DX.SetDrawScreen(screen);
    }

    /// <summary>
    /// コントロールのレクタングルを取得する
    /// </summary>
    /// <returns>レクタングル</returns>
    public Rectangle GetRectangle() =>
        new Rectangle(0, 0, Size.Width, Size.Height);

    /// <summary>
    /// ホバーしているかを取得する
    /// </summary>
    /// <returns>ホバーしているか否か</returns>
    public bool IsHovering() =>
        _isJudge && GetRectangle().Contains(ControlPoint.PointX, ControlPoint.PointY);

    /// <summary>
    /// クリックしている間を取得する
    /// </summary>
    /// <returns>クリックしているか否か</returns>
    public bool IsClicking() =>
        IsHovering() && _mouse.GetKeyDown(MouseKey.Left);

    /// <summary>
    /// クリックした瞬間を取得する
    /// </summary>
    /// <returns>クリックしているか否か</returns>
    public bool IsClicked() =>
        IsHovering() && _mouse.GetKey(MouseKey.Left);

    /// <summary>
    /// クリックしていない瞬間を取得する
    /// </summary>
    /// <returns>クリックしていないか否か</returns>
    public bool IsClickUp() =>
        IsHovering() && _mouse.GetKeyUp(MouseKey.Left);
}