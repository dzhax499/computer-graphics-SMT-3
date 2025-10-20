using Godot;

/// <summary>
/// Main Menu - Updated untuk load level yang baru
/// Diperbaiki: Class sekarang mewarisi dari Node2D, sesuai dengan tipe node di scene.
/// </summary>
public partial class Menu : Node2D // <-- DIUBAH DARI CONTROL MENJADI NODE2D
{
    public override void _Ready()
    {
        GD.Print("=== Main Menu Ready ===");
        GD.Print("Pattern Block Activity v2.0");
        GD.Print("- Easy Level: 7 pieces (Tangram)");
        GD.Print("- Medium Level: 8 pieces (House)");
        GD.Print("- Hard Level: 11 pieces (Castle)");
        GD.Print("========================");
    }

    // Button Easy -> Signal "pressed" -> _on_btn_easy_pressed
    private void _on_btn_easy_pressed()
    {
        GD.Print("🎮 Loading Easy Level...");
        // Pastikan path ini benar sesuai struktur folder Anda
        GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
    }

    // Button Medium -> Signal "pressed" -> _on_btn_medium_pressed
    private void _on_btn_medium_pressed()
    {
        GD.Print("🎮 Loading Medium Level...");
        // Ganti dengan scene medium jika sudah ada
        // GetTree().ChangeSceneToFile("res://Scenes/MediumLevel.tscn");
    }

    // Button Hard -> Signal "pressed" -> _on_btn_hard_pressed
    private void _on_btn_hard_pressed()
    {
        GD.Print("🎮 Loading Hard Level...");
        // Ganti dengan scene hard jika sudah ada
        // GetTree().ChangeSceneToFile("res://Scenes/HardLevel.tscn");
    }

    // Button Back -> Signal "pressed" -> _on_BtnBack_pressed
    private void _on_BtnBack_pressed()
    {
        GD.Print("🔙 Going back to Welcome screen");
        GetTree().ChangeSceneToFile("res://Scenes/Welcome.tscn");
    }
}