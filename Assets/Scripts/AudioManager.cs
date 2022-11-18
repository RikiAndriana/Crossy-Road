using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip newClip;
    [SerializeField] AudioClip sfxClip;

    private void Awake()
    {
        audioSource.volume = 0.5f;
        audioSource.mute = false;
        // audioSource.playOnAwake = true;
        // audioSource.loop = true;
        audioSource.priority = 128;
        audioSource.pitch = 1f;
        audioSource.panStereo = 0.5f;
        // audioSource.spatialBlend = 1;
        audioSource.reverbZoneMix = 1;
        audioSource.dopplerLevel = 1;
        audioSource.spread = 0;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.minDistance = 1;
        audioSource.maxDistance = 20;
    }
    private void Start()
    {
        audioSource.clip = newClip;
        audioSource.Play();
        // audioSource.PlayDelayed(2);
    }
    float timer = 0;
    private void Update()
    {
        // if (timer < 3)
        // {
        //     timer += Time.deltaTime;
        //     return;
        // }


        // if (audioSource.isPlaying)
        //     audioSource.Pause();
        // if (timer < 10)
        // {
        //     timer += Time.deltaTime;
        //     return;
        // }
        // if (audioSource.isPlaying == false)
        //     audioSource.UnPause();
        // if (timer < 15)
        // {
        //     timer += Time.deltaTime;
        //     return;
        // }
        // if (audioSource.isPlaying)
        //     audioSource.Stop();
    }
    public void PlaySFX()
    {
        audioSource.PlayOneShot(sfxClip);
    }
}
