# Implementasi Challenge Easy - Pattern Block Activity

## Fitur yang Telah Diimplementasikan

### 1. **Bentuk Dasar Pattern Blocks**
- **Persegi** - Bentuk dasar persegi
- **Trapesium Siku** - Untuk bagian depan dan belakang motor
- **Segitiga Sama Kaki** - Untuk bagian atas motor
- **Hexagon** - Untuk roda motor

### 2. **Sistem Drag & Drop**
- **Mouse Interaction**: Klik dan drag untuk memindahkan bentuk
- **Visual Feedback**: Bentuk berubah warna saat di-snap
- **Snap Detection**: Otomatis snap ke outline yang sesuai (threshold 30px)

### 3. **Transformasi 2D**
- **Rotasi**: Tekan tombol 'R' untuk memutar bentuk 15 derajat
- **Translasi**: Drag untuk memindahkan posisi
- **Implementasi Manual**: Menggunakan fungsi transformasi dari modul 4

### 4. **Template Outline**
- **Tema Motor Civic**: 8 outline shapes yang membentuk motor
- **Warna Abu-abu**: Outline ditampilkan dengan style dotted
- **Posisi Template**: Di tengah layar (640, 300)

### 5. **Sistem Validasi**
- **Snap Detection**: Bentuk otomatis snap ke outline yang sesuai
- **Completion Check**: Game menang ketika semua bentuk terpasang dengan benar
- **Visual Indicator**: Label "LEVEL COMPLETED!" muncul saat selesai

### 6. **Interface Permainan**
- **Layout 720p**: Ukuran layar 1280x720
- **Area Puzzle**: Kiri layar untuk template outline
- **Area Shapes**: Kanan layar untuk draggable shapes
- **Instruksi**: Panduan kontrol di layar

## Struktur File

```
Scripts/
├── Core/
│   ├── Main.cs              # Menu utama dan navigasi level
│   └── DraggableShape.cs    # Sistem drag & drop untuk pattern blocks
├── Scenes/
│   └── Easy.cs              # Implementasi challenge Easy
├── BentukDasar.cs           # Fungsi-fungsi bentuk dasar
├── Transformasi.cs          # Implementasi transformasi 2D
├── GraphicsUtils.cs         # Utility untuk menggambar
└── ScreensUtils.cs          # Utility koordinat layar

Scenes/
└── ChallengeEasy.tscn       # Scene file untuk challenge Easy
```

## Cara Bermain

1. **Pilih Level Easy** dari menu utama
2. **Drag & Drop** bentuk-bentuk dari area kanan ke outline abu-abu di area kiri
3. **Rotasi** bentuk dengan menekan tombol 'R' saat mouse berada di atas bentuk
4. **Snap Otomatis** akan terjadi ketika bentuk dekat dengan outline yang sesuai
5. **Selesaikan** semua outline untuk menyelesaikan level

## Kontrol

- **Mouse Left Click + Drag**: Memindahkan bentuk
- **R Key**: Memutar bentuk 15 derajat (saat mouse di atas bentuk)
- **Back Button**: Kembali ke menu utama

## Spesifikasi Teknis

- **Ukuran Layar**: 1280x720 (720p)
- **Transformasi**: Implementasi manual menggunakan matriks 3x3
- **Snap Threshold**: 30 pixel
- **Rotasi Step**: 15 derajat per tombol R
- **Bentuk Dasar**: Menggunakan fungsi dari modul praktikum 1-4

## Status Implementasi

✅ **Selesai**: Challenge Easy dengan tema Motor Civic
⏳ **Berikutnya**: Challenge Medium dan Hard (sesuai permintaan)
