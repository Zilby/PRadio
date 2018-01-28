using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    public float fadeDuration;
    public float playTime;

    public List<AudioClip> tracks;
    private int curTrackIndex = 0;

    private AudioSource musicSource;
    private float originalVolume;
    private bool paused;


    void Start() {
        musicSource = gameObject.GetComponent<AudioSource>();
        musicSource.loop = true;
        paused = false;
        originalVolume = 0f;

        musicSource.clip = tracks[0];
        StartCoroutine(FadeIn());
        StartCoroutine(ChangeTrack());
    }

    public void SetAudio(bool on) {
        AudioListener.pause = !on;
    }

    public void AdvanceTrack() {
        if (curTrackIndex >= tracks.Count - 1) {
            curTrackIndex = 0;
        } else {
            curTrackIndex++;
        }
        if (musicSource.isPlaying) {
            StartCoroutine(FadeOutThenIn());
        } else {
            musicSource.clip = tracks[curTrackIndex];
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator ChangeTrack() {
        while (!paused) {
            yield return new WaitForSeconds(playTime);
            AdvanceTrack();
        }
    }

    private IEnumerator FadeOutThenIn() {
        yield return StartCoroutine(FadeOut());
        musicSource.clip = tracks[curTrackIndex];
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        musicSource.volume = 0f;
        musicSource.Play();
        while (musicSource.volume < originalVolume) {
            musicSource.volume += originalVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        musicSource.volume = originalVolume;
    }

    private IEnumerator FadeOut() {
        float startVolume = musicSource.volume;
        while (musicSource.volume > 0f) {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        musicSource.volume = 0f;
        musicSource.Stop();
    }
}
