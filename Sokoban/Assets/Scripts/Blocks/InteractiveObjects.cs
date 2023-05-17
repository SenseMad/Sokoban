using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс взаимодействия с объектами
/// </summary>
public class InteractiveObjects : Block
{


  //======================================



  //======================================



  //======================================

  /// <summary>
  /// True, если взаимодействуем с объектом
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



  //======================================
}