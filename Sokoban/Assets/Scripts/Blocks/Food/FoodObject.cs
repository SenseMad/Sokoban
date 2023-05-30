using UnityEngine;

using Sokoban.LevelManagement;

/// <summary>
///  Класс объекта еды
/// </summary>
public class FoodObject : InteractiveObjects
{
  [SerializeField, Tooltip("Тип еды")]
  private TypesFood _typeFood;

  //--------------------------------------

  private Transform meshTransform;

  //======================================

  protected override void Awake()
  {
    base.Awake();

    meshTransform = GetComponentInChildren<MeshFilter>().transform;
  }

  private void Update()
  {
    meshTransform.Rotate(new Vector3(0.0f, 30.0f * Time.deltaTime, 0.0f));
  }

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