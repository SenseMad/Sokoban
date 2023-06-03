using UnityEngine;

using Sokoban.LevelManagement;

/// <summary>
/// Объект кнопки для двери
/// </summary>
public class ButtonDoorObject : InteractiveObjects
{
  [SerializeField, Tooltip("Цвет кнопки двери")]
  private DoorColor _buttonDoorColor;

  [SerializeField, Tooltip("")]
  private GameObject _buttonGameObject;

  //--------------------------------------

  private LevelManager levelManager;

  //======================================

  protected override void Awake()
  {
    base.Awake();

    levelManager = LevelManager.Instance;
  }

  //======================================

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Block>().GetTypeObject() == TypeObject.playerObject || other.GetComponent<Block>().GetTypeObject() == TypeObject.dynamicObject)
    {
      var listDoorObjects = levelManager.GridLevel.GetListDoorObjects();
      for (int i = 0; i < listDoorObjects.Count; i++)
      {
        if (listDoorObjects[i].GetDoorColor() != _buttonDoorColor || !listDoorObjects[i].BoxCollider.enabled)
          continue;

        //listDoorObjects[i].gameObject.SetActive(false);
        listDoorObjects[i].BoxCollider.enabled = false;
        listDoorObjects[i].OpenDoorMesh.SetActive(false);
        listDoorObjects[i].ClosedDoorMesh.SetActive(true);
        _buttonGameObject.SetActive(false);
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    var listDoorObjects = levelManager.GridLevel.GetListDoorObjects();
    for (int i = 0; i < listDoorObjects.Count; i++)
    {
      if (listDoorObjects[i].GetDoorColor() != _buttonDoorColor || listDoorObjects[i].BoxCollider.enabled)
        continue;

      //listDoorObjects[i].gameObject.SetActive(true);
      listDoorObjects[i].BoxCollider.enabled = true;
      listDoorObjects[i].OpenDoorMesh.SetActive(true);
      listDoorObjects[i].ClosedDoorMesh.SetActive(false);
      _buttonGameObject.SetActive(true);
    }
  }

  //======================================
}