using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� �������������� � ���������
/// </summary>
public class InteractiveObjects : Block
{
  [SerializeField, Tooltip("��� �������")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("������ �������")]
  private int _indexObject;

  [SerializeField, Tooltip("������� �������")]
  private Vector3Int _objectPosition;
  
  //======================================

  public override TypeObject GetObjectType() => _typeObject;

  public override Vector3Int GetObjectPosition() => _objectPosition;

  public override int GetIndexObject() => _indexObject;

  //======================================

  public override void SetPositionObject(Vector3Int parObjectPosition)
  {
    _objectPosition = parObjectPosition;
  }

  //======================================

  private void Update()
  {
    
  }

  //======================================

  /// <summary>
  /// True, ���� ��������������� � ��������
  /// </summary>
  public bool IsInteractObject()
  {
    if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, 1f))
    {
      var collider = hit.collider;
      if (collider)
      {
        return true;
      }
    }

    return false;
  }

  //======================================

  public override void RemoveRigidbody() { }

  //======================================
}