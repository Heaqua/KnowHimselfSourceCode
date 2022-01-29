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
    [Range(1f, 20f)] public float SubtitleDuration = 10f;

    bool rendering = false;

    private void OnEnable()
    {
        SubtitleQueue = new List<string>();
        ClearQueue();
    }

    private void OnDisable()
    {
        ClearQueue();
    }

    private void Update()
    {
        if (!rendering && SubtitleQueue.Count > 0)
            StartQueueRender();
    }

    public void AddToQueue(string dialogue) => SubtitleQueue.Add(dialogue);

    public void ClearQueue()
    {
        StopAllCoroutines();
        rendering = false;

        if (SubtitleDisplay != null)
            SubtitleDisplay.text = string.Empty;

        SubtitleQueue.Clear();
    }

    void StartQueueRender()
    {
        if (SubtitleDisplay == null)
            return;

        StartCoroutine(RenderQueue());
    }

    IEnumerator RenderQueue()
    {

        while (SubtitleQueue.Count > 0)
        {
            SubtitleDisplay.text = SubtitleQueue[0];
            yield return new WaitForSeconds(SubtitleDuration);
            SubtitleQueue.RemoveAt(0);
        }
        SubtitleDisplay.text = string.Empty;
    }
}
