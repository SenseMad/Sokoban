using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.LevelManagement;

/// <summary>
/// ����� ������� ���
/// </summary>
public class FoodObject : InteractiveObjects
{
  [SerializeField, Tooltip("��� ���")]
  private TypesFood _typeFood;

  //======================================

  /// <summary>
  /// �������� ��� ���
  /// </summary>
  public TypesFood GetTypeFood() => _typeFood;

  /// <summary>
  /// True, ���� ��� �������
  /// </summary>
  public bool IsFoodCollected { get; private set; }

  //======================================

  private void Start()
  {
    typeObject = TypeObject.foodObject;
  }

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

/// <summary>
/// ���� ���
/// </summary>
public enum TypesFood
{
  /// <summary>
  /// ���������
  /// </summary>
  Hamburger,
  /// <summary>
  /// ���-���
  /// </summary>
  HotDog
}