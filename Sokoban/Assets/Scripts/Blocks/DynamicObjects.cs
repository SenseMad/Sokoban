using UnityEngine;

/// <summary>
/// Класс динамических объектов, которые можно двигать
/// </summary>
public class DynamicObjects : Block
{

  //--------------------------------------

  private new Rigidbody rigidbody;

  /// <summary>
  /// True, если объект движется
  /// </summary>
  private bool isMoving = false;
  /// <summary>
  /// Скорость объекта
  /// </summary>
  private float speed = 2.0f;
  /// <summary>
  /// Новая позиция
  /// </summary>
  private Vector3 lastPosition;
  /// <summary>
  /// Направление движения
  /// </summary>
  private Vector3 direction;

  //======================================

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  private void Start()
  {
    typeObject = TypeObject.dynamicObject;
  }

  private void Update()
  {
    if (!isMoving)
      return;

    transform.position = Vector3.MoveTowards(transform.position, lastPosition + direction, speed * Time.deltaTime);

    if (transform.position == lastPosition + direction)
      isMoving = false;
  }

  //======================================

  /// <summary>
  /// Движение объекта
  /// </summary>
  /// <param name="parDirection">Направление движения</param>
  public bool ObjectMove(Vector3 parDirection, float parSpeed)
  {
    if (IsBlocked(parDirection))
      return false;

    isMoving = true;
    lastPosition = transform.position;
    direction = parDirection;
    speed = parSpeed;
    return true;
  }

  /// <summary>
  /// True, если движение объекта вперед заблокировано
  /// </summary>
  /// <param name="direction">Направление движения</param>
  private bool IsBlocked(Vector3 direction)
  {
    if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1))
    {
      if (hit.collider)
      {
        if (hit.collider.GetComponent<DynamicObjects>() || hit.collider.GetComponent<StaticObjects>())
          return true;

        if (hit.collider.TryGetComponent(out SpikeObject spikeObject))
        {
          if (spikeObject.IsSpikeActivated)
            return true;

          return false;
        }

        if (hit.collider.GetComponent<ButtonDoorObject>())
          return false;
      }
    }

    return false;
  }

  /// <summary>
  /// True, если объект падает
  /// </summary>
  public bool IsObjectFalling()
  {
    return rigidbody.velocity.y < -0.1f;
  }

  //======================================

  public override void RemoveRigidbody()
  {
    Destroy(rigidbody);
  }

  //======================================
}