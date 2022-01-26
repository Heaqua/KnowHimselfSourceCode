using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void openPanel()
    {
        Debug.Log("OpenPanel");
        if (gameObject)
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
    }

    public void closePanel()
    {

        if (gameObject)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
