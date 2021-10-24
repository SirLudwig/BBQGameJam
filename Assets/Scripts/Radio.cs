using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Radio : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource source;

    public int currentAudioClip;

    private void Start()
    {
        source.clip = audioClips[0];
        source.Play();
    }

    private void Update()
    {
        Debug.Log(source.isPlaying);
    }
}
