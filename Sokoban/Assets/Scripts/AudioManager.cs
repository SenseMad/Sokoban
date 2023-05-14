using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.GameManagement
{
  public class AudioManager : MonoBehaviour
  {
    private static AudioManager _instance;

    //======================================

    [SerializeField, Tooltip("Игровые музыкальные клипы")]
    private List<AudioClip> _gameMusicClips;

    [SerializeField, Tooltip("Звук интерфейса")]
    private AudioClip _interfaceSound;

    //--------------------------------------

    private AudioSource audioSource;

    private GameManager gameManager;

    //======================================
    
    public static AudioManager Instance
    {
      get
      {
        return _instance != null ? _instance : FindObjectOfType<AudioManager>();
      }
    }

    //======================================

    /// <summary>
    /// Событие: Проиграть звук интерфейса
    /// </summary>
    public UnityEvent OnPlaySoundInterface { get; } = new UnityEvent();

    /// <summary>
    /// Событие: Проиграть звук
    /// </summary>
    public UnityEvent<AudioClip> OnPlaySFX { get; } = new UnityEvent<AudioClip>();

    //======================================

    private void Awake()
    {
      audioSource = GetComponent<AudioSource>();

      gameManager = GameManager.Instance;

      if (_instance != null && _instance != this)
      {
        Destroy(gameObject);
        return;
      }

      _instance = this;
      DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
      audioSource.clip = GetRandomClip();
      audioSource.Play();
    }

    private void OnEnable()
    {
      UpdateAudioSource(gameManager.SettingsData.MusicValue);

      OnPlaySoundInterface.AddListener(PlaySoundInterface);
      OnPlaySFX.AddListener(PlaySFX);

      gameManager.SettingsData.ChangeMusicValue.AddListener(UpdateAudioSource);
    }

    private void OnDisable()
    {
      OnPlaySoundInterface.RemoveListener(PlaySoundInterface);
      OnPlaySFX.RemoveListener(PlaySFX);

      gameManager.SettingsData.ChangeMusicValue.RemoveListener(UpdateAudioSource);
    }

    private void Update()
    {
      if (_gameMusicClips.Count == 0)
        return;

      if (_gameMusicClips.Count > 0)
      {
        if (!audioSource.isPlaying && audioSource.time == audioSource.clip.length)
        {
          audioSource.clip = GetRandomClip();
          audioSource.Play();
        }
      }
    }

    //======================================

    /// <summary>
    /// Рандомная музыка без повторов
    /// </summary>
    private AudioClip GetRandomClip()
    {
      AudioClip clip;

      clip = _gameMusicClips[Random.Range(0, _gameMusicClips.Count)];

      while (clip == audioSource.clip)
        clip = _gameMusicClips[Random.Range(0, _gameMusicClips.Count)];

      return clip;
    }

    /// <summary>
    /// Воспроизвести звук
    /// </summary>
    private AudioSource PlaySound(AudioClip parAudioClip)
    {
      GameObject objectAudio = new GameObject("SoundEffects");
      objectAudio.transform.position = transform.position;

      AudioSource tempAudioSource = objectAudio.AddComponent<AudioSource>();
      tempAudioSource.clip = parAudioClip;
      tempAudioSource.volume = (float)gameManager.SettingsData.SoundVolume / 100;
      tempAudioSource.Play();
      Destroy(objectAudio, parAudioClip.length);

      return tempAudioSource;
    }

    //======================================

    /// <summary>
    /// Проиграть звук интерфейса
    /// </summary>
    private void PlaySoundInterface()
    {
      PlaySound(_interfaceSound);
    }

    /// <summary>
    /// Проиграть звук
    /// </summary>
    private void PlaySFX(AudioClip parAudioClip)
    {
      PlaySound(parAudioClip);
    }

    //======================================

    private void UpdateAudioSource(int parValue)
    {
      audioSource.volume = (float)parValue / 100 * 0.5f;
    }

    //======================================
  }
}