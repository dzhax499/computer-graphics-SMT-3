namespace Godot;

public static class ScreenUtils
{
    public static int ScreenWidth { get; private set; }
    public static int ScreenHeight { get; private set; }
    public static int MarginLeft { get; private set; } = 50;
    public static int MarginTop { get; private set; } = 50;
    public static int MarginRight { get; private set; }
    public static int MarginBottom { get; private set; }
    public static int XMax { get; private set; }
    public static int XMin { get; private set; }
    public static int YMax { get; private set; }
    public static int YMin { get; private set; }

    public static void Initialize(Viewport viewport)
    {
        Vector2 windowSize = viewport.GetVisibleRect().Size;
        ScreenWidth = (int)windowSize.X;
        ScreenHeight = (int)windowSize.Y;
        MarginRight = ScreenWidth - MarginLeft;
        MarginBottom = ScreenHeight - MarginTop;
        XMax = MarginRight;
        XMin = MarginLeft;
        YMax = MarginTop;
        YMin = MarginBottom;
    }
    // KOORDINAT CONVERSION FUNCTIONS

    /// Konversi dari koordinat kartesian ke screen coordinate
    public static Vector2 ToScreenCoordinate(float x, float y)
    {
        float screenX = ScreenWidth / 2 + x;
        float screenY = ScreenHeight / 2 - y;
        return new Vector2(screenX, screenY);
    }

    public static void DrawAxes(Node2D node, Primitif primitif)
    {
        float centerX = ScreenWidth / 2;
        float centerY = ScreenHeight / 2;

        // Garis X
        var axisX = primitif.LineBresenham(0, (int)centerY, ScreenWidth, (int)centerY);
        GraphicsUtils.PutPixelAll(node, axisX, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(0));

        // Garis Y
        var axisY = primitif.LineBresenham((int)centerX, 0, (int)centerX, ScreenHeight);
        GraphicsUtils.PutPixelAll(node, axisY, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(0));
    }

    /// Konversi dari screen coordinate ke koordinat kartesian
    public Vector2 ToWorldCoordinate(float screenX, float screenY)
    {
        float worldX = screenX - ScreenUtils.MarginLeft;
        float worldY = ScreenUtils.MarginBottom - screenY;
        return new Vector2(worldX, worldY);
    }
}