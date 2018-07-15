using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource MusicSource;

    private AudioSource FxSource;

    [SerializeField]
    private AudioClip MenuMusic;

    [SerializeField]
    private AudioClip FightMusic;

    [SerializeField]
    private AudioClip CoundDown;

    [SerializeField]
    private AudioClip StartStinger;

    [SerializeField]
    private AudioClip WinMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        this.MusicSource = GetComponents<AudioSource>()[0];
        this.FxSource = GetComponents<AudioSource>()[1];

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Invoke("PlayMenuMusic", 2.35f);
    }

    public void PlayMenuMusic()
    {
        if (MusicSource.clip == MenuMusic)
            return;

        MusicSource.clip = MenuMusic;
        MusicSource.loop = true;
        MusicSource.Play();
    }

    public void PlayCountdownAndFight()
    {
        MusicSource.clip = CoundDown;
        MusicSource.Play();
        PlaySound(StartStinger, 0.8f);
        Invoke("PlayFightMusic", CoundDown.length);
    }

    private void PlayFightMusic()
    {
        MusicSource.clip = FightMusic;
        MusicSource.Play();
    }

    public void PlayWinMusic()
    {
        MusicSource.clip = WinMusic;
        MusicSource.loop = false;
        MusicSource.Play();
        Invoke("PlayMenuMusic", WinMusic.length - 1f);
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        if (sound != null)
        {
            FxSource.PlayOneShot(sound, volume * 1.3f);
        }
    }

}
