using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    [Header("Backrooms Music Assets")]
    [Tooltip("Drag the music files from the Backrooms Asset Pack here.")]
    public List<AudioClip> musicTracks;

    [Header("Settings")]
    public bool shuffleTracks = true;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set up the source for ambient vibes
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        if (musicTracks != null && musicTracks.Count > 0)
        {
            StartCoroutine(PlayAmbienceLoop());
        }
        else
        {
            Debug.LogWarning("No music tracks assigned to the Sound script!");
        }
    }

    IEnumerator PlayAmbienceLoop()
    {
        while (true)
        {
            // Pick a track
            AudioClip clipToPlay = musicTracks[currentTrackIndex];
            audioSource.clip = clipToPlay;
            audioSource.Play();

            // Wait for the track to finish before playing the next one
            yield return new WaitForSeconds(clipToPlay.length);

            // Move to next track
            if (shuffleTracks)
            {
                currentTrackIndex = Random.Range(0, musicTracks.Count);
            }
            else
            {
                currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
            }
        }
    }
}