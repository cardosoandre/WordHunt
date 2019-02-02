using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    private AudioSource aud;
    public AudioSource foundAudioSource;
    public AudioSource finishAudioSource;

    public AudioClip select;
    public AudioClip highlight;
    public AudioClip complete;
    public AudioClip finish;

    public float foundCounter = 1f;

    private void Awake()
    {
        if (AudioManager.instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);


        WordHunt.FoundWord += FoundWord;
        WordHunt.Finish += PlayFinish;
    }

    void Start () {

        aud = GetComponent<AudioSource>();
		
	}

    public void PlayFinish()
    {
        finishAudioSource.clip = finish;
        finishAudioSource.Play();
    }

    public void FoundWord(Transform one, Transform two){
        foundAudioSource.clip = complete;
        foundAudioSource.pitch = foundCounter;
        foundAudioSource.Play();
        foundCounter += .1f;
    }

    public void PlaySound(AudioClip clip, float pitch){
        aud.clip = clip;
        aud.pitch = pitch;
        aud.Play();
    }

    private void OnDestroy()
    {
        WordHunt.FoundWord -= FoundWord;
        WordHunt.Finish -= PlayFinish;
    }
}
