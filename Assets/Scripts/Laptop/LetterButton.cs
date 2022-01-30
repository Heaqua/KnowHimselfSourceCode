using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class LetterButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
   public GameObject path;
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        path = Instantiate(path,this.transform);
        path.GetComponent<TextMeshProUGUI>().SetText(Application.persistentDataPath + "/Letter.txt");
        Debug.Log("clicked");
    }
}
