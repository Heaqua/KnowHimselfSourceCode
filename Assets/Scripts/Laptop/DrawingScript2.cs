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
    public enum Stage {Stage1, Stage2, Stage3, Save} // Save for development use only

    public Stage stage;

    RawImage rawImage;
    RenderTexture renderTexture;

    public Camera drawingCamera;
    

    #endregion


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

        switch (stage)
        {
            case Stage.Stage2:
                LoadData();
                break;
        }
    }

    void Update()
    {
        Draw();
        if (IsDrawingCompleted())
        {
            ShowScore();
            if (stage == Stage.Save) SaveData();
        }
        
    }

    void ShowScore()
    {
        int scoreInt = 0;
        switch (stage)
        {
            case Stage.Stage1:
                scoreInt = new Random().Next(11, 30);
                break;
            case Stage.Stage2:
            case Stage.Stage3:
                scoreInt = new Random().Next(80, 100);
                break;
        }
        
        string scoreString = $"Your score: {scoreInt}"+
                             "\nKeep working!";
        score.GetComponent<TextMeshProUGUI>().SetText(scoreString);
        drawingStarted = false;
        drawingCompletedTime = 0;
    }

    

    #region Save&Load
    private List<SerializedVector2> _questionMark = new List<SerializedVector2>();
    void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/QuestionMark.dat");
        Debug.Log(Application.persistentDataPath + "/QuestionMark.dat");
        SavePredefinedImage.questionMark = _questionMark.ToArray() ;
        bf.Serialize(file, SavePredefinedImage.questionMark);
        file.Close();
    }
    void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/QuestionMark.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/QuestionMark.dat", FileMode.Open);
            SavePredefinedImage.questionMark = (SerializedVector2[]) bf.Deserialize(file);
        }
    }
    #endregion
    
    // If a player has started drawing something, and ends his drawing for more than 1 second, we give a score
    private bool drawingStarted = false;
    private float drawingCompletedTime = 0f;
    private bool IsDrawingCompleted()
    {
        if (isDrawing && !drawingStarted)
        {
            // the moment the player starts drawing
            ClearCanvas();
            score.GetComponent<TextMeshProUGUI>().SetText("");
            drawingStarted = true;
            drawingCompletedTime = 0;
            switch (stage)
            {
                case Stage.Stage2:
                    StartCoroutine(PlayeeDrawsQuestionMark());
                    break;
            }
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
        if (!Mouse.current.leftButton.isPressed)
        {
            isDrawing = false;
            return;
        }

        isDrawing = true;

        switch (stage)
        {
            case Stage.Stage1:
                DrawExactly();
                break;
            case Stage.Save:
                DrawExactly();
                break;
            case Stage.Stage2:
            case Stage.Stage3:
                // StartCoroutine in the function IsDrawingCompleted
                break;
            // TODO: Stage2, Stage3
        }


    }

    #region DrawOnCanvas

    private Vector2 lastMousePosition;
    void DrawExactly()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rawImage.rectTransform, Mouse.current.position.ReadValue(),
            drawingCamera, out var mousePosition3);
        Vector2 mousePosition = mousePosition3;
        mousePosition -= new Vector2(2.75f, 1.5f);

        if (Mouse.current.leftButton.wasPressedThisFrame)
            lastMousePosition = mousePosition;

        brushMaterial.color = brushColor;
        foreach (Vector2 point in GetPointsBetween(mousePosition, lastMousePosition))
        {
            DrawBrush(point);
            if(stage == Stage.Save) _questionMark.Add(new SerializedVector2(point));
        }

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

    #endregion
    

    private void ClearCanvas()
    {
        RenderTexture screen = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = screen;
    }
    
    #region ToSavePredefinedImage

    [Serializable]
    class SavePredefinedImage
    {
        public static SerializedVector2[] questionMark;
    }
    
    [Serializable]class SerializedVector2
    {
        public float _x;
        public float _y;
     
        public Vector2 Vector2
        {
            get{
                return new Vector2(_x, _y);
            }
            set
            {
                _x = value.x;
                _y = value.y;
            }
        }

        public SerializedVector2(Vector2 _vector2)
        {
            _x = _vector2.x;
            _y = _vector2.y;
        }
    }


    #endregion

    #region DrawPredefined

    private float delay = 0.002f;

    IEnumerator PlayeeDrawsQuestionMark()
    {
        drawingCompletedTime = 0;
        for (int i = 0; i < SavePredefinedImage.questionMark.Length; i++)
        {
            DrawBrush(SavePredefinedImage.questionMark[i].Vector2);
            yield return new WaitForSeconds(delay);
        }
    }

    #endregion
    
}
