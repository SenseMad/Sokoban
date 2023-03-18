using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using LevelManagement;

namespace Sokoban.GridEditor
{
  public class GridLevel : MonoBehaviour
  {
    [SerializeField, Tooltip("Список типов блочных объектов")]
    private ListBlockObjectTypes _listBlockObjectTypes;

    //======================================

    private LevelManager levelManager;

    /// <summary>
    /// Объекты блоков
    /// </summary>
    private Block[,,] blockObjects;

    /// <summary>
    /// Список объектов еды
    /// </summary>
    private List<FoodObject> listFoodObjects = new List<FoodObject>();

    //======================================

    /// <summary>
    /// Получение объектов сетки
    /// </summary>
    public Block[,,] GetBlockObjects() => blockObjects;

    //======================================

    /// <summary>
    /// Событие: Уровень создан
    /// </summary>
    public UnityEvent OnLevelCreated { get; } = new UnityEvent();

    //======================================

    private void Start()
    {
      levelManager = (LevelManager)LevelManager.Instance;

      blockObjects = new Block[0, 0, 0];

      //CreatingLevelGrid();
    }

    //======================================

    #region Поиск еды на уровне

    /// <summary>
    /// Найти всю еду на уровне
    /// </summary>
    private void FindAllFoodObjects()
    {
      listFoodObjects = new List<FoodObject>();

      if (blockObjects != null)
      {
        foreach (var blockObject in blockObjects)
        {
          if (blockObject == null)
            continue;

          if (!blockObject.TryGetComponent<FoodObject>(out var foodObject))
            continue;

          listFoodObjects.Add(foodObject);
        }
      }
    }

    /// <summary>
    /// Получить список объектов еды на уровне
    /// </summary>
    public List<FoodObject> GetListFoodObjects()
    {
      return listFoodObjects;
    }

    #endregion

    //======================================

    /// <summary>
    /// Создание сетки уровня
    /// </summary>
    public void CreatingLevelGrid()
    {
      // Если сетка уровня уже создана, удаляем её
      DeletingLevelObjects();

      LevelData levelData = levelManager.GetCurrentLevelData();
      blockObjects = new Block[levelData.FieldSize.x, levelData.FieldSize.y, levelData.FieldSize.z];

      foreach (var levelObject in levelData.ListLevelObjects)
      {
        Block newBlockObject = Instantiate(_listBlockObjectTypes.GetBlockObject(levelObject.TypeObject, 0), transform);
        newBlockObject.transform.position = levelObject.PositionObject;
        newBlockObject.SetPositionObject(levelObject.PositionObject);
        blockObjects[levelObject.PositionObject.x, levelObject.PositionObject.y, levelObject.PositionObject.z] = newBlockObject;

        //yield return new WaitForSeconds(0f);
      }

      OnLevelCreated?.Invoke();
      FindAllFoodObjects();
    }

    /// <summary>
    /// Удаление объектов уровня
    /// </summary>
    public void DeletingLevelObjects()
    {
      if (blockObjects != null)
      {
        foreach (var blockObject in blockObjects)
        {
          if (blockObject != null)
          {
            Destroy(blockObject.gameObject);
          }
        }

        blockObjects = null;
      }
    }

    //======================================
  }
}