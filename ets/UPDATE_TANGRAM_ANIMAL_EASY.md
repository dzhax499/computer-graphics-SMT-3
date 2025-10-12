# Update: Level Easy - Tangram Animal

## Perubahan Berdasarkan Gambar Tangram

### 1. **Analisis Gambar Tangram**
Berdasarkan gambar yang diberikan, tangram membentuk siluet hewan (anjing/rubah) dengan 7 bentuk geometris:

1. **Orange Small Triangle** - Segitiga kecil oranye (ekor)
2. **Green Square** - Persegi hijau (kaki depan, diputar 45°)
3. **Red Large Triangle** - Segitiga besar merah (tubuh atas)
4. **Purple Large Triangle** - Segitiga besar ungu (kepala)
5. **Blue Parallelogram** - Jajar genjang biru (tubuh bawah)
6. **Blue Small Triangle** - Segitiga kecil biru (kaki belakang)
7. **Yellow Small Triangle** - Segitiga kecil kuning (kaki belakang)

### 2. **Outline Template yang Diupdate**

#### **Posisi dan Bentuk:**
```csharp
// 1. Orange Small Triangle (tail) - kiri atas
CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
    templateCenter + new Vector2(-120, -40), baseSize * 0.8f, 0);

// 2. Green Square (front leg, rotated 45°) - kiri bawah
CreateOutlineShape(DraggableShape.ShapeType.Persegi,
    templateCenter + new Vector2(-80, 20), baseSize * 0.7f, 45);

// 3. Red Large Triangle (upper body) - tengah atas
CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
    templateCenter + new Vector2(-20, -20), baseSize * 1.2f, 0);

// 4. Purple Large Triangle (head) - kanan atas
CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
    templateCenter + new Vector2(60, -60), baseSize * 1.0f, 0);

// 5. Blue Parallelogram (lower body) - tengah
CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
    templateCenter + new Vector2(20, 40), baseSize * 1.0f, 0);

// 6. Blue Small Triangle (hind leg) - kanan bawah
CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
    templateCenter + new Vector2(100, 60), baseSize * 0.6f, 0);

// 7. Yellow Small Triangle (hind leg) - kanan bawah
CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
    templateCenter + new Vector2(80, 80), baseSize * 0.6f, 0);
```

### 3. **Draggable Shapes dengan Warna yang Sesuai**

#### **Warna dan Bentuk:**
```csharp
var shapes = new List<(DraggableShape.ShapeType type, Color color, float size)>
{
    (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 0.5f, 0f), baseSize * 0.8f),    // Orange Small Triangle (tail)
    (DraggableShape.ShapeType.Persegi, new Color(0f, 0.8f, 0f), baseSize * 0.7f),             // Green Square (front leg)
    (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 0f, 0f), baseSize * 1.2f),      // Red Large Triangle (upper body)
    (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0.6f, 0f, 1f), baseSize * 1.0f),    // Purple Large Triangle (head)
    (DraggableShape.ShapeType.JajarGenjang, new Color(0f, 0f, 1f), baseSize * 1.0f),          // Blue Parallelogram (lower body)
    (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0f, 0.5f, 1f), baseSize * 0.6f),    // Blue Small Triangle (hind leg)
    (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 1f, 0f), baseSize * 0.6f),      // Yellow Small Triangle (hind leg)
};
```

### 4. **Penambahan Shape Type Baru**

#### **JajarGenjang Support:**
- **Enum**: Ditambahkan `JajarGenjang` ke `ShapeType`
- **Generation**: Menggunakan `bentukDasar.JajarGenjang()`
- **Parameters**: `(posisi, alas, tinggi, jarakBeda)`

```csharp
case ShapeType.JajarGenjang:
    OriginalPoints = bentukDasar.JajarGenjang(
        new Vector2(-ShapeSize / 2, -ShapeSize / 4),
        (int)ShapeSize,
        (int)(ShapeSize * 0.6f),
        (int)(ShapeSize * 0.3f)
    );
    break;
```

### 5. **Layout dan UI Updates**

#### **Title Update:**
- **Sebelumnya**: "LEVEL EASY - MOTOR CIVIC"
- **Sekarang**: "LEVEL EASY - TANGRAM ANIMAL"

#### **Grid Layout:**
- **Sebelumnya**: 2 kolom (8 shapes)
- **Sekarang**: 3 kolom (7 shapes) - lebih compact

#### **Base Size:**
- **Sebelumnya**: 60f
- **Sekarang**: 50f - lebih proporsional untuk tangram

### 6. **Komposisi Tangram Animal**

#### **Struktur Bentuk:**
```
    [Orange Triangle] (tail)
         |
[Green Square] - [Red Triangle] - [Purple Triangle] (head)
    (front leg)    (upper body)
         |
[Blue Parallelogram] (lower body)
         |
[Blue Triangle] - [Yellow Triangle] (hind legs)
```

#### **Warna Mapping:**
- **Orange** (1f, 0.5f, 0f) - Tail triangle
- **Green** (0f, 0.8f, 0f) - Front leg square
- **Red** (1f, 0f, 0f) - Upper body triangle
- **Purple** (0.6f, 0f, 1f) - Head triangle
- **Blue** (0f, 0f, 1f) - Lower body parallelogram
- **Light Blue** (0f, 0.5f, 1f) - Hind leg triangle
- **Yellow** (1f, 1f, 0f) - Hind leg triangle

### 7. **Fitur yang Dipertahankan**

#### **Sistem yang Tetap Sama:**
- **Drag & Drop** - Tetap menggunakan mouse
- **Rotasi** - Tekan 'R' untuk rotasi 45 derajat
- **Snap System** - Otomatis snap ke outline
- **Level Progression** - Tetap lanjut ke Medium setelah selesai
- **Restart** - Tetap bisa restart level

### 8. **Cara Bermain Tangram Animal**

1. **Drag** setiap bentuk dari area kanan ke outline yang sesuai
2. **Rotasi** bentuk dengan hover + tekan 'R' (khusus untuk Green Square yang perlu diputar 45°)
3. **Snap** otomatis akan terjadi ketika bentuk dekat dengan outline
4. **Selesaikan** semua 7 bentuk untuk membentuk siluet hewan
5. **Lanjut** ke level Medium setelah selesai

### 9. **Tantangan Level Easy**

#### **Kompleksitas:**
- **7 bentuk** berbeda (sebelumnya 8)
- **1 bentuk perlu rotasi** (Green Square 45°)
- **Bentuk bervariasi** - segitiga, persegi, jajar genjang
- **Warna berbeda** - mudah dibedakan

#### **Strategi Penyelesaian:**
1. Mulai dengan **Red Large Triangle** (tubuh utama)
2. Pasang **Purple Large Triangle** (kepala)
3. Tambahkan **Blue Parallelogram** (tubuh bawah)
4. Pasang **Orange Small Triangle** (ekor)
5. Tambahkan **Green Square** (kaki depan) - perlu rotasi
6. Pasang **Blue Small Triangle** (kaki belakang)
7. Akhiri dengan **Yellow Small Triangle** (kaki belakang)

## Status

✅ **Selesai**: Analisis gambar tangram animal
✅ **Selesai**: Update outline template dengan 7 bentuk
✅ **Selesai**: Update draggable shapes dengan warna yang sesuai
✅ **Selesai**: Tambah support JajarGenjang
✅ **Selesai**: Update UI dan layout
✅ **Selesai**: Test dan validasi

Level Easy sekarang menggunakan tema tangram animal yang lebih menarik dan sesuai dengan gambar referensi!
