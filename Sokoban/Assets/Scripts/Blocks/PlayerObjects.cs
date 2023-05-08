using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using Sokoban.LevelManagement;
using Sokoban.GridEditor;

public class PlayerObjects : Block
{
  [SerializeField, Tooltip("��� �������")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("������ �������")]
  private int _indexObject;

  [SerializeField, Tooltip("������� �������")]
  private Vector3Int _objectPosition;

  //--------------------------------------

  private new Rigidbody rigidbody;

  private InputHandler inputHandler;

  private LevelManager levelManager;

  private CinemachineVirtualCamera cinemachineVirtual;

  /// <summary>
  /// True, ���� ����� ���������
  /// </summary>
  private bool isPossibleMove = true;

  /// <summary>
  /// ��������� �������� Time.time, ����� ���� ������ ������ �������� � ��������� ���
  /// </summary>
  private float lastTime = 0;

  /// <summary>
  /// ����� �������� ����� ��������� �������� ������
  /// </summary>
  private float delayTimeNextButtonPress = 0.25f;

  //======================================

  public override TypeObject GetObjectType() => _typeObject;

  public override Vector3Int GetObjectPosition() => _objectPosition;

  public override int GetIndexObject() => _indexObject;

  //======================================

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();

    inputHandler = InputHandler.Instance;

    levelManager = LevelManager.Instance;

    if (!GridEditor.GridEditorEnabled)
      cinemachineVirtual = FindObjectOfType<CinemachineVirtualCamera>();
  }

  private void Start()
  {
    if (cinemachineVirtual != null)
      cinemachineVirtual.Follow = transform;
  }

  private void OnEnable()
  {
    levelManager.IsPause.AddListener(PossibleMove);
  }

  private void OnDisable()
  {
    levelManager.IsPause.AddListener(PossibleMove);
  }

  private void Update()
  {
    if (GridEditor.GridEditorEnabled)
      return;

    PlayerMovement();
  }

  //======================================

  /// <summary>
  /// �������� ������
  /// </summary>
  private void PlayerMovement()
  {
    Vector2 axisMovement = inputHandler.GetMove();
    axisMovement.Normalize();

    if (axisMovement.sqrMagnitude > 0.5f)
    {
      if (Time.time - lastTime >= delayTimeNextButtonPress)
      {
        Vector3 direction = new Vector3(axisMovement.x, 0.0f, axisMovement.y);
        Move(direction);
        lastTime = Time.time;
      }

      return;
    }

    lastTime = 0;
  }

  /// <summary>
  /// ��������
  /// </summary>
  /// <param name="direction">����������� ��������</param>
  private bool Move(Vector3 direction)
  {
    if (levelManager.LevelCompleted || !isPossibleMove)
      return false;

    if (Mathf.Abs(direction.x) < 0.5f)
      direction.x = 0;
    else
      direction.z = 0;

    direction.Normalize();

    if (IsBlocked(direction))
      return false;

    levelManager.NumberMoves++;
    transform.Translate(direction);
    return true;
  }

  /// <summary>
  /// ���������� True, ���� ����� ������� 2 � ����� �����, ���� ������� ������ �������, � �.�.
  /// </summary>
  /// <param name="direction">����������� ��������</param>
  private bool IsBlocked(Vector3 direction)
  {
    if (!CheckGroundPlayer(direction))
      return true;

    if (CheckUnevenBlock(direction))
      return true;

    if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1))
    {
      if (hit.collider)
      {
        var block = hit.collider.GetComponent<Block>();
        if (block.GetObjectType() == TypeObject.staticObject)
          return true;

        #region �������� ���������� ��������

        if (hit.collider.TryGetComponent(out DynamicObjects dynamicObject))
        {
          return !dynamicObject.ObjectMove(direction);
        }

        #endregion

        #region �������� �����

        if (hit.collider.TryGetComponent(out SpikeObject spikeObject))
        {
          // ���� ��� �����������, ���������� True
          if (spikeObject.IsSpikeActivated)
            return true;

          return false;
        }

        #endregion

        if (block.GetObjectType() == TypeObject.foodObject)
          return false;
      }

      return true;
    }

    return false;
  }

  /// <summary>
  /// True, ���� ����� ������� ���� �����
  /// </summary>
  /// <param name="direction">����������� ��������</param>
  private bool CheckGroundPlayer(Vector3 direction)
  {
    // ��������� ������� ������ ������� ������
    if (direction.z > 0f && !Physics.Raycast(transform.position + transform.forward, Vector3.down, 1f))
      return false;
    // ��������� ������� ������ ����� �� ������
    else if (direction.x < 0f && !Physics.Raycast(transform.position - transform.right, Vector3.down, 1f))
      return false;
    // ��������� ������� ������ ������ �� ������
    else if (direction.x > 0f && !Physics.Raycast(transform.position + transform.right, Vector3.down, 1f))
      return false;
    // ��������� ������� ������ ������ ������
    else if (direction.z < 0f && !Physics.Raycast(transform.position - transform.forward, Vector3.down, 1f))
      return false;

    return true;
  }

  /// <summary>
  /// ��������� �������� ����
  /// </summary>
  /// <param name="direction">����������� ��������</param>
  private bool CheckUnevenBlock(Vector3 direction)
  {
    if (direction.z > 0f && IsUnevenBlock(transform.position + transform.forward))
      return true;
    else if (direction.x < 0f && IsUnevenBlock(transform.position - transform.right))
      return true;
    else if (direction.x > 0f && IsUnevenBlock(transform.position + transform.right))
      return true;
    else if (direction.z < 0f && IsUnevenBlock(transform.position - transform.forward))
      return true;

    return false;
  }

  /// <summary>
  /// True, ���� ����� ������� �������� ����
  /// </summary>
  private bool IsUnevenBlock(Vector3 origin)
  {
    if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 1f))
    {
      if (hit.collider.TryGetComponent(out DynamicObjects dynamicObject))
      {
        return dynamicObject.IsObjectFalling();
      }
    }

    return false;
  }

  /// <summary>
  /// ����� �� ���������
  /// </summary>
  private void PossibleMove(bool parValue)
  {
    isPossibleMove = !parValue;
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
}