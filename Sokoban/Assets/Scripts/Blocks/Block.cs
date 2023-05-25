using UnityEngine;

public abstract class Block : MonoBehaviour
{
  [SerializeField, Tooltip("Тип объекта")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("Индекс объекта")]
  private int _indexObject;

  [SerializeField, Tooltip("Позиция объекта")]
  private Vector3Int _objectPosition;

  [Header("UI")]
  [SerializeField, Tooltip("Название объекта")]
  private string _nameObject;
  [SerializeField, Tooltip("Спрайт объекта")]
  private Sprite _spriteObject;

  //--------------------------------------

  private BoxCollider boxCollider;

  //======================================

  protected virtual void Awake()
  {
    boxCollider = GetComponent<BoxCollider>();
  }

  //======================================

  /// <summary>
  /// Получить тип объекта
  /// </summary>
  public TypeObject GetTypeObject() => _typeObject;

  /// <summary>
  /// Получить индекс объекта
  /// </summary>
  public int GetIndexObject() => _indexObject;

  /// <summary>
  /// Получить позицию объекта
  /// </summary>
  public Vector3Int GetObjectPosition() => _objectPosition;

  /// <summary>
  /// Получить название объекта
  /// </summary>
  public string GetNameObject() => _nameObject;

  /// <summary>
  /// Получить спрайт объекта
  /// </summary>
  public Sprite GetSpriteObject() => _spriteObject;

  /// <summary>
  /// Получить BoxCollider
  /// </summary>
  public BoxCollider GetBoxCollider() => boxCollider;

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