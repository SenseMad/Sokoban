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

  private Rigidbody rigidbody;

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
    if (IsObjectForwardBlocked(direction))
      return false;

    transform.Translate(direction);
    return true;
  }

  /// <summary>
  /// True, ���� �������� ������� ������ �������������
  /// </summary>
  /// <param name="direction">����������� ��������</param>
  private bool IsObjectForwardBlocked(Vector3 direction)
  {
    if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1))
    {
      var collider = hit.collider;
      if (collider)
      {
        if (collider.GetComponent<DynamicObjects>() || collider.GetComponent<StaticObjects>())
          return true;
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