using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.LevelManagement;

/// <summary>
/// ����� ������� ���
/// </summary>
public class FoodObject : InteractiveObjects
{
  /// <summary>
  /// True, ���� ��� �������
  /// </summary>
  public bool IsFoodCollected { get; private set; }

  //======================================

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<PlayerObjects>())
    {
      IsFoodCollected = true;
      LevelManager.Instance.IsFoodCollected();
      gameObject.SetActive(false);
    }
  }

  //======================================
}