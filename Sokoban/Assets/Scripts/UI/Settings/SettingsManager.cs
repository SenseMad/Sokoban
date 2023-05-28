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
    [SerializeField] private SwitchSpinBox _languageValue;

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
      if (_languageValue) spinBoxBases.Add(_languageValue);

      _musicValue.OnValueChanged += MusicValue_OnValueChanged;
      _soundValue.OnValueChanged += SoundValue_OnValueChanged;
      _languageValue.OnValueChanged += LanguageValue_OnValueChanged;
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
      indexActiveButton = 0;

      base.OnEnable();

      _musicValue.SetValueWithoutNotify(gameManager.SettingsData.MusicValue);
      _soundValue.SetValueWithoutNotify(gameManager.SettingsData.SoundVolume);
      _languageValue.SetValueWithoutNotify((int)gameManager.SettingsData.CurrentLanguage);
    }

    private void OnDestroy()
    {
      _musicValue.OnValueChanged -= MusicValue_OnValueChanged;
      _soundValue.OnValueChanged -= SoundValue_OnValueChanged;
      _languageValue.OnValueChanged -= LanguageValue_OnValueChanged;
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
    }

    private void LanguageValue_OnValueChanged(int parValue)
    {
      var localisationSystem = LocalisationSystem.GetNamesAllLanguage().Length - 1;
      if (parValue > localisationSystem)
      {
        parValue = 0;
        _languageValue.SetValueWithoutNotify(0);
      }

      if (parValue < 0)
      {
        parValue = localisationSystem;
        _languageValue.SetValueWithoutNotify(localisationSystem);
      }

      gameManager.SettingsData.CurrentLanguage = (Language)parValue;
      _languageValue.UpdateText(LocalisationSystem.GetNameLanguage(gameManager.SettingsData.CurrentLanguage));
      Sound();
      //gameManager.SettingsData.CurrentLanguage = (Language)parValue;
      //_languageValue.UpdateText();
      //();
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