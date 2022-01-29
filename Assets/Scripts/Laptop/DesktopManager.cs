using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesktopManager : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public Button DrawingGameButton;
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
            Debug.Log("Double clicked");
        }
    }
}
