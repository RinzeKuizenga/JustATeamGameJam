using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static float NoiseEffects = 1f;
    public static float MusicEffects = 1f;
    public static AudioSource Music;
    public static void PlayClip(string path)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.Log("failed to load audio");
            return;
        }

        GameObject tempGO = new GameObject("TempAudio");
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.volume = NoiseEffects;
        aSource.Play();

        if (path == "PlayerFound")
        {
            aSource.volume = NoiseEffects / 2;
            aSource.pitch = 1.7f;
        }
        DontDestroyOnLoad(tempGO);
        // Destroy after the clip finishes playing
        UnityEngine.Object.Destroy(tempGO, clip.length);
    }
    public static void ChangeMusic(string path)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.Log("failed to load audio");
            return;
        }

        Music.clip = clip;
        Music.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var temp = new GameObject("MusicAudio");
        Music = temp.AddComponent<AudioSource>();
        Music.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        Music.volume = MusicEffects;
    }
}
