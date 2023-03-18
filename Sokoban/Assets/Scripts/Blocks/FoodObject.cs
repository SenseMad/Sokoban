using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LevelManagement;
using Unity.VisualScripting;

/// <summary>
/// ����� ������� ���
/// </summary>
public class FoodObject : InteractiveObjects
{
  /// <summary>
  /// True, ���� ��� �������
  /// </summary>
  public bool IsFoodCollected { get; set; }

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