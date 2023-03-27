using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : Block
{
  [SerializeField, Tooltip("Тип объекта")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("Позиция объекта")]
  private Vector3Int _objectPosition;

  //--------------------------------------

  private new Rigidbody rigidbody;

  //======================================

  public override TypeObject GetObjectType() => _typeObject;

  public override Vector3Int GetObjectPosition() => _objectPosition;

  //======================================

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  //======================================

  public override void RemoveRigidbody()
  {
    Destroy(rigidbody);
  }

  public override void SetPositionObject(Vector3Int parObjectPosition)
  {
    _objectPosition = parObjectPosition;
  }

  //======================================

}