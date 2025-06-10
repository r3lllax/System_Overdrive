using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] audioClips; 
    public float delayBetweenClips = 0f; 

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            enabled = false;
        }
        StartSequence();
    }
    void Update()
    {
        if (audioSource)
        {
            audioSource.volume = DataManager.CurrentUser.Settings.MusicVolume;
        }
        
    }
    public void StartSequence()
    {
        if (isPlaying) return; 
        isPlaying = true;
        currentClipIndex = 0;
        PlayNextClip();
    }

    private void PlayNextClip()
    {
        if (currentClipIndex >= audioClips.Length)
        {
            currentClipIndex = 0;
        }
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
        StartCoroutine(WaitAndPlayNextClip());
    }

    private IEnumerator WaitAndPlayNextClip()
    {
        yield return new WaitForSeconds(audioSource.clip.length + delayBetweenClips); 
        currentClipIndex++;
        PlayNextClip();
    }

    private void StopSequence()
    {
        audioSource.Stop();
        isPlaying = false;
    }
}
