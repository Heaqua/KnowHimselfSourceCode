using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{ 

    public static SubtitleManager instance;
    List<string> SubtitleQueue;

    [Header("UI Parameters")]
    [SerializeField] TextMeshProUGUI SubtitleDisplay;
    [Range(1f, 20f)] public float SubtitleDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
