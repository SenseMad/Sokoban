using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeObject : InteractiveObjects
{
  /// <summary>
  /// True, если шип активирован
  /// </summary>
  public bool IsSpikeActivated { get; private set; }

  //======================================

  private void OnTriggerExit(Collider other)
  {
    if (other.GetComponent<Block>().GetObjectType() != TypeObject.playerObject)
      return;

    IsSpikeActivated = true;
  }

  //======================================
}