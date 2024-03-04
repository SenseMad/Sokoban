using System;
using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.GameManagement
{
  public sealed class SettingsData
  {
    [SerializeField] private int _musicValue = 0;

    [SerializeField] private int _soundVolume = 0;

    //======================================

    public int MusicValue
    {
      get => _musicValue;
      set
      {
        _musicValue = value;
        ChangeMusicValue?.Invoke(value);
      }
    }

    public int SoundValue
    {
      get => _soundVolume;
      set
      {
        _soundVolume = value;
        ChangeSoundValue?.Invoke(value);
      }
    }

    public Language CurrentLanguage
    {
      get => LocalisationSystem.CurrentLanguage;
      set
      {
        LocalisationSystem.CurrentLanguage = value;
        ChangeLanguage?.Invoke(value);
      }
    }

    //======================================

    public UnityEvent<int> ChangeMusicValue { get; } = new UnityEvent<int>();

    public UnityEvent<int> ChangeSoundValue { get; } = new UnityEvent<int>();

    public UnityEvent<Language> ChangeLanguage { get; } = new UnityEvent<Language>();

    //======================================
  }
}