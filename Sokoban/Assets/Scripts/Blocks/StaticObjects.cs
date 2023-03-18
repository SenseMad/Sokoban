using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс объектов, которые нельзя двигать
/// </summary>
public class StaticObjects : Block
{
  [SerializeField, Tooltip("Тип объекта")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("Позиция объекта")]
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