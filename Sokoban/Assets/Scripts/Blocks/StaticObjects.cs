using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��������, ������� ������ �������
/// </summary>
public class StaticObjects : Block
{
  [SerializeField, Tooltip("��� �������")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("������� �������")]
  private Vector3Int _objectPosition;

  //======================================

  public override TypeObject GetObjectType() => _typeObject;

  public override Vector3Int GetObjectPosition() => _objectPosition;

  //======================================

  public override void SetPositionObject(Vector3Int parObjectPosition)
  {
    _objectPosition = parObjectPosition;
  }

  public override void RemoveRigidbody() { }

  //======================================
}