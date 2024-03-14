using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.GameManagement
{
  public sealed class SettingsData
  {
    [SerializeField] private int _musicValue = 25;

    [SerializeField] private int _soundValue = 25;

    [SerializeField] private bool _fullScreenValue = true;

    [SerializeField] private List<Resolution> _resolutions = new();

    [SerializeField] private bool _vSyncValue = true;

    //======================================

    public int CurrentSelectedResolution { get; set; }

    //--------------------------------------

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
      get => _soundValue;
      set
      {
        _soundValue = value;
        ChangeSoundValue?.Invoke(value);
      }
    }

#if !UNITY_PS4
    public bool FullScreenValue
    {
      get => _fullScreenValue;
      set
      {
        _fullScreenValue = value;
        OnChangeFullScreenValue?.Invoke(value);
      }
    }

    public List<Resolution> Resolutions { get => _resolutions; set => _resolutions = value; }

    public bool VSyncValue
    {
      get => _vSyncValue;
      set
      {
        _vSyncValue = value;
        OnChangeVSyncValue?.Invoke(value);
      }
    }
#endif

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

    public event Action<bool> OnChangeFullScreenValue;

    public event Action<bool> OnChangeVSyncValue;

    public UnityEvent<Language> ChangeLanguage { get; } = new UnityEvent<Language>();

    //======================================

    public void CreateResolutions()
    {
      Resolution[] resolutions = Screen.resolutions;
      HashSet<Tuple<int, int>> newResolutions = new HashSet<Tuple<int, int>>();
      Dictionary<Tuple<int, int>, int> maxRefreshRates = new Dictionary<Tuple<int, int>, int>();

      for (int i = 0; i < resolutions.Length; i++)
      {
        var resolution = new Tuple<int, int>(resolutions[i].width, resolutions[i].height);
        newResolutions.Add(resolution);

        if (!maxRefreshRates.ContainsKey(resolution))
        {
          maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
        }
        else
        {
          maxRefreshRates[resolution] = resolutions[i].refreshRate;
        }
      }

      foreach (var resolution in newResolutions)
      {
        Resolution newResolution = new()
        {
          width = resolution.Item1,
          height = resolution.Item2
        };

        if (maxRefreshRates.TryGetValue(resolution, out int refreshRate))
        {
          newResolution.refreshRate = refreshRate;
        }

        Resolutions.Add(newResolution);
      }

      for (int i = 0; i < Resolutions.Count; i++)
      {
        if (Resolutions[i].width == Screen.width & Resolutions[i].height == Screen.height)
        {
          CurrentSelectedResolution = i;
        }
      }
    }

    //======================================
  }
}