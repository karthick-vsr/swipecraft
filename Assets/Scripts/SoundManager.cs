using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource bgSource;
    public AudioSource sfxSource;

    public AudioClip bgMusic;
    public AudioClip matchClip;
    public AudioClip missClip;

    private int previousMatches = 0; // only here
    private int previousTurns = 0;   // only here

  
    private void Start()
    {
        PlayBackgroundMusic();
        GameManager.OnScoreChanged += HandleScoreChanged;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(int matches, int turns)
    {
        if (matches > previousMatches)
        {
            PlaySFX(matchClip);
        }
        else if (turns > previousTurns)
        {
            PlaySFX(missClip);
        }

        previousMatches = matches;
        previousTurns = turns;
    }

    private void PlayBackgroundMusic()
    {
        if (bgSource != null && bgMusic != null)
        {
            bgSource.clip = bgMusic;
            bgSource.loop = true;
            bgSource.Play();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
