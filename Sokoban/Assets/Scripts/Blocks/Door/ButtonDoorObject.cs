using UnityEngine;

using Sokoban.LevelManagement;

public class ButtonDoorObject : InteractiveObjects
{
  [SerializeField] private DoorColor _buttonDoorColor;

  [SerializeField] private GameObject _buttonGameObject;

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
    if (!other.TryGetComponent(out Block block))
      return;

    if (block.GetTypeObject() != TypeObject.playerObject && block.GetTypeObject() != TypeObject.dynamicObject)
      return;

    foreach (var doorObject in levelManager.GridLevel.GetListDoorObjects())
    {
      if (doorObject.GetDoorColor() != _buttonDoorColor || !doorObject.BoxCollider.enabled)
        continue;

      doorObject.BoxCollider.enabled = false;
      doorObject.MeshGameObject.SetActive(false);
      /*doorObject.OpenDoorMesh.SetActive(false);
      doorObject.ClosedDoorMesh.SetActive(true);*/
      _buttonGameObject.SetActive(false);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    foreach (var doorObject in levelManager.GridLevel.GetListDoorObjects())
    {
      if (doorObject.GetDoorColor() != _buttonDoorColor || doorObject.BoxCollider.enabled)
        continue;

      doorObject.BoxCollider.enabled = true;
      doorObject.MeshGameObject.SetActive(true);
      /*doorObject.OpenDoorMesh.SetActive(true);
      doorObject.ClosedDoorMesh.SetActive(false);*/
      _buttonGameObject.SetActive(true);
    }
  }

  //======================================
}