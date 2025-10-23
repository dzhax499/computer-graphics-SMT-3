namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class OutlineShape : Node2D
{
    // Shape properties
    public DraggableShape.ShapeType Type { get; set; }
    public float ShapeSize { get; set; } = 50f;
    public float DimAlas { get; set; } = 0f;
    public float DimTinggi { get; set; } = 0f;
    public float DimLebar { get; set; } = 0f;
    public float DimSkew { get; set; } = 0f;


    public float InitialRotation { get; set; } = 0f;
    public Color OutlineColor { get; set; } = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    private BentukDasar bentukDasar;
    private Transformasi transformasi;
    private List<Vector2> shapePoints;
    private List<Vector2> transformedPoints;
    private float[,] transformMatrix;

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        transformasi = new Transformasi();
        transformMatrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(transformMatrix);
        SetProcessInput(false);
        SetProcessUnhandledInput(false);
        GenerateShape();
        ApplyInitialRotation();
        QueueRedraw();
    }

    private void GenerateShape()
    {
        shapePoints = new List<Vector2>();

        switch (Type)
        {
            case DraggableShape.ShapeType.Persegi:
                float lebarPersegi = DimLebar > 0 ? DimLebar : ShapeSize;
                shapePoints = bentukDasar.Persegi(-lebarPersegi / 2, -lebarPersegi / 2, lebarPersegi);
                break;

            case DraggableShape.ShapeType.TrapesiumSiku:
                float alasTrapesium = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiTrapesium = DimTinggi > 0 ? DimTinggi : (ShapeSize * 0.6f);
                float sisiAtasTrapesium = DimLebar > 0 ? DimLebar : (ShapeSize * 0.6f);

                shapePoints = bentukDasar.TrapesiumSiku(
                    new Vector2(-alasTrapesium / 2, -tinggiTrapesium / 3),
                    (int)tinggiTrapesium,
                    (int)alasTrapesium,
                    (int)sisiAtasTrapesium
                );
                break;

                case DraggableShape.ShapeType.SegitigaSamaKaki:
                float alasSegitiga = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiSegitiga = DimTinggi > 0 ? DimTinggi : (ShapeSize * 0.8f);

                var pBawahKiri = new Vector2(-alasSegitiga / 2, tinggiSegitiga / 3);
                var pBawahKanan = new Vector2(alasSegitiga / 2, tinggiSegitiga / 3);
                var pPuncak = new Vector2(0, -tinggiSegitiga * 2 / 3);

                shapePoints = new List<Vector2>
                {
                    pBawahKiri,
                    pBawahKanan,
                    pPuncak,
                    pBawahKiri
                };
                break;

            case DraggableShape.ShapeType.SegitigaSiku:
                float alasSiku = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiSiku = DimTinggi > 0 ? DimTinggi : ShapeSize;

                shapePoints = bentukDasar.SegitigaSiku(
                    new Vector2(-alasSiku / 2, -tinggiSiku / 2),
                    (int)alasSiku,
                    (int)tinggiSiku
                );
                break;

            case DraggableShape.ShapeType.Hexagon:
                float sizeHex = DimLebar > 0 ? DimLebar : ShapeSize;
                shapePoints = bentukDasar.Hexagon(Vector2.Zero, sizeHex / 2, 0);
                break;

            case DraggableShape.ShapeType.Lingkaran:
                float diameter = DimLebar > 0 ? DimLebar : ShapeSize;
                shapePoints = bentukDasar.Lingkaran(Vector2.Zero, (int)(diameter / 2));
                break;

            case DraggableShape.ShapeType.JajarGenjang:
                float alasJajar = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiJajar = DimTinggi > 0 ? DimTinggi : (ShapeSize * 0.6f);
                float skewJajar = DimSkew != 0 ? DimSkew : (ShapeSize * 0.3f);

                shapePoints = bentukDasar.JajarGenjang(
                    new Vector2(-alasJajar / 2, -tinggiJajar / 4),
                    (int)alasJajar,
                    (int)tinggiJajar,
                    (int)skewJajar
                );
                break;

            case DraggableShape.ShapeType.TrapesiumSamaKaki:
                float alasIsosceles = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiIsosceles = DimTinggi > 0 ? DimTinggi : (ShapeSize * 0.6f);
                float sisiAtasIsosceles = DimLebar > 0 ? DimLebar : (ShapeSize * 0.6f);

                shapePoints = bentukDasar.TrapesiumSamaKaki(
                    new Vector2(-alasIsosceles / 2, -tinggiIsosceles / 3),
                    (int)tinggiIsosceles,
                    (int)alasIsosceles,
                    (int)sisiAtasIsosceles
                );
                break;
        }

        transformedPoints = new List<Vector2>(shapePoints);
    }

    private void ApplyInitialRotation()
    {
        if (InitialRotation != 0)
        {
            Transformasi.Matrix3x3Identity(transformMatrix);
            transformasi.RotationClockwise(transformMatrix, InitialRotation, Vector2.Zero);
            transformedPoints = transformasi.GetTransformPoint(transformMatrix, shapePoints);
        }
    }

    public override void _Draw()
    {
        if (transformedPoints == null || transformedPoints.Count == 0) return;

        // Draw outline (dashed style)
        DrawPolyline(transformedPoints.ToArray(), OutlineColor, 2.0f);

        // Draw center point indicator
        DrawCircle(Vector2.Zero, 3, OutlineColor);
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}