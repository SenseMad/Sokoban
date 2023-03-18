using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
  /// <summary>
  /// Получить тип объекта
  /// </summary>
  public abstract TypeObject GetObjectType();

  /// <summary>
  /// Получить позицию объекта
  /// </summary>
  public abstract Vector3Int GetObjectPosition();

  //======================================

  /// <summary>
  /// Установить позицию объекта
  /// </summary>
  public abstract void SetPositionObject(Vector3Int parObjectPosition);

  /// <summary>
  /// Удалить Rigidbody у объектов которые могут падать
  /// </summary>
  public abstract void RemoveRigidbody();

  //======================================
}