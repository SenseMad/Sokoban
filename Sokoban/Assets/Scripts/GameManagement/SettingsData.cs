using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.GameManagement
{
  /// <summary>
  /// ������ ��������
  /// </summary>
  public sealed class SettingsData
  {
    [SerializeField, Tooltip("��������� ������")]
    private int _musicValue = 0;

    [SerializeField, Tooltip("��������� ������")]
    private int _soundVolume = 0;

    //======================================

    /// <summary>
    /// ��������� ������
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
    /// ��������� ������
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
    /// �������: ��������� ��������� ������
    /// </summary>
    public UnityEvent<int> ChangeMusicValue { get; } = new UnityEvent<int>();

    /// <summary>
    /// �������: ��������� ��������� ������
    /// </summary>
    public UnityEvent<int> ChangeSoundValue { get; } = new UnityEvent<int>();

    //======================================
  }
}