using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] List<AudioClip> deathSound;
    [SerializeField] AudioClip weaponSound;
    [SerializeField] AudioClip weaponCollisionSound;
    [SerializeField] AudioClip VictorySound;
    [SerializeField] AudioClip ButtonSound;
    [SerializeField] AudioClip ButtonRejectSound;
    [SerializeField] [Range(0f, 1f)] public float soundVolume = 1f;

    [Header("Music")]
    [SerializeField] AudioClip musicSound;
    [SerializeField] [Range(0f, 1f)] public float musicVolume = 1f;
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private GameObject player;

    public static AudioManager instance;

    void Awake()
    {
        ManageSingleton();
        // musicSource = gameObject.AddComponent<AudioSource>();
        // musicSource.clip = musicSound;
        // musicSource.volume = musicVolume;
        // musicSource.loop = true; 
    }

    void Start()
    {
        PlayMusic(); 
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayWeaponSoundClip()
    {
        PlayClip(weaponSound, soundVolume);
    }
    public void PlayDeathSoundClip()
    {
        int randomNumber = Random.Range(0, deathSound.Count - 1);
        PlayClip(deathSound[randomNumber],soundVolume);
    }

    public void PlayWeaponCollisionSoundClip()
    {
        PlayClip(weaponCollisionSound, soundVolume);
    }

    public void PlayVictorySoundClip()
    {
        PlayClip(VictorySound, soundVolume);
    }

    public void PlayButtonSoundClip()
    {
        PlayClip(ButtonSound, soundVolume);
    }

    public void PlayButtonRejectSoundClip()
    {
        PlayClip(ButtonRejectSound, soundVolume);
    }

    public void PlayMusic()
    {
        // if (!musicSource.isPlaying)
        //     musicSource.Play();
    }
    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
    public void MuteMusic()
    {
        musicSource.volume = 0;  
    }
    public void EnableMusic()
    {
        musicSource.volume = 1;  
    }
    public void MuteSound()
    {
        soundVolume = 0;  
    }
    public void EnableSound()
    {
        soundVolume = 1;  
    }
    public void EnableVibration(bool isVibrate)
    {
        if (isVibrate)  Handheld.Vibrate();
    }
    
}
