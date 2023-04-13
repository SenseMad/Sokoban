using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.LevelManagement;
using Sokoban.Player;

/// <summary>
/// Класс объекта еды
/// </summary>
public class FoodObject : InteractiveObjects
{
  /// <summary>
  /// True, если еда собрана
  /// </summary>
  public bool IsFoodCollected { get; private set; }

  //======================================

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<PlayerController>())
    {
      IsFoodCollected = true;
      LevelManager.Instance.IsFoodCollected();
      gameObject.SetActive(false);
    }
  }

  //======================================
}