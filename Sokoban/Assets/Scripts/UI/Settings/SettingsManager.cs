using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.UI
{
  public class SettingsManager : MenuUI
  {
    [SerializeField] private RangeSpinBox _musicValue;
    [SerializeField] private RangeSpinBox _soundValue;

    //--------------------------------------

    private List<SpinBoxBase> spinBoxBases = new List<SpinBoxBase>();

    //======================================



    //======================================

    protected override void Awake()
    {
      base.Awake();

      if (_musicValue) spinBoxBases.Add(_musicValue);
      if (_soundValue) spinBoxBases.Add(_soundValue);
    }

    private void Start()
    {
      foreach (var spinBoxBase in spinBoxBases)
      {
        spinBoxBase.IsSelected = false;
      }

      OnSelected();
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