using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
  [SerializeField, Tooltip("Индекс объекта")]
  protected int _indexObject;

  [SerializeField, Tooltip("Позиция объекта")]
  protected Vector3Int _objectPosition;

  /// <summary>
  /// Тип объекта
  /// </summary>
  protected TypeObject typeObject;

  //======================================

  /// <summary>
  /// Получить тип объекта
  /// </summary>
  public TypeObject GetTypeObject() => typeObject;

  /// <summary>
  /// Получить позицию объекта
  /// </summary>
  public Vector3Int GetObjectPosition() => _objectPosition;

  //======================================

  /// <summary>
  /// Установить позицию объекта
  /// </summary>
  public void SetPositionObject(Vector3Int parObjectPosition)
  {
    _objectPosition = parObjectPosition;
  }

  /// <summary>
  /// Удалить Rigidbody у объектов которые могут падать
  /// </summary>
  public virtual void RemoveRigidbody() { }

  //======================================
}