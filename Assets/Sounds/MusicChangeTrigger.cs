using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class MusicChangeTrigger : MonoBehaviour
{

    private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        changeMusic();
    }

    private void changeMusic()
    {
        MusicSingleton.changeSong(musicSource.clip, 1, musicSource.volume, musicSource.loop);
        //gameObject.SetActive(false);
    }
}
