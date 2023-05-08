using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sokoban.LevelManagement;

namespace Sokoban.GridEditor
{
  public class GridLevel : MonoBehaviour
  {
    [SerializeField, Tooltip("������ ����� ������� ��������")]
    private ListBlockObjectTypes _listBlockObjectTypes;

    //======================================

    private LevelManager levelManager;

    /// <summary>
    /// ������� ������
    /// </summary>
    private Block[,,] blockObjects;

    /// <summary>
    /// ������ �������� ���
    /// </summary>
    private List<FoodObject> listFoodObjects = new List<FoodObject>();

    //======================================

    /// <summary>
    /// ��������� �������� �����
    /// </summary>
    public Block[,,] GetBlockObjects() => blockObjects;

    //======================================

    /// <summary>
    /// �������: ������� ������
    /// </summary>
    public UnityEvent OnLevelCreated { get; } = new UnityEvent();

    //======================================

    private void Start()
    {
      levelManager = LevelManager.Instance;
    }

    //======================================

    #region ����� ��� �� ������

    /// <summary>
    /// ����� ��� ��� �� ������
    /// </summary>
    private void FindAllFoodObjects()
    {
      if (blockObjects == null)
        return;

      listFoodObjects = new List<FoodObject>();

      foreach (var blockObject in blockObjects)
      {
        if (blockObject == null)
          continue;

        if (!blockObject.TryGetComponent<FoodObject>(out var foodObject))
          continue;

        listFoodObjects.Add(foodObject);
      }
    }

    /// <summary>
    /// �������� ������ �������� ��� �� ������
    /// </summary>
    public List<FoodObject> GetListFoodObjects()
    {
      return listFoodObjects;
    }

    #endregion

    //======================================

    /// <summary>
    /// �������� ����� ������
    /// </summary>
    public void CreatingLevelGrid()
    {
      // ���� ����� ������ ��� �������, ������� �
      DeletingLevelObjects();

      LevelData levelData = levelManager.GetCurrentLevelData();
      blockObjects = new Block[levelData.FieldSize.x, levelData.FieldSize.y, levelData.FieldSize.z];

      foreach (var levelObject in levelData.ListLevelObjects)
      {
        Block newBlockObject = Instantiate(_listBlockObjectTypes.GetBlockObject(levelObject.TypeObject, levelObject.IndexObject), transform);
        newBlockObject.transform.position = levelObject.PositionObject;
        newBlockObject.SetPositionObject(levelObject.PositionObject);
        blockObjects[levelObject.PositionObject.x, levelObject.PositionObject.y, levelObject.PositionObject.z] = newBlockObject;

        //yield return new WaitForSeconds(0f);
      }

      FindAllFoodObjects();
      OnLevelCreated?.Invoke();
    }

    /// <summary>
    /// �������� �������� ������
    /// </summary>
    public void DeletingLevelObjects()
    {
      if (blockObjects == null)
        return;

      foreach (var blockObject in blockObjects)
      {
        if (blockObject == null)
          continue;

        Destroy(blockObject.gameObject);
      }

      blockObjects = new Block[0, 0, 0];
    }

    //======================================
  }
}