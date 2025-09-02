using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource, sfxSource;
    public AudioClip[] musicClips, sfxClips;
    public static AudioManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayMusic("background");
    }
    public void PlayMusic(string name)
    {
        AudioClip clip = System.Array.Find(musicClips, music => music.name == name);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + name);
        }
    }
    public void playSFX(string name)
    {
        AudioClip clip = System.Array.Find(sfxClips, sfx => sfx.name == name);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
