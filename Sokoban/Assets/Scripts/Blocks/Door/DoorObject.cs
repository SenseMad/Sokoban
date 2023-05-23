using UnityEngine;

/// <summary>
/// Объект двери
/// </summary>
public class DoorObject : StaticObjects
{
  [SerializeField, Tooltip("Цвет двери")]
  private DoorColor _doorColor;

  //======================================

  /// <summary>
  /// Получить цвет двери
  /// </summary>
  public DoorColor GetDoorColor() => _doorColor;

  //======================================
}