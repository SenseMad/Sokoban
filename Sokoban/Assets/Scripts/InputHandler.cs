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
  /// True, ���� ����� ������������ ���� ������
  /// </summary>
  public bool CanProcessInput()
  {
    return AI_Player != null;
  }

  //======================================

  /// <summary>
  /// ������� ��������
  /// </summary>
  public Vector2 GetMove()
  {
    return CanProcessInput() ? AI_Player.Player.Move.ReadValue<Vector2>() : Vector2.zero;
  }

  /// <summary>
  /// �������� �������
  /// </summary>
  public Vector2 GetLook()
  {
    return CanProcessInput() ? AI_Player.Camera.Look.ReadValue<Vector2>() : Vector2.zero;
  }

  #region ������
  /// <summary>
  /// �������� ������ ���������� ������ ������
  /// </summary>
  public bool GetButtonSlowCamera()
  {
    return CanProcessInput() ? AI_Player.Camera.SlowCameraSpeed.ReadValue<float>() == 1 : false;
  }

  /// <summary>
  /// �������� ������ �������� ������ ������
  /// </summary>
  public bool GetButtonFastCamera()
  {
    return CanProcessInput() ? AI_Player.Camera.FastCameraSpeed.ReadValue<float>() == 1 : false;
  }
  #endregion

  #region UI

  /// <summary>
  /// �������� ������ ��������� (�� ���������)
  /// </summary>
  public float GetNavigationInput()
  {
    return AI_Player != null ? AI_Player.Player.Move.ReadValue<Vector2>().y : 0f;
  }

  /// <summary>
  /// �������� ������ ��������� �������� (�� �����������)
  /// </summary>
  public float GetChangingValuesInput()
  {
    return AI_Player != null ? AI_Player.Player.Move.ReadValue<Vector2>().x : 0f;
  }

  /// <summary>
  /// �������� ������ �����
  /// </summary>
  /*public bool GetButtonPause()
  {
    return CanProcessInput() ? AI_Player.UI.Pause.ReadValue<float>() == 1 : false;
  }

  /// <summary>
  /// �������� ������ ������������
  /// </summary>
  public bool GetButtonReload()
  {
    return CanProcessInput() ? AI_Player.UI.Reload.ReadValue<float>() == 1 : false;
  }*/
  #endregion

  //======================================
}