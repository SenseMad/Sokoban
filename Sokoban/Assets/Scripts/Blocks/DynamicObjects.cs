using UnityEngine;

/// <summary>
/// Класс динамических объектов, которые можно двигать
/// </summary>
public class DynamicObjects : Block
{

  //--------------------------------------

  private new Rigidbody rigidbody;

  //======================================

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  private void Start()
  {
    typeObject = TypeObject.dynamicObject;
  }

  //======================================

  /// <summary>
  /// Движение объекта
  /// </summary>
  /// <param name="direction">Направление движения</param>
  public bool ObjectMove(Vector3 direction)
  {
    if (IsBlocked(direction))
      return false;

    transform.Translate(direction);
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