using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : SingletonInGame<InputHandler>
{
  public AI_Player AI_Player { get; private set; }

  //======================================

  private new void Awake()
  {
    AI_Player = new AI_Player();
  }

  private void OnEnable()
  {
    AI_Player.Enable();
  }

  private void OnDisable()
  {
    AI_Player.Disable();
  }

  //======================================

  /// <summary>
  /// True, если можно использовать ввод данных
  /// </summary>
  public bool CanProcessInput()
  {
    return AI_Player != null;
  }

  //======================================

  /// <summary>
  /// Получит движение
  /// </summary>
  public Vector2 GetMove()
  {
    return CanProcessInput() ? AI_Player.Player.Move.ReadValue<Vector2>() : Vector2.zero;
  }

  /// <summary>
  /// Получить поворот
  /// </summary>
  public Vector2 GetLook()
  {
    return CanProcessInput() ? AI_Player.Camera.Look.ReadValue<Vector2>() : Vector2.zero;
  }

  #region Камера
  /// <summary>
  /// Получить кнопку меделеного полета камеры
  /// </summary>
  public bool GetButtonSlowCamera()
  {
    return CanProcessInput() ? AI_Player.Camera.SlowCameraSpeed.ReadValue<float>() == 1 : false;
  }

  /// <summary>
  /// Получить кнопку быстрого полета камеры
  /// </summary>
  public bool GetButtonFastCamera()
  {
    return CanProcessInput() ? AI_Player.Camera.FastCameraSpeed.ReadValue<float>() == 1 : false;
  }
  #endregion

  #region UI

  /// <summary>
  /// Получить кнопки навигации (По вертикали)
  /// </summary>
  public float GetNavigationInput()
  {
    return AI_Player != null ? AI_Player.Player.Move.ReadValue<Vector2>().y : 0f;
  }

  /// <summary>
  /// Получить кнопки изменения значений (По горизонтали)
  /// </summary>
  public float GetChangingValuesInput()
  {
    return AI_Player != null ? AI_Player.Player.Move.ReadValue<Vector2>().x : 0f;
  }

  /// <summary>
  /// Получить кнопку паузы
  /// </summary>
  /*public bool GetButtonPause()
  {
    return CanProcessInput() ? AI_Player.UI.Pause.ReadValue<float>() == 1 : false;
  }

  /// <summary>
  /// Получить кнопку перезагрузки
  /// </summary>
  public bool GetButtonReload()
  {
    return CanProcessInput() ? AI_Player.UI.Reload.ReadValue<float>() == 1 : false;
  }*/
  #endregion

  //======================================
}