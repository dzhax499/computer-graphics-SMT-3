namespace Godot;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Pattern Block Spawner - Updated untuk kompatibilitas dengan BaseChallengeLevel
/// Spawn blocks dengan keyboard (1-7) atau SPACE untuk random
/// </summary>
public partial class PatternBlockSpawner : Node2D
{
    [Signal] public delegate void BlockSpawnedEventHandler(DraggableShape block);

    // Containers
    private Node2D outlineContainer;
    private List<OutlineShape> outlinesCache = new List<OutlineShape>();

    private BentukDasar bentukDasar;
    private Node2D shapesContainer;
    private Vector2 spawnPosition;
    private float spacing = 80f;

    // Pattern block templates
    private List<(DraggableShape.ShapeType type, Color color, float size)> blockTemplates;

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        InitializeBlockTemplates();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            DraggableShape.ShapeType? typeToSpawn = null;

            switch (keyEvent.Keycode)
            {
                case Key.Space:
                    SpawnRandomBlock();
                    GD.Print("🎲 Random block spawned via SPACE");
                    return;
                case Key.Key1:
                    typeToSpawn = DraggableShape.ShapeType.Persegi;
                    break;
                case Key.Key2:
                    typeToSpawn = DraggableShape.ShapeType.TrapesiumSiku;
                    break;
                case Key.Key3:
                    typeToSpawn = DraggableShape.ShapeType.SegitigaSamaKaki;
                    break;
                case Key.Key4:
                    typeToSpawn = DraggableShape.ShapeType.SegitigaSiku;
                    break;
                case Key.Key5:
                    typeToSpawn = DraggableShape.ShapeType.Hexagon;
                    break;
                case Key.Key6:
                    typeToSpawn = DraggableShape.ShapeType.Lingkaran;
                    break;
                case Key.Key7:
                    typeToSpawn = DraggableShape.ShapeType.JajarGenjang;
                    break;
            }

            if (typeToSpawn.HasValue)
            {
                SpawnBlock(typeToSpawn.Value);
                GD.Print($"⌨️ Spawned {typeToSpawn.Value} via keyboard");
            }
        }
    }

    private void InitializeBlockTemplates()
    {
        float baseSize = 50f;

        blockTemplates = new List<(DraggableShape.ShapeType type, Color color, float size)>
        {
            (DraggableShape.ShapeType.Persegi, new Color(1f, 0.2f, 0.2f), baseSize),
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0.2f, 1f, 0.2f), baseSize),
            (DraggableShape.ShapeType.Hexagon, new Color(0.2f, 0.2f, 1f), baseSize),
            (DraggableShape.ShapeType.JajarGenjang, new Color(1f, 1f, 0.2f), baseSize),
            (DraggableShape.ShapeType.TrapesiumSiku, new Color(1f, 0.2f, 1f), baseSize),
            (DraggableShape.ShapeType.SegitigaSiku, new Color(0.2f, 1f, 1f), baseSize),
            (DraggableShape.ShapeType.Lingkaran, new Color(1f, 0.5f, 0.2f), baseSize),
        };
    }

    public void SetSpawnPosition(Vector2 position)
    {
        spawnPosition = position;
    }

    public void SetShapesContainer(Node2D container)
    {
        shapesContainer = container;
    }

    public void SetOutlineSource(Node2D container)
    {
        outlineContainer = container;
        outlinesCache = outlineContainer?.GetChildren().OfType<OutlineShape>().ToList() ?? new List<OutlineShape>();
    }

    public DraggableShape SpawnBlock(DraggableShape.ShapeType type)
    {
        if (shapesContainer == null)
        {
            GD.PrintErr("❌ ShapesContainer not set!");
            return null;
        }

        // Cari outline kosong dengan tipe sama
        float? sizeFromOutline = null;
        if (outlinesCache != null && outlinesCache.Count > 0)
        {
            var candidate = outlinesCache.FirstOrDefault(o => o.Type == type && !IsOccupied(o));
            if (candidate != null) sizeFromOutline = candidate.ShapeSize;
        }

        var template = blockTemplates.FirstOrDefault(t => t.type == type);
        if (template.Equals(default))
        {
            GD.PrintErr($"❌ No template found for shape type: {type}");
            return null;
        }

        DraggableShape newBlock = new DraggableShape();
        newBlock.Type = template.type;
        newBlock.ShapeColor = template.color;
        newBlock.ShapeSize = sizeFromOutline ?? template.size;
        newBlock.Position = spawnPosition;

        // Add to scene FIRST before emitting signal
        shapesContainer.AddChild(newBlock);

        // Move spawn position for next block
        spawnPosition += new Vector2(spacing, 0);

        // Emit signal
        EmitSignal(SignalName.BlockSpawned, newBlock);

        GD.Print($"✅ Spawned {type} (size={newBlock.ShapeSize})");

        return newBlock;
    }

    public DraggableShape SpawnRandomBlock()
    {
        var randomTemplate = blockTemplates[GD.RandRange(0, blockTemplates.Count - 1)];
        return SpawnBlock(randomTemplate.type);
    }

    private bool IsOccupied(OutlineShape o)
    {
        if (shapesContainer == null) return false;
        foreach (var s in shapesContainer.GetChildren().OfType<DraggableShape>())
            if (s.IsSnapped && s.SnappedToOutline == o) return true;
        return false;
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}