using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource MusicSource;

    private AudioSource FxSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.MusicSource = GetComponents<AudioSource>()[0];
        this.FxSource = GetComponents<AudioSource>()[1];
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        if (sound != null)
        {
            FxSource.clip = sound;
            FxSource.volume = volume;
            FxSource.Play();
        }
    }

}
