using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using Random = System.Random;

[RequireComponent(typeof(RawImage))]
public class DrawingScript : MonoBehaviour
{
    public Texture2D brushTexture;
    public Mesh brushMesh;
    public Material brushMaterial;
    public Color brushColor = new Color(1, 1, 1, 1);
    public float brushScale = 1f;
    public bool isDrawing = false;
    public GameObject score;
    public Transform scoreTransform;
    public GameObject canvas;


    RawImage rawImage;
    RenderTexture renderTexture;

    public Camera drawingCamera;

    void Start()
    {
        rawImage = GetComponent<RawImage>();

        brushTexture.wrapMode = TextureWrapMode.Clamp;
        brushMaterial.mainTexture = brushTexture;

        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        // scoreTransform.position = rawImage.rectTransform.position + new Vector3(10f, 10f, 0);

        renderTexture = new RenderTexture(width, height, 1, RenderTextureFormat.ARGB32, 0);
        rawImage.texture = renderTexture;

        drawingCamera.targetTexture = renderTexture;
    }

    void Update()
    {
        Draw();
        if (drawingCompleted())
        {
            int scoreInt = new Random().Next(5, 20);
            String scoreString = $"Your score: {scoreInt}"+
                "\nKeep working!";
            score.GetComponent<TextMeshProUGUI>().SetText(scoreString);
            drawingStarted = false;
            drawingCompletedTime = 0;
        }
    }

    // If a player has started drawing something, and ends his drawing for more than 2 seconds, we give a score
    private bool drawingStarted = false;
    private float drawingCompletedTime = 0f;
    private bool drawingCompleted()
    {
        if (isDrawing && !drawingStarted)
        {
            // the moment the player starts drawing
            ClearCanvas();
            score.GetComponent<TextMeshProUGUI>().SetText("");
            drawingStarted = true;
            drawingCompletedTime = 0;
        }else if (isDrawing)
        {
            // Continues drawing
            drawingCompletedTime = 0;
        }
        else if (drawingStarted)
        {
            // Drawing started but not drawing now
            drawingCompletedTime += Time.deltaTime;
        }

        if (drawingCompletedTime > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Vector2 lastMousePosition;
    private void Draw()
    {
        if (!Mouse.current.leftButton.isPressed)
        {
            isDrawing = false;
            return;
        }

        isDrawing = true;
            
        Vector3 mousePosition3;
        Vector2 mousePosition;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(rawImage.rectTransform, Mouse.current.position.ReadValue(),
            drawingCamera, out mousePosition3);
        mousePosition = mousePosition3;
        mousePosition = mousePosition - new Vector2(2.75f, 1.5f);


        if (Mouse.current.leftButton.wasPressedThisFrame)
            lastMousePosition = mousePosition;

        brushMaterial.color = brushColor;
        foreach (Vector2 point in GetPointsBetween(mousePosition, lastMousePosition))
            DrawBrush(point);

        lastMousePosition = mousePosition;
    }

    void DrawBrush(Vector2 position)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(position, Quaternion.identity, Vector3.one * brushScale);
        Graphics.DrawMesh(brushMesh, matrix, brushMaterial, 0, drawingCamera);
    }

    Vector2[] GetPointsBetween(Vector2 a, Vector2 b)
    {
        float spacing = 0.05f;
        Vector2 dir = (b - a).normalized;
        float dist = (b - a).magnitude;
        int num = Mathf.RoundToInt(dist / spacing);

        if (num == 0)
            num = 1;

        Vector2[] points = new Vector2[num];

        for (int i = 0; i < num; i++)
            points[i] = a + dir * i * spacing;

        return points;
    }

    public void ClearCanvas()
    {
        RenderTexture screen = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = screen;
    }
}
