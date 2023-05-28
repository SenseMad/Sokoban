using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.GameManagement
{
  /// <summary>
  /// Данные настроек
  /// </summary>
  public sealed class SettingsData
  {
    [SerializeField, Tooltip("√ромкость музыка")]
    private int _musicValue = 0;

    [SerializeField, Tooltip("√ромкость звуков")]
    private int _soundVolume = 0;

    //======================================

    /// <summary>
    /// Громкость музыка
    /// </summary>
    public int MusicValue
    {
      get => _musicValue;
      set
      {
        _musicValue = value;
        ChangeMusicValue?.Invoke(value);
      }
    }

    /// <summary>
    /// Громкость звуков
    /// </summary>
    public int SoundVolume
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

    /// <summary>
    /// —обытие: »зменение громкости музыки
    /// </summary>
    public UnityEvent<int> ChangeMusicValue { get; } = new UnityEvent<int>();

    /// <summary>
    /// —обытие: »зменение громкости звуков
    /// </summary>
    public UnityEvent<int> ChangeSoundValue { get; } = new UnityEvent<int>();

    /// <summary>
    /// —обытие: »зменение €зыка
    /// </summary>
    public UnityEvent<Language> ChangeLanguage { get; } = new UnityEvent<Language>();

    //======================================
  }
}