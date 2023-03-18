using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

using Sokoban.GridEditor;
using LevelManagement;

public class PlayerController : MonoBehaviour
{
  [SerializeField, Tooltip("����� �������� ����� ��������� �������� ������")]
  private float _delayTimeNextButtonPress = 0.25f;

  //--------------------------------------

  private LevelManager levelManager;

  private CinemachineVirtualCamera cinemachineVirtual;

  private Vector2 axisMovement;

  /// <summary>
  /// True, ���� ����� ���������
  /// </summary>
  private bool isPossibleMove = true;

  /// <summary>
  /// ��������� �������� Time.time, ����� ���� ������ ������ �������� � ��������� ���
  /// </summary>
  private float lastTime = 0;

  //======================================

  private void Awake()
  {
    if (!GridEditor.GridEditorEnabled)
      cinemachineVirtual = FindObjectOfType<CinemachineVirtualCamera>();

    levelManager = LevelManager.Instance;
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

    axisMovement.Normalize();
    Vector3 direction = new Vector3(axisMovement.x, 0.0f, axisMovement.y);

    if (axisMovement.sqrMagnitude > 0.5f)
    {
      if (Time.time - lastTime >= _delayTimeNextButtonPress)
      {
        Move(direction);
        lastTime = Time.time;
      }
    }
    else
    {
      lastTime = 0;
    }
  }

  //======================================

  /// <summary>
  /// �������� ������
  /// </summary>
  /// <param name="direction">����������� ��������</param>
  public bool Move(Vector3 direction)
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
      var collider = hit.collider;
      if (collider)
      {
        /*if (collider.GetComponent<GridObject>().TypeObject == TypeObject.staticObject)
          return true;*/
        if (collider.GetComponent<Block>().GetObjectType() == TypeObject.staticObject)
          return true;

        var dynamicObject = collider.GetComponent<DynamicObjects>();
        if (dynamicObject)
        {
          if (dynamicObject.GetObjectType() == TypeObject.dynamicObject)
          {
            return !dynamicObject.ObjectMove(direction);
          }
        }

        if (collider.GetComponent<Block>().GetObjectType() == TypeObject.interactiveObject)
          return false;
        /*var box = collider.GetComponent<Box>();
        if (box)
        {
          if (box.GetComponent<GridObject>().TypeObject == TypeObject.dynamicObject)
          {
            return !box.Move(direction);
          }
        }*/
      }

      return true;
    }

    return false;
  }

  /// <summary>
  /// True, ���� ����� ����� ���� �����
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
  /// True, ���� �������� ���� ����� ������� ������
  /// </summary>
  private bool IsUnevenBlock(Vector3 origin)
  {
    if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 1f))
    {
      var dynamicObject = hit.collider.GetComponent<DynamicObjects>();
      if (dynamicObject)
      {
        return dynamicObject.IsObjectFalling();
      }

      /*var box = hit.collider.GetComponent<Box>();
      if (box)
      {
        return box.FallCheck();
      }*/
    }

    return false;
  }

  //======================================

  /// <summary>
  /// ��������
  /// </summary>
  public void OnMove(InputAction.CallbackContext context)
  {
    axisMovement = context.ReadValue<Vector2>();
  }

  //======================================

  /// <summary>
  /// ����� �� ���������
  /// </summary>
  private void PossibleMove(bool parValue)
  {
    isPossibleMove = !parValue;
  }

  //======================================
}