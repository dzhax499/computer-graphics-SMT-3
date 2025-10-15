namespace Godot;

using Godot;
using System;

public partial class Menu : Node2D
{
    /// <summary>
    /// Dipanggil saat tombol "Back" ditekan.
    /// </summary>
    private void _on_BtnBack_pressed()
    {
        GD.Print("Tombol Back ditekan!");
        // Tambahkan logika untuk kembali ke menu utama di sini
        // Contoh: GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }

    /// <summary>
    /// Dipanggil saat tombol "EASY" ditekan.
    /// </summary>
    private void _on_btn_easy_pressed()
    {
        GD.Print("Level Mudah dipilih!");
        // Tambahkan logika untuk memulai level mudah di sini
        // Contoh: GetTree().ChangeSceneToFile("res://Scenes/EasyLevel.tscn");
    }

    /// <summary>
    /// Dipanggil saat tombol "MEDIUM" ditekan.
    /// </summary>
    private void _on_btn_medium_pressed()
    {
        GD.Print("Level Sedang dipilih!");
        // Tambahkan logika untuk memulai level sedang di sini
        GetTree().ChangeSceneToFile("res://Scenes/MediumLevel.tscn");
    }

    /// <summary>
    /// Dipanggil saat tombol "HARD" ditekan.
    /// </summary>
    private void _on_btn_hard_pressed()
    {
        GD.Print("Level Sulit dipilih!");
        // Tambahkan logika untuk memulai level sulit di sini
        // Contoh: GetTree().ChangeSceneToFile("res://Scenes/HardLevel.tscn");
    }
}