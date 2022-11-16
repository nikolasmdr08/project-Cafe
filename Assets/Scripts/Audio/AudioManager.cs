using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Range(0f, 1f)]
    public float masterVolume = 1;
    [Header("Sound")]
    [Range(0f, 1f)]
    public float soundVolume = 1;
    public bool SoundOn;

    public int poolSize;
    public Transform poolParent;

    private List<AudioSource> poolSources2D;
    private int soundIndex2D;
    public AudioMixerGroup soundMixer;

    [Header("Music")]
    [Range(0f, 1f)]
    public float musicVolume = 0;
    public bool MusicOn;
    public bool LoopMusic = true;
    public float fadeTime = 2;
    [SerializeField]
    private AudioSource[] musicChannels;
    public AudioMixerGroup[] musicMixers; 

    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();

    void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(gameObject); 

        CreatePool();
        SetMasterVolume(masterVolume);
        SetSoundVolume(soundVolume);
        SetMusicVolume(musicVolume);

        DontDestroyOnLoad(gameObject);
    }

    void CreatePool()
    {
        if (poolSources2D != null && poolSources2D.Count > 0)
            return;

        poolSources2D = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = new GameObject();
            AudioSource aSource = go.AddComponent<AudioSource>();
            go.name = "Audio 2D: " + i;
            aSource.spatialBlend = 0;
            aSource.playOnAwake = false;
            go.transform.parent = poolParent;
            aSource.outputAudioMixerGroup = soundMixer;
            poolSources2D.Add(aSource);
        }
        soundIndex2D = 0;
    }

    public static void S_PlaySound2D(SFXID id)
    {
        Instance.PlaySound(id, Vector3.zero);
    }

    public static void S_PlayMusic(SFXID id, int channel)
    {
        Instance.PlayMusic(id, channel);
    }

    void PlaySound(SFXID id, Vector3 sourcePosition)
    {
        bool variation = id != SFXID.UI_BUTTON;

        if (id == SFXID.NONE)
            return;

        AudioClip clip = GetSound(id);

        if (clip == null)
        {
            Debug.LogError("Something is wrong with sound: " + id);
            return;
        }

        if (clip != null)
            PlaySound(clip, sourcePosition, variation);
    }

    void PlaySound(AudioClip ac, Vector3 sourcePosition, bool withVariation)
    {
        if (!SoundOn)
            return;

        if (ac == null)
        {
            Debug.LogError($"PlaySound ac == null");
        }
        poolSources2D[soundIndex2D].clip = ac;
        if (withVariation) 
        {
            poolSources2D[soundIndex2D].pitch = Random.Range(0.8f, 1f);
            poolSources2D[soundIndex2D].volume = Random.Range(soundVolume * 0.6f, soundVolume);
        } else 
            poolSources2D[soundIndex2D].volume = soundVolume;

        poolSources2D[soundIndex2D].Play();
        soundIndex2D++;
        soundIndex2D = soundIndex2D % poolSize;
    }

    void PlayMusic(SFXID id, int channel)
    {
        if (id == SFXID.NONE)
        {
            if (musicChannels[channel].isPlaying)
                StartCoroutine(FadeOut(channel));
            return;
        }

        AudioClip clip = GetSound(id);

        if (clip == null)
        {
            Debug.LogError("Something is wrong with sound: " + id);
            return;
        }

        if (clip != null)
            PlayMusic(clip, channel);
    }

    void PlayMusic(AudioClip ac = null, int channel = 0)
    {

        Debug.Log("Music is " + MusicOn);

        if (!MusicOn)
            return;

        if (musicChannels == null || channel >= musicChannels.Length)
        {
            Debug.LogError("Error in music musicChannels");
            return;
        }

        if (!musicChannels[channel])
        {
            Debug.LogError("Sound channel not found");
            return;
        }

        if (musicChannels[channel].clip == ac && musicChannels[channel].volume > 0)
            return;

        StartCoroutine(FadeIn(channel, musicVolume));

        musicChannels[channel].clip = ac;
        musicChannels[channel].loop = LoopMusic;
        musicChannels[channel].Play();
    }

    AudioClip GetSound(SFXID id)
    {
        return audioClips.Find( ac => ac.name == id.ToString());
    }

    // ///////////////////HELPERS ////////////////////////////////

    public void SetMasterVolume(float val)
    {
        masterVolume = val;
        float dbVal = (val - 1) * 80f;
        soundMixer.audioMixer.SetFloat("MasterVolume", val);
    }

    public void ToggleSound()
    {
        SoundOn = !SoundOn;
    }

    public void SetSoundVolume(float val)
    {
        soundVolume = val;
        float dbVal = (val - 1) * 80f;
        soundMixer.audioMixer.SetFloat("SoundEffectVolume", dbVal);
    }

    public void ToggleMusic()
    {
        //MusicOn = !MusicOn;
        //if (MusicOn)
        //    PlayMusic(currentSong);
        //else
        //{
        //    if (channel00 != null)
        //        channel00.Stop();
        //}
    }
    public void SetMusicVolume(float val)
    {
        musicVolume = val;
        float dbVal = (val - 1) * 80f;
        soundMixer.audioMixer.SetFloat("Music01Volume", dbVal);
        soundMixer.audioMixer.SetFloat("Music02Volume", dbVal);
        soundMixer.audioMixer.SetFloat("Music03Volume", dbVal);
        soundMixer.audioMixer.SetFloat("Music04Volume", dbVal);
    }
    
    IEnumerator FadeOut(int channel)
    {
        float t = 0;

        while (t <= fadeTime)
        {
            float val = t / fadeTime;
            musicChannels[channel].volume = (musicChannels[channel].volume - val);

            t += Time.deltaTime;
            yield return null;
        }

        musicChannels[channel].volume = 0;

        musicChannels[channel].Stop();
    }

    IEnumerator FadeIn(int channel, float maxVolume)
    {
        float t = 0;

        while (t <= fadeTime && musicChannels[channel].volume < maxVolume)
        {
            float val = t / fadeTime;
            musicChannels[channel].volume = val;

            t += Time.deltaTime;
            yield return null;
        }

        musicChannels[channel].volume = maxVolume;
    }
}

public enum SFXID 
{
    NONE,
    MUSIC_UI_DEFAULT,
    UI_BUTTON,
    MUSIC_SCENE,
    MUSIC_PAUSE,
    PLAYER_JUMP,
    PLAYER_GROUNDED,
    PLAYER_ATTACK,
    PLAYER_RECEIVE_DAMAGE,
    PLAYER_DEATH,
    ENEMY_ATTACK_GHOST,
    ENEMY_ATTACK_HELLGATO,
    ENEMY_ATTACK_SKELETON,
    ENEMY_DEATH
}