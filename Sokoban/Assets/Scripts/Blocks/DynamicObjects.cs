using UnityEngine;

/// <summary>
/// ����� ������������ ��������, ������� ����� �������
/// </summary>
public class DynamicObjects : Block
{

  //--------------------------------------

  private new Rigidbody rigidbody;

  /// <summary>
  /// True, ���� ������ ��������
  /// </summary>
  private bool isMoving = false;
  /// <summary>
  /// �������� �������
  /// </summary>
  private float speed = 2.0f;
  /// <summary>
  /// ����� �������
  /// </summary>
  private Vector3 lastPosition;
  /// <summary>
  /// ����������� ��������
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
  /// �������� �������
  /// </summary>
  /// <param name="parDirection">����������� ��������</param>
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