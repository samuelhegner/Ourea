﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScatterSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] scatterSounds;

    [SerializeField] private AudioSource source;

    public float secondRandomizer;

    public float secondsBetweenSounds;

    [Range(0, .5f)] public float randomPitch;
    [Range(0, .5f)] public float randomVolume;

    private float startPitch;
    private float startVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        startPitch = source.pitch;
        startVolume = source.volume;
        
        StartCoroutine(PlayScatterSounds());
    }

    IEnumerator PlayScatterSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenSounds + Random.Range(-secondRandomizer, secondRandomizer));

            int ran = Random.Range(0, scatterSounds.Length);

            source.clip = scatterSounds[ran];

            source.pitch = startPitch + Random.Range(-randomPitch, randomPitch);
            source.volume = startVolume + Random.Range(-randomVolume, randomVolume);


            source.Play();
            
            yield return new WaitWhile(StillPlaying);
        }
    }


    bool StillPlaying()
    {
        if (source.isPlaying)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
