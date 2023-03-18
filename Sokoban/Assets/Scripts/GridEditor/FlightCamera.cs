using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace Sokoban.GridEditor
{
  /// <summary>
  /// Полет камеры во время создания уровня
  /// </summary>
  public class FlightCamera : MonoBehaviour
  {
    [Header("СКОРОСТИ ДВИЖЕНИЯ")]
    [SerializeField, Tooltip("Замедленная скорость камеры")]
    private float _slowCameraSpeed = 5f;
    [SerializeField, Tooltip("Обычная скорость камеры")]
    private float _normalCameraSpeed = 10f;
    [SerializeField, Tooltip("Быстрая скорость камеры")]
    private float _fastCameraSpeed = 20f;

    [Header("ВРАЩЕНИЕ КАМЕРЫ")]
    [SerializeField, Tooltip("Скорость вращения камеры")]
    private float _rotationSpeed = 3f;

    //--------------------------------------

    private CinemachineVirtualCamera virtualCamera;
    private GridEditor gridEditor;

    private Vector2 axisMovement;
    private Vector2 axisLook;

    private float mouseX, mouseY;

    private bool slowCamera;
    private bool fastCamera;

    //======================================

    private void Awake()
    {
      virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
      gridEditor = FindObjectOfType<GridEditor>();
    }

    private void Update()
    {
      Move();

      if (gridEditor.EditingMode)
        return;

      RotateCamera();
    }

    //======================================

    private void Move()
    {
      float speed = fastCamera ? _fastCameraSpeed : (!slowCamera ? _normalCameraSpeed : _slowCameraSpeed);
      Vector3 moveDirection = new Vector3(axisMovement.x, 0, axisMovement.y) * speed * Time.deltaTime;
      virtualCamera.transform.Translate(moveDirection, Space.Self);
    }

    private void RotateCamera()
    {
      if (Mouse.current.rightButton.IsPressed())
      {
        mouseX += axisLook.x * _rotationSpeed;
        mouseY -= axisLook.y * _rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -89f, 89f);

        virtualCamera.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
      }
    }

    //======================================

    /// <summary>
    /// Поворот камеры
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
      axisLook = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Движение
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
      axisMovement = context.ReadValue<Vector2>();
    }

    public void OnSlowCamera(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Started:
          slowCamera = true;
          break;
        case InputActionPhase.Canceled:
          slowCamera = false;
          break;
      }
    }

    public void OnFastCamera(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Started:
          fastCamera = true;
          break;
        case InputActionPhase.Canceled:
          fastCamera = false;
          break;
      }
    }

    //======================================
  }
}