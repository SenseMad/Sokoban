using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.GridEditor;

public class Box : MonoBehaviour
{


  //--------------------------------------

  private Rigidbody rigidbody;

  //======================================

  /// <summary>
  /// True, если коробка установлена
  /// </summary>
  public bool IsBoxInstalled { get; private set; }

  //======================================

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  //======================================

  /// <summary>
  /// Движение объекта
  /// </summary>
  /// <param name="direction">Направление движения</param>
  public bool Move(Vector3 direction)
  {
    if (IsBoxBlocked(direction))
      return false;

    //rigidbody.useGravity = _canBlockFall;
    //IsBoxPlace(direction);
    transform.Translate(direction);
    return true;
  }

  /// <summary>
  /// True, если ящик заблокирован чем либо
  /// </summary>
  /// <param name="direction">Направление движения</param>
  private bool IsBoxBlocked(Vector3 direction)
  {
    if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1))
    {
      var collider = hit.collider;
      if (collider)
      {
        /*if (collider.GetComponent<GridObject>().TypeObject == TypeObject.staticObject)
          return true;*/

        if (collider.GetComponent<Box>())
          return true;
      }
    }

    return false;
  }

  /// <summary>
  /// Возвращает True, если коробка падает
  /// </summary>
  public bool FallCheck()
  {
    return rigidbody.velocity.y < -0.1f;
  }

  /// <summary>
  /// Проверка, установлена ли коробка на место
  /// </summary>
  /*private void IsBoxPlace(Vector3 direction)
  {
    if (Physics.Raycast(transform.position + Vector3.down, direction, out RaycastHit hit, 1f))
    {
      var gridObject = hit.collider.GetComponent<GridObject>();
      if (gridObject)
      {
        if (gridObject.TypeObject == TypeObject.pointBlock)
        {
          IsBoxInstalled = true;
          return;
        }
      }
    }

    IsBoxInstalled = false;
  }*/

  //======================================
}