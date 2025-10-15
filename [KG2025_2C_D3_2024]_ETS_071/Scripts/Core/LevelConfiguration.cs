namespace Godot;

using System;

/// <summary>
/// Ini adalah kelas dasar abstrak untuk semua konfigurasi level.
/// Setiap script kesulitan (Easy, Medium, Hard) akan mewarisi dari kelas ini.
/// Tugasnya adalah mendefinisikan "kontrak": setiap level HARUS bisa membuat outlinenya sendiri.
/// </summary>
public abstract partial class LevelConfiguration : Node
{
    // Properti ini akan diisi oleh ChallengeLevel
    protected BentukDasar BentukDasar { get; private set; }
    protected Vector2 TemplateCenter { get; private set; }
    protected Color OutlineColor { get; private set; }

    /// <summary>
    /// Fungsi utama yang harus diimplementasikan oleh setiap level.
    /// </summary>
    /// <param name="outlineContainer">Node tempat menaruh semua OutlineShape.</param>
    public abstract void CreateLevelOutlines(Node2D outlineContainer);

    /// <summary>
    /// Inisialisasi data yang dibutuhkan dari ChallengeLevel.
    /// </summary>
    public void Initialize(BentukDasar bentukDasar, Vector2 templateCenter, Color outlineColor)
    {
        this.BentukDasar = bentukDasar;
        this.TemplateCenter = templateCenter;
        this.OutlineColor = outlineColor;
    }

    /// <summary>
    /// Fungsi helper untuk membuat satu outline. Ditaruh di sini agar tidak duplikat kode.
    /// </summary>
    protected void CreateOutline(Node2D container, DraggableShape.ShapeType type, Vector2 position, float size, float rotation)
    {
        OutlineShape outline = new OutlineShape();
        outline.Type = type;
        outline.Position = position;
        outline.ShapeSize = size;
        outline.InitialRotation = rotation;
        outline.OutlineColor = this.OutlineColor;
        container.AddChild(outline);
    }
}