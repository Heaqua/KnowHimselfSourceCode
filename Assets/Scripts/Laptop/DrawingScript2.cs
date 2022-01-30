using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using Random = System.Random;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using static PredefinedImage;


[RequireComponent(typeof(RawImage))]
public class DrawingScript2 : MonoBehaviour
{
    #region Attributes
    public Texture2D brushTexture;
    public Mesh brushMesh;
    public Material brushMaterial;
    public Color brushColor = new Color(1, 1, 1, 1);
    public float brushScale = 1f;
    public bool isDrawing = false;
    public GameObject score;
    public GameObject instruction;
    public enum Stage {Stage1, Stage2, Stage3, Save2, Save3} // Save for development use only

    public Stage stage;

    RawImage rawImage;
    RenderTexture renderTexture;

    public Camera drawingCamera;
    

    #endregion


    void Start()
    {
        
        rawImage = GetComponent<RawImage>();
        Debug.Log(rawImage.rectTransform.rect.position);
        brushTexture.wrapMode = TextureWrapMode.Clamp;
        brushMaterial.mainTexture = brushTexture;

        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        // scoreTransform.position = rawImage.rectTransform.position + new Vector3(10f, 10f, 0);

        renderTexture = new RenderTexture(width, height, 1, RenderTextureFormat.ARGB32, 0);
        rawImage.texture = renderTexture;

        drawingCamera.targetTexture = renderTexture;

        switch (stage)
        {
            case Stage.Stage2:
                LoadQuestionMark();
                break;
            case Stage.Stage3:
                LoadToilet();
                break;
        }
    }

    void Update()
    {
        ShowInstruction();

        if (IsDrawingCompleted())
        {
            ShowScore();
            if (stage == Stage.Save2) SaveQuestionMark();
            if (stage == Stage.Save3) SaveToilet();
        }

        if (isDrawing) Draw();
    }

    void StartDraw()
    {
        // the moment the player starts drawing

        ClearCanvas();
        score.GetComponent<TextMeshProUGUI>().SetText("");
        switch (stage)
        {
            case Stage.Stage2:
                StartCoroutine(PlayeeDrawsQuestionMark());
                break;
            case Stage.Stage3:
                StartCoroutine(PlayeeDrawsToilet());
                break;
        }
    }

    void ShowInstruction()
    {
        switch (stage)
        {
            case Stage.Stage1:
                instruction.GetComponent<TextMeshProUGUI>().SetText("Please draw a duck:");
                break;
            case Stage.Stage2:
                instruction.GetComponent<TextMeshProUGUI>().SetText("Please draw a exclamation mark '!':");
                break;
            case Stage.Stage3:
                instruction.GetComponent<TextMeshProUGUI>().SetText("Please draw a water bottle:");
                break;
        }
    }
    void ShowScore()
    {
        int scoreInt = 0;
        string encourage = "";
        switch (stage)
        {
            case Stage.Stage1:
                scoreInt = new Random().Next(11, 30);
                encourage = "\nKeep working!";
                break;
            case Stage.Stage2:
            case Stage.Stage3:
                scoreInt = new Random().Next(80, 100);
                encourage = "\nBrilliant!";
                break;
        }

        string scoreString = $"Your score: {scoreInt}" + encourage;
                             ;
        score.GetComponent<TextMeshProUGUI>().SetText(scoreString);
        drawingStarted = false;
        drawingCompletedTime = 0;
    }

    


    // If a player has started drawing something, and ends his drawing for more than 1 second, we give a score
    private bool drawingStarted = false;
    public float drawingCompletedTime = 0f;
    private bool IsDrawingCompleted()
    {
        isDrawing = Mouse.current.leftButton.isPressed;
        if (isDrawing && !drawingStarted)
        {
            drawingStarted = true;
            drawingCompletedTime = 0;
            StartDraw();

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

        return drawingCompletedTime > 1;
    }
    
    private void Draw()
    {

        switch (stage)
        {
            case Stage.Stage1:
                DrawExactly();
                break;
            case Stage.Save2: 
            case Stage.Save3:
                DrawExactly();
                break;
            case Stage.Stage2:
                
            case Stage.Stage3:
                // StartCoroutine in the function StartDraw
                break;
        }


    }

    #region DrawOnCanvas

    private Vector2 lastMousePosition;
    void DrawExactly()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rawImage.rectTransform, Mouse.current.position.ReadValue(),
            drawingCamera, out var mousePosition3);
        Vector2 mousePosition = mousePosition3;
        mousePosition -= new Vector2(7.0f, 2.5f);

        if (Mouse.current.leftButton.wasPressedThisFrame)
            lastMousePosition = mousePosition;

        brushMaterial.color = brushColor;
        foreach (Vector2 point in GetPointsBetween(mousePosition, lastMousePosition))
        {
            DrawBrush(point);
            if(stage == Stage.Save2) _questionMark.Add(new SerializedVector2(point));
            if(stage == Stage.Save3) _toilet.Add(new SerializedVector2(point));
        }

        lastMousePosition = mousePosition;
        
    }

    void DrawBrush(Vector2 position)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(position, Quaternion.identity, Vector3.one * brushScale);
        Graphics.DrawMesh(brushMesh, matrix, brushMaterial, 0, drawingCamera);
    }

    Vector2[] GetPointsBetween(Vector2 b, Vector2 a)
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

    #endregion
    

    private void ClearCanvas()
    {
        RenderTexture screen = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = screen;
    }
 
    #region DrawPredefined

    private float delay = 0.0001f;

    IEnumerator PlayeeDrawsQuestionMark()
    {
        
        for (int i = 0; i < SavePredefinedImage.questionMark.Length; i+=1)
        {
            DrawBrush(SavePredefinedImage.questionMark[i].Vector2);
            drawingCompletedTime -= Time.deltaTime;
            yield return new WaitForSeconds(0); //delay
        }
    }
    
    IEnumerator PlayeeDrawsToilet()
    {
        
        for (int i = 0; i < SavePredefinedImage.toilet.Length; i+=5) // to control the speed
        {
            DrawBrush(SavePredefinedImage.toilet[i].Vector2);
            drawingCompletedTime -= Time.deltaTime;
            yield return new WaitForSeconds(0); // delay
        }
    }

    #endregion
    
}
