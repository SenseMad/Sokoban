using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
  [SerializeField, Tooltip("������ �������")]
  protected int _indexObject;

  [SerializeField, Tooltip("������� �������")]
  protected Vector3Int _objectPosition;

  /// <summary>
  /// ��� �������
  /// </summary>
  protected TypeObject typeObject;

  //======================================

  /// <summary>
  /// �������� ��� �������
  /// </summary>
  public TypeObject GetTypeObject() => typeObject;

  /// <summary>
  /// �������� ������� �������
  /// </summary>
  public Vector3Int GetObjectPosition() => _objectPosition;

  //======================================

  /// <summary>
  /// ���������� ������� �������
  /// </summary>
  public void SetPositionObject(Vector3Int parObjectPosition)
  {
    _objectPosition = parObjectPosition;
  }

  /// <summary>
  /// ������� Rigidbody � �������� ������� ����� ������
  /// </summary>
  public virtual void RemoveRigidbody() { }

  //======================================
}