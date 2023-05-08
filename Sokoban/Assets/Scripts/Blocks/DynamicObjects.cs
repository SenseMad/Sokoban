using UnityEngine;

/// <summary>
/// ����� ������������ ��������, ������� ����� �������
/// </summary>
public class DynamicObjects : Block
{
  [SerializeField, Tooltip("��� �������")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("������� �������")]
  private Vector3Int _objectPosition;

  //--------------------------------------

  private new Rigidbody rigidbody;

  //======================================

  public override TypeObject GetObjectType() => _typeObject;

  public override Vector3Int GetObjectPosition() => _objectPosition;

  //======================================

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  //======================================

  public override void SetPositionObject(Vector3Int parObjectPosition)
  {
    _objectPosition = parObjectPosition;
  }

  public override void RemoveRigidbody()
  {
    Destroy(rigidbody);
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
}