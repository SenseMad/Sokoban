using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using Sokoban.LevelManagement;
using Sokoban.GridEditor;

public class PlayerObjects : Block
{
  [SerializeField, Tooltip("Тип объекта")]
  private TypeObject _typeObject;

  [SerializeField, Tooltip("Индекс объекта")]
  private int _indexObject;

  [SerializeField, Tooltip("Позиция объекта")]
  private Vector3Int _objectPosition;

  //--------------------------------------

  private new Rigidbody rigidbody;

  private InputHandler inputHandler;

  private LevelManager levelManager;

  private CinemachineVirtualCamera cinemachineVirtual;

  /// <summary>
  /// True, если можно двигаться
  /// </summary>
  private bool isPossibleMove = true;

  /// <summary>
  /// Сохраняет значение Time.time, когда была нажата кнопка движения в последний раз
  /// </summary>
  private float lastTime = 0;

  /// <summary>
  /// Время задержки перед следующим нажатием кнопки
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
  /// Движение игрока
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
  /// Движение
  /// </summary>
  /// <param name="direction">Направление движения</param>
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
  /// Возвращает True, если перед игроком 2 и более ящика, блок который нельзя двигать, и т.д.
  /// </summary>
  /// <param name="direction">Направление движения</param>
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

        #region Проверка движущихся объектов

        if (hit.collider.TryGetComponent(out DynamicObjects dynamicObject))
        {
          return !dynamicObject.ObjectMove(direction);
        }

        #endregion

        #region Проверка шипов

        if (hit.collider.TryGetComponent(out SpikeObject spikeObject))
        {
          // Если шип активирован, возвращаем True
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
  /// True, если перед игроком есть земля
  /// </summary>
  /// <param name="direction">Направление движения</param>
  private bool CheckGroundPlayer(Vector3 direction)
  {
    // Проверяем позицию клетки впереди игрока
    if (direction.z > 0f && !Physics.Raycast(transform.position + transform.forward, Vector3.down, 1f))
      return false;
    // Проверяем позицию клетки слева от игрока
    else if (direction.x < 0f && !Physics.Raycast(transform.position - transform.right, Vector3.down, 1f))
      return false;
    // Проверяем позицию клетки справа от игрока
    else if (direction.x > 0f && !Physics.Raycast(transform.position + transform.right, Vector3.down, 1f))
      return false;
    // Проверяем позицию клетки позади игрока
    else if (direction.z < 0f && !Physics.Raycast(transform.position - transform.forward, Vector3.down, 1f))
      return false;

    return true;
  }

  /// <summary>
  /// Проверить неровный блок
  /// </summary>
  /// <param name="direction">Направление движения</param>
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
  /// True, если перед игроком неровный блок
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
  /// Можно ли двигаться
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