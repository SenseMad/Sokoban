using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using Sokoban.LevelManagement;

namespace Sokoban.Player
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField, Tooltip("Время задержки перед следующим нажатием кнопки")]
    private float _delayTimeNextButtonPress = 0.25f;

    //--------------------------------------

    private InputHandler inputHandler;

    private LevelManager levelManager;

    private CinemachineVirtualCamera cinemachineVirtual;

    private Vector2 axisMovement;

    /// <summary>
    /// True, если можно двигаться
    /// </summary>
    private bool isPossibleMove = true;

    /// <summary>
    /// Сохраняет значение Time.time, когда была нажата кнопка движения в последний раз
    /// </summary>
    private float lastTime = 0;

    //======================================

    private void Awake()
    {
      inputHandler = InputHandler.Instance;

      levelManager = LevelManager.Instance;

      if (!GridEditor.GridEditor.GridEditorEnabled)
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
      if (GridEditor.GridEditor.GridEditorEnabled)
        return;

      PlayerMovement();
    }

    //======================================

    /// <summary>
    /// Движение игрока
    /// </summary>
    private void PlayerMovement()
    {
      axisMovement = inputHandler.GetMove();
      axisMovement.Normalize();

      if (axisMovement.sqrMagnitude > 0.5f)
      {
        if (Time.time - lastTime >= _delayTimeNextButtonPress)
        {
          Vector3 direction = new Vector3(axisMovement.x, 0.0f, axisMovement.y);
          Move(direction);
          lastTime = Time.time;
        }
      }
      else
      {
        lastTime = 0;
      }
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
          if (hit.collider.GetComponent<Block>().GetObjectType() == TypeObject.staticObject)
            return true;

          if (hit.collider.TryGetComponent(out DynamicObjects dynamicObject))
          {
            if (dynamicObject.GetObjectType() == TypeObject.dynamicObject)
            {
              return !dynamicObject.ObjectMove(direction);
            }
          }

          if (hit.collider.GetComponent<Block>().GetObjectType() == TypeObject.interactiveObject)
            return false;
        }

        return true;
      }

      return false;
    }

    /// <summary>
    /// True, если перед игрок есть земля
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

    //======================================

    /// <summary>
    /// Можно ли двигаться
    /// </summary>
    private void PossibleMove(bool parValue)
    {
      isPossibleMove = !parValue;
    }

    //======================================
  }
}