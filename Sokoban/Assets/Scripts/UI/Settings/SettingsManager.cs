using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.GameManagement;

namespace Sokoban.UI
{
  public class SettingsManager : MenuUI
  {
    [SerializeField] private RangeSpinBox _musicValue;
    [SerializeField] private RangeSpinBox _soundValue;

    //--------------------------------------

    private GameManager gameManager;

    private List<SpinBoxBase> spinBoxBases = new List<SpinBoxBase>();

    //======================================



    //======================================

    protected override void Awake()
    {
      base.Awake();

      gameManager = GameManager.Instance;

      if (_musicValue) spinBoxBases.Add(_musicValue);
      if (_soundValue) spinBoxBases.Add(_soundValue);

      _musicValue.OnValueChanged += MusicValue_OnValueChanged;
      _soundValue.OnValueChanged += SoundValue_OnValueChanged;
    }

    private void Start()
    {
      foreach (var spinBoxBase in spinBoxBases)
      {
        spinBoxBase.IsSelected = false;
      }

      OnSelected();
    }

    protected override void OnEnable()
    {
      base.OnEnable();

      _musicValue.SetValueWithoutNotify(gameManager.SettingsData.MusicValue);
      _soundValue.SetValueWithoutNotify(gameManager.SettingsData.SoundVolume);
    }

    private void OnDestroy()
    {
      _musicValue.OnValueChanged -= MusicValue_OnValueChanged;
      _soundValue.OnValueChanged -= SoundValue_OnValueChanged;
    }

    //======================================

    private void MusicValue_OnValueChanged(int parValue)
    {
      gameManager.SettingsData.MusicValue = parValue;
      Sound();
    }

    private void SoundValue_OnValueChanged(int parValue)
    {
      gameManager.SettingsData.SoundVolume = parValue;
      Sound();
      //AudioManager.OnPlaySoundInterface?.Invoke();
    }

    //======================================

    protected override void OnSelected()
    {
      base.OnSelected();

      if (indexActiveButton > spinBoxBases.Count - 1)
        return;

      spinBoxBases[indexActiveButton].IsSelected = true;
    }

    protected override void OnDeselected()
    {
      base.OnDeselected();

      if (indexActiveButton > spinBoxBases.Count - 1)
        return;

      spinBoxBases[indexActiveButton].IsSelected = false;
    }

    //======================================
  }
}