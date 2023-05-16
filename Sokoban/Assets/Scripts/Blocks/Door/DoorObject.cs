using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �����
/// </summary>
public class DoorObject : StaticObjects
{
  [SerializeField, Tooltip("���� �����")]
  private DoorColor _doorColor;

  //======================================

  /// <summary>
  /// �������� ���� �����
  /// </summary>
  public DoorColor GetDoorColor() => _doorColor;

  //======================================

  private void Start()
  {
    //typeObject = TypeObject.doorObject;
  }

  //======================================
}