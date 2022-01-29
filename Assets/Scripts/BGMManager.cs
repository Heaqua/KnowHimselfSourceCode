using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    [Header("Music")]
    public AudioClip BGMStage1;
    public AudioClip BGMStage2;
    public AudioClip BGMStage3;

    AudioSource Source;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Source = GetComponent<AudioSource>();
        if (Source != null)
        {
            Source.spatialBlend = 0f;
            Source.loop = true;
        }
    }

    public void StopMusic()
    {
        if (Source == null)
            return;

        Source.Stop();
    }

    public void PlayMusic(Commons.Stage CurrentStage)
    {
        if (Source == null)
            return;

        switch (CurrentStage)
        {
            case Commons.Stage.Stage1:
                break;
            case Commons.Stage.Stage2:
                break;
            case Commons.Stage.Stage3:
                break;
        }
    }
}
