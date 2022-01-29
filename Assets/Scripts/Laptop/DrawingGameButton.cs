using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawingGameButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject drawingGame;

    public GameObject canvas;

    public Camera drawingCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;
        if (clickCount == 2)
        {
            Debug.Log("double Clicked");
            drawingGame = Instantiate(drawingGame,this.transform.position, Quaternion.identity);
            drawingGame.transform.GetChild(1).GetComponent<DrawingScript2>().drawingCamera = drawingCamera;
            Debug.Log(drawingGame.transform.GetChild(1).name);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
