using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioClip[] musicAudioClips;

    private void Start()
    {
        
    }

    public void PlaySound(AudioSource audioSource, AudioClip audioClip, bool shouldAudioLoop)
    {
        if (audioSource == null || audioSource == null)
            return;

        audioSource.clip = audioClip;
        audioSource.loop = shouldAudioLoop;
        audioSource.Play();
    }

    public void PlayMusic(bool musicLoops)
    {
        if (!player.GetComponent<AudioSource>())
            return;

        int randomIndex = Random.Range(0, musicAudioClips.Length);
        AudioSource musicAudioSource = player.GetComponent<AudioSource>();
        PlaySound(musicAudioSource, musicAudioClips[randomIndex], musicLoops);

    }

    public void StopMusic()
    {
        if (player.GetComponent<AudioSource>())
        {
            AudioSource musicAudioSource = player.GetComponent<AudioSource>();
            musicAudioSource.Stop();
        }
 
    }

    public void ChangeMusicVolume(Slider slider)
    {
        if (!player.GetComponent<AudioSource>()) 
            return;

        AudioSource audioSource = player.GetComponent<AudioSource>();
        audioSource.volume = slider.value;
    }
}
