using UnityEngine;

/// <summary>
/// ����� ������������ ��������, ������� ����� �������
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
  /// �������� �������
  /// </summary>
  /// <param name="direction">����������� ��������</param>
  public bool ObjectMove(Vector3 direction)
  {
    if (IsBlocked(direction))
      return false;

    transform.Translate(direction);
    return true;
  }

  /// <summary>
  /// True, ���� �������� ������� ������ �������������
  /// </summary>
  /// <param name="direction">����������� ��������</param>
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
  /// True, ���� ������ ������
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