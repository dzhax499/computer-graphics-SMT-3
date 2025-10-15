namespace Godot;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class ChallengeLevel : Node2D
{
    // ENUMS
    public enum Difficulty { Easy, Medium, Hard }

    // EXPORTS
    [Export] public Difficulty CurrentDifficulty { get; set; } = Difficulty.Easy;

    // ... (Sisa variabel Anda seperti isGameCompleted, elapsedTime, dll tetap di sini) ...
    // CORE COMPONENTS
    private BentukDasar bentukDasar;
    private Node2D outlineContainer;
    private Node2D shapesContainer;

    // NODE REFERENSI BARU
    private Node levelConfigurationsContainer;

    // ... (Sisa referensi UI, state, dll.) ...
    private float elapsedTime = 0f;
    private bool isGameCompleted = false;
    private Vector2 templateCenter = new Vector2(640, 300);
    private Color outlineColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
    private Vector2 shapeSpawnStart = new Vector2(1050, 150);
    private PatternBlockSpawner blockSpawner;
    private WinNotification winNotification;
    private ShapeControls shapeControls;

    public override void _Ready()
    {
        try
        {
            GD.Print($"=== Memulai ChallengeLevel {CurrentDifficulty} ===");
            bentukDasar = new BentukDasar();

            // Dapatkan referensi node standar
            outlineContainer = GetNode<Node2D>("OutlineContainer");
            shapesContainer = GetNode<Node2D>("ShapesContainer");

            // Dapatkan referensi ke container node level
            levelConfigurationsContainer = GetNode<Node>("LevelConfigurations");

            // Panggil fungsi setup level yang baru
            SetupLevel();

            // Sisa dari _Ready() Anda
            CreatePaletteFromOutlines();
            CreateComponents();
            // ... (setup UI, pause menu, dll) ...

            QueueRedraw();
            GD.Print($"=== Level {CurrentDifficulty} Siap! ===");
        }
        catch (Exception e)
        {
            GD.PrintErr($"❌ Error di ChallengeLevel._Ready(): {e.Message}\n{e.StackTrace}");
        }
    }

    /// <summary>
    /// Fungsi ini sekarang bertugas memilih dan mengaktifkan
    /// node konfigurasi level yang sesuai.
    /// </summary>
    private void SetupLevel()
    {
        LevelConfiguration activeLevelConfig = null;

        // Iterasi melalui semua anak dari container konfigurasi
        foreach (var child in levelConfigurationsContainer.GetChildren())
        {
            // Cek apakah nama node cocok dengan tingkat kesulitan
            if (child.Name == CurrentDifficulty.ToString())
            {
                // Jika cocok, jadikan ini konfigurasi aktif kita
                activeLevelConfig = child as LevelConfiguration;
                child.ProcessMode = ProcessModeEnum.Inherit; // Aktifkan node ini
                GD.Print($"Mengaktifkan Konfigurasi Level: {child.Name}");
            }
            else
            {
                // Jika tidak cocok, nonaktifkan node ini
                child.ProcessMode = ProcessModeEnum.Disabled;
            }
        }

        if (activeLevelConfig != null)
        {
            // Berikan data yang dibutuhkan ke script level
            activeLevelConfig.Initialize(bentukDasar, templateCenter, outlineColor);
            // Suruh script level yang aktif untuk membuat outline
            activeLevelConfig.CreateLevelOutlines(outlineContainer);
        }
        else
        {
            GD.PrintErr($"Tidak dapat menemukan konfigurasi untuk level: {CurrentDifficulty}");
        }
    }

    // HAPUS FUNGSI-FUNGSI INI DARI ChallengeLevel.cs
    // private void CreateEasyOutlines() { ... }
    // private void CreateMediumOutlines() { ... }
    // private void CreateHardOutlines() { ... }
    // private void CreateOutlineShape(...) { ... }

    // ... (Semua sisa kode Anda seperti OnShapeSnapped, CheckWinCondition, Pause, dll, tetap tidak berubah) ...
    private void CreatePaletteFromOutlines() { /* ... implementasi Anda ... */ }
    private void CreateComponents() { /* ... implementasi Anda ... */ }
    public void OnShapeSnapped(DraggableShape shape) { /* ... implementasi Anda ... */ }
}
