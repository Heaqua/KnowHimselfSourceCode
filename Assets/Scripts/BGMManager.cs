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

    [Header("Parameters")]
    public bool PlayOnEnable;

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

    private void OnEnable()
    {
        if (Source == null)
            return;

        if (PlayOnEnable)
            PlayMusic(Commons.Stage.Stage1);
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

        if (Source.isPlaying)
            Source.Stop();

        switch (CurrentStage)
        {
            case Commons.Stage.Stage1:
                Source.clip = BGMStage1;
                break;
            case Commons.Stage.Stage2:
                Source.clip = BGMStage2;
                break;
            case Commons.Stage.Stage3:
                Source.clip = BGMStage3;
                break;
        }

        if (Source.clip != null)
            Source.Play();
    }
}
