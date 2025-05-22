using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Clips")]
    public AudioClip coinClip;
    public AudioClip buttonClip;
    public AudioClip coinGameMusicClip;

    private bool isMuted;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load mute state
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        ApplyMuteState();
    }

    //App's SFX control
    public void PlaySFX(string key)
    {
        if (sfxSource == null) return;

        switch (key)
        {
            case "coin":
                sfxSource.PlayOneShot(coinClip);
                break;
            case "button":
                sfxSource.PlayOneShot(buttonClip);
                break;
        }
    }

    //Game's BGM control
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyMuteState();
    }

    private void ApplyMuteState()
    {
        musicSource.volume = isMuted ? 0f : 1f;
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
