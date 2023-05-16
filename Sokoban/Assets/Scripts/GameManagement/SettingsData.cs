using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.GameManagement
{
  /// <summary>
  /// Данные настроек
  /// </summary>
  public sealed class SettingsData
  {
    [SerializeField, Tooltip("Громкость музыка")]
    private int _musicValue = 0;

    [SerializeField, Tooltip("Громкость звуков")]
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

    //======================================

    /// <summary>
    /// Событие: Изменение громкости музыки
    /// </summary>
    public UnityEvent<int> ChangeMusicValue { get; } = new UnityEvent<int>();

    /// <summary>
    /// Событие: Изменение громкости звуков
    /// </summary>
    public UnityEvent<int> ChangeSoundValue { get; } = new UnityEvent<int>();

    //======================================
  }
}