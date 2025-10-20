namespace Godot;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Base class untuk semua challenge levels
/// Mengandung logic umum untuk snap, win condition, UI, dll
/// </summary>
public abstract partial class BaseChallengeLevel : Node2D
{
    // CONSTANTS
    protected const float WIN_POS_TOLERANCE = 5f;
    protected const float WIN_ROT_TOLERANCE = 8f;
    protected const float WIN_SIZE_TOLERANCE = 2f;

    // CORE COMPONENTS
    protected BentukDasar bentukDasar;
    protected Node2D outlineContainer;
    protected Node2D shapesContainer;

    // UI REFERENCES
    protected Label titleLabel;
    protected Label timeLabel;
    protected Label fpsLabel;
    protected Label latencyLabel;
    protected Button backButton;
    protected ColorRect patternBlockArea;
    protected Sprite2D boardSprite;

    // MANAGERS
    protected PatternBlockSpawner blockSpawner;
    protected GameTools gameTools;
    protected WinNotification winNotification;
    protected List<DraggableShape> spawnedBlocks = new List<DraggableShape>();

    // GAME STATE
    protected bool isGameCompleted = false;
    protected float elapsedTime = 0f;
    protected Color outlineColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    // POSITIONS (disesuaikan dengan Game.tscn)
    protected Vector2 boardCenter = new Vector2(493, 462);
    protected Vector2 patternBlockStart = new Vector2(1000, 200);

    // ABSTRACT METHODS - harus diimplementasi di child class
    protected abstract void CreateLevelOutlines();
    protected abstract string GetLevelTitle();
    protected abstract string GetNextLevelPath();
    protected abstract bool IsLastLevel();

    public override void _Ready()
    {
        try
        {
            GD.Print($"=== {GetLevelTitle()} _Ready() ===");

            bentukDasar = new BentukDasar();

            // Get references from scene
            GetSceneReferences();

            // Setup level
            SetupLevel();

            // Create managers
            CreateManagers();

            // Setup UI
            SetupUI();

            // Connect signals
            ConnectSignals();

            QueueRedraw();
            GD.Print($"=== {GetLevelTitle()} Ready! ===");
        }
        catch (Exception e)
        {
            GD.PrintErr($"❌ Error in BaseChallengeLevel._Ready(): {e.Message}\n{e.StackTrace}");
        }
    }

    protected virtual void GetSceneReferences()
    {
        // Get containers
        outlineContainer = GetNodeOrNull<Node2D>("Outline");
        if (outlineContainer == null)
        {
            GD.PrintErr("❌ Outline container not found!");
            outlineContainer = new Node2D { Name = "Outline" };
            AddChild(outlineContainer);
        }

        // Get atau create ShapesContainer
        shapesContainer = GetNodeOrNull<Node2D>("ShapesContainer");
        if (shapesContainer == null)
        {
            shapesContainer = new Node2D { Name = "ShapesContainer" };
            AddChild(shapesContainer);
        }

        // Get UI elements dari scene
        titleLabel = GetNodeOrNull<Label>("Title");
        var hud = GetNodeOrNull<CanvasLayer>("Hud");
        if (hud != null)
        {
            timeLabel = hud.GetNodeOrNull<Label>("Time");
            fpsLabel = hud.GetNodeOrNull<Label>("Fps");
            latencyLabel = hud.GetNodeOrNull<Label>("Latency");
        }

        var panel = GetNodeOrNull<Panel>("Panel");
        if (panel != null)
        {
            backButton = panel.GetNodeOrNull<Button>("BtnBack");
            boardSprite = panel.GetNodeOrNull<Sprite2D>("Papann");
        }

        patternBlockArea = GetNodeOrNull<ColorRect>("ColorRect");
    }

    protected virtual void SetupLevel()
    {
        // Create outlines (implemented by child)
        CreateLevelOutlines();

        // Create palette dari outlines
        CreatePaletteFromOutlines();
    }

    protected void CreateOutlineShape(DraggableShape.ShapeType type, Vector2 position, float size, float rotation)
    {
        OutlineShape outline = new OutlineShape();
        outline.Type = type;
        outline.Position = position;
        outline.ShapeSize = size;
        outline.InitialRotation = rotation;
        outline.OutlineColor = outlineColor;
        outlineContainer.AddChild(outline);
    }

    protected void CreatePaletteFromOutlines()
    {
        float spacing = 90f;
        int col = 0, row = 0;
        int maxCols = 2;

        var outlines = outlineContainer.GetChildren().OfType<OutlineShape>().ToList();

        // Posisi spawn di dalam ColorRect (pattern block area)
        Vector2 paletteStart = patternBlockStart;

        foreach (var outline in outlines)
        {
            var template = new DraggableShape();
            template.Type = outline.Type;
            template.ShapeSize = outline.ShapeSize;
            template.ShapeColor = GetColorForType(outline.Type);

            Vector2 spawnPos = paletteStart + new Vector2(col * spacing, row * spacing);
            template.Position = spawnPos;
            template.IsPaletteTemplate = true;
            template.PaletteSpawnPosition = spawnPos;

            shapesContainer.AddChild(template);

            // Connect signal
            template.ShapeSnapped += OnShapeSnapped;

            col++;
            if (col >= maxCols)
            {
                col = 0;
                row++;
            }
        }

        GD.Print($"✅ Created {outlines.Count} palette templates");
    }

    protected Color GetColorForType(DraggableShape.ShapeType type)
    {
        return type switch
        {
            DraggableShape.ShapeType.Persegi => new Color(0.8f, 0.4f, 0.2f),
            DraggableShape.ShapeType.SegitigaSamaKaki => new Color(1f, 0.5f, 0f),
            DraggableShape.ShapeType.JajarGenjang => new Color(0f, 0f, 1f),
            DraggableShape.ShapeType.Hexagon => new Color(0.2f, 0.6f, 0.2f),
            DraggableShape.ShapeType.SegitigaSiku => new Color(0.2f, 1f, 1f),
            DraggableShape.ShapeType.TrapesiumSiku => new Color(1f, 0.2f, 1f),
            DraggableShape.ShapeType.Lingkaran => new Color(1f, 0.5f, 0.2f),
            _ => new Color(0.7f, 0.7f, 0.7f)
        };
    }

    protected virtual void CreateManagers()
    {
        // Pattern block spawner
        blockSpawner = new PatternBlockSpawner();
        blockSpawner.SetSpawnPosition(patternBlockStart + new Vector2(0, 300));
        blockSpawner.SetShapesContainer(shapesContainer);
        blockSpawner.SetOutlineSource(outlineContainer);
        blockSpawner.BlockSpawned += OnBlockSpawned;
        AddChild(blockSpawner);

        // Game tools (Undo/Delete)
        gameTools = new GameTools();
        gameTools.UndoRequested += OnUndoRequested;
        gameTools.DeleteRequested += OnDeleteRequested;
        AddChild(gameTools);

        // Win notification
        winNotification = new WinNotification();
        winNotification.NextLevelRequested += OnNextLevel;
        winNotification.RestartRequested += OnRestart;
        winNotification.BackToMenuRequested += OnBackToMenu;
        AddChild(winNotification);
    }

    protected virtual void SetupUI()
    {
        if (titleLabel != null)
            titleLabel.Text = GetLevelTitle();

        if (backButton != null)
            backButton.Pressed += OnBackToMenu;
    }

    protected virtual void ConnectSignals()
    {
        // Already connected in CreateManagers
    }

    #region EVENT HANDLERS

    protected void OnBlockSpawned(DraggableShape block)
    {
        spawnedBlocks.Add(block);
        block.ShapeSnapped += OnShapeSnapped;
        GD.Print($"Block spawned: {block.Type}, Total: {spawnedBlocks.Count}");
    }

    public void OnShapeSnapped(DraggableShape shape)
    {
        GD.Print($"🔔 OnShapeSnapped: {shape.Type}");
        gameTools.ShowForShape(shape);
        UpdateProgressDisplay();
        CheckWinCondition();
    }

    protected void OnUndoRequested(DraggableShape shape)
    {
        shape.UndoToOriginalPosition();
        gameTools.HideTools();
        UpdateProgressDisplay();
    }

    protected void OnDeleteRequested(DraggableShape shape)
    {
        shape.DeleteShape();
        spawnedBlocks.Remove(shape);
        gameTools.HideTools();
        UpdateProgressDisplay();
    }

    protected void OnNextLevel()
    {
        string nextPath = GetNextLevelPath();
        if (!string.IsNullOrEmpty(nextPath))
            GetTree().ChangeSceneToFile(nextPath);
    }

    protected void OnRestart()
    {
        GetTree().ReloadCurrentScene();
    }

    protected void OnBackToMenu()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");
    }

    #endregion

    #region WIN CONDITION

    protected void CheckWinCondition()
    {
        if (isGameCompleted) return;

        var outlines = outlineContainer.GetChildren().OfType<OutlineShape>().ToList();
        var shapes = shapesContainer.GetChildren().OfType<DraggableShape>()
            .Where(s => !s.IsPaletteTemplate)
            .ToList();

        int correctCount = 0;

        foreach (var outline in outlines)
        {
            bool filled = false;

            foreach (var shape in shapes)
            {
                if (!shape.IsSnapped || shape.SnappedToOutline != outline) continue;

                bool posMatch = shape.GlobalPosition.DistanceTo(outline.GlobalPosition) <= WIN_POS_TOLERANCE;
                bool rotMatch = AngleDeltaDeg(shape.CurrentRotationDeg, outline.InitialRotation) <= WIN_ROT_TOLERANCE;
                bool sizeMatch = Mathf.Abs(shape.ShapeSize - outline.ShapeSize) <= WIN_SIZE_TOLERANCE;
                bool typeMatch = shape.Type == outline.Type;

                if (posMatch && rotMatch && sizeMatch && typeMatch)
                {
                    filled = true;
                    break;
                }
            }

            if (filled) correctCount++;
        }

        // WIN!
        if (correctCount == outlines.Count && correctCount > 0)
        {
            isGameCompleted = true;
            winNotification.ShowWin(GetLevelTitle(), IsLastLevel());
            GD.Print($"\n🎉 {GetLevelTitle()} COMPLETED! 🎉\n");
        }
    }

    protected static float AngleDeltaDeg(float a, float b)
    {
        float d = Mathf.Abs(Mathf.PosMod(a - b, 360f));
        return Mathf.Min(d, 360f - d);
    }

    #endregion

    #region UI UPDATES

    protected void UpdateProgressDisplay()
    {
        // Override di child jika perlu custom display
    }

    protected void UpdateHUD()
    {
        if (timeLabel != null)
        {
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);
            timeLabel.Text = $"Time: {minutes:D2}:{seconds:D2}";
        }

        if (fpsLabel != null)
        {
            fpsLabel.Text = $"FPS: {Engine.GetFramesPerSecond()}";
        }

        if (latencyLabel != null)
        {
            latencyLabel.Text = $"Latency: {Performance.GetMonitor(Performance.Monitor.TimeProcess) * 1000:F1}ms";
        }
    }

    #endregion

    public override void _Process(double delta)
    {
        if (!isGameCompleted)
        {
            elapsedTime += (float)delta;
            UpdateHUD();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            if (keyEvent.Keycode == Key.Escape)
            {
                OnBackToMenu();
                GetViewport().SetInputAsHandled();
            }
        }
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}