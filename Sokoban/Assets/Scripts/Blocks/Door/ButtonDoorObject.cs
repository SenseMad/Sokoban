using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.LevelManagement;

/// <summary>
/// Объект кнопки для двери
/// </summary>
public class ButtonDoorObject : InteractiveObjects
{
  [SerializeField, Tooltip("Цвет кнопки двери")]
  private DoorColor _buttonDoorColor;

  //--------------------------------------

  private LevelManager levelManager;

  //======================================

  private void Awake()
  {
    levelManager = LevelManager.Instance;

    //typeObject = TypeObject.buttonDoorObject;
  }

  private void Start()
  {
    
  }

  //======================================

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Block>().GetTypeObject() == TypeObject.playerObject || other.GetComponent<Block>().GetTypeObject() == TypeObject.dynamicObject)
    {
      var listDoorObjects = levelManager.GridLevel.GetListDoorObjects();
      for (int i = 0; i < listDoorObjects.Count; i++)
      {
        if (listDoorObjects[i].GetDoorColor() != _buttonDoorColor || !listDoorObjects[i].gameObject.activeSelf)
          continue;

        listDoorObjects[i].gameObject.SetActive(false);
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    var listDoorObjects = levelManager.GridLevel.GetListDoorObjects();
    for (int i = 0; i < listDoorObjects.Count; i++)
    {
      if (listDoorObjects[i].GetDoorColor() != _buttonDoorColor || listDoorObjects[i].gameObject.activeSelf)
        continue;

      listDoorObjects[i].gameObject.SetActive(true);
    }
  }

  //======================================
}