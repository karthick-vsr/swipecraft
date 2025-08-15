using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioSource bgSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip bgMusic;
    [SerializeField] AudioClip matchClip;
    [SerializeField] AudioClip missClip;
    [SerializeField] AudioClip clickClip;

    public static SoundManager Instance { get; private set; }



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }


    private void Start()
    {
        PlayBackgroundMusic();
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

    public void PlayMatchSound()
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(matchClip);
        }
    }
    public void PlayMissSound()
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(missClip);
        }
    }
    public void PlayClickSound()
    {
        if (sfxSource != null )
        {
            sfxSource.PlayOneShot(clickClip);
        }
    }
}
