//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicSingleton : MonoBehaviour
{

    private static MusicSingleton musicSingletonInstance;
    private static AudioSource musicSource;



    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);

        //Singleton check
        if(musicSingletonInstance == null)
        {
            musicSingletonInstance = this;
            musicSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public static void changeSong(AudioClip song, float fadeTime = 1f, float newVolume = 0f, bool loop = true)
    {
        if (musicSingletonInstance && song != musicSource.clip)
        {
            musicSingletonInstance.StartCoroutine(AudioFader.ChangeSound(musicSource, song, fadeTime, newVolume));
            musicSource.loop = loop;
        }
    }
}