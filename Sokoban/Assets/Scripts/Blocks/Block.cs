using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
  /// <summary>
  /// �������� ��� �������
  /// </summary>
  public abstract TypeObject GetObjectType();

  /// <summary>
  /// �������� ������� �������
  /// </summary>
  public abstract Vector3Int GetObjectPosition();

  //======================================

  /// <summary>
  /// ���������� ������� �������
  /// </summary>
  public abstract void SetPositionObject(Vector3Int parObjectPosition);

  /// <summary>
  /// ������� Rigidbody � �������� ������� ����� ������
  /// </summary>
  public abstract void RemoveRigidbody();

  //======================================
}