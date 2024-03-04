using UnityEngine;

/// <summary>
/// Объект двери
/// </summary>
public class DoorObject : StaticObjects
{
  [SerializeField, Tooltip("Цвет двери")]
  private DoorColor _doorColor;

  [SerializeField] private GameObject _meshGameObject;

  [SerializeField, Tooltip("Открытая дверь")]
  private GameObject _openDoorMesh;
  [SerializeField, Tooltip("Закрытая дверь")]
  private GameObject _closedDoorMesh;

  //======================================

  /// <summary>
  /// Получить цвет двери
  /// </summary>
  public DoorColor GetDoorColor() => _doorColor;

  /// <summary>
  /// Открытая дверь
  /// </summary>
  public GameObject OpenDoorMesh { get => _openDoorMesh; private set => _openDoorMesh = value; }
  /// <summary>
  /// Закрытая дверь
  /// </summary>
  public GameObject ClosedDoorMesh { get => _closedDoorMesh; private set => _closedDoorMesh = value; }

  public GameObject MeshGameObject { get => _meshGameObject; private set => _meshGameObject = value; }

  //======================================

  protected override void Awake()
  {
    base.Awake();

    MeshGameObject.SetActive(true);
    /*OpenDoorMesh.SetActive(true);
    ClosedDoorMesh.SetActive(false);*/
  }

  //======================================
}