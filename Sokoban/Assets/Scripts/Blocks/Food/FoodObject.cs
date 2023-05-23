using UnityEngine;

using Sokoban.LevelManagement;

/// <summary>
///  Класс объекта еды
/// </summary>
public class FoodObject : InteractiveObjects
{
  [SerializeField, Tooltip("Тип еды")]
  private TypesFood _typeFood;

  //======================================

  /// <summary>
  /// Получить вид еды
  /// </summary>
  public TypesFood GetTypeFood() => _typeFood;

  /// <summary>
  /// True, если еда собрана
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

/// <summary>
/// Виды еды
/// </summary>
public enum TypesFood
{
  /// <summary>
  /// Гамбергер
  /// </summary>
  Hamburger,
  /// <summary>
  /// Хот-дог
  /// </summary>
  HotDog
}