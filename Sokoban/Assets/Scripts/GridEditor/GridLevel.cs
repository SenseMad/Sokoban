using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sokoban.LevelManagement;
using System.Collections;

namespace Sokoban.GridEditor
{
  public class GridLevel : MonoBehaviour
  {
    [SerializeField, Tooltip("Список типов блочных объектов")]
    private ListBlockObjectTypes _listBlockObjectTypes;

    [SerializeField, Tooltip("")]
    private AnimationCurve animationCurve;

    //--------------------------------------

    private StatesLevel statesLevel;

    private Coroutine myCoroutine;

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

    /// <summary>
    /// Список объектов дверей
    /// </summary>
    private List<DoorObject> listDoorObjects = new List<DoorObject>();

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

    /// <summary>
    /// Событие: Уровень удален
    /// </summary>
    public UnityEvent OnLevelDeleted { get; } = new UnityEvent();

    //======================================

    private void Start()
    {
      levelManager = LevelManager.Instance;

      transform.localPosition = new Vector3(0, -2, 0);
    }

    //======================================

    #region Поиск еды на уровне

    /// <summary>
    /// Найти всю еду на уровне
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
    /// Получить список объектов еды на уровне
    /// </summary>
    public List<FoodObject> GetListFoodObjects()
    {
      return listFoodObjects;
    }

    #endregion

    #region Поиск объектов дверей на уровне
    
    /// <summary>
    /// Найти все объекты дверей на уровне
    /// </summary>
    private void FindAllDoorObjects()
    {
      if (blockObjects == null)
        return;

      listDoorObjects = new List<DoorObject>();

      foreach (var blockObject in blockObjects)
      {
        if (blockObject == null)
          continue;

        if (!blockObject.TryGetComponent<DoorObject>(out var doorObject))
          continue;

        listDoorObjects.Add(doorObject);
      }
    }

    /// <summary>
    /// Получить список объектов дверей на уровне
    /// </summary>
    public List<DoorObject> GetListDoorObjects()
    {
      return listDoorObjects;
    }

    #endregion

    //======================================

    /// <summary>
    /// Создание сетки уровня
    /// </summary>
    public void CreatingLevelGrid()
    {
      DeletingLevelObjects();

      StartCoroutine(CreateLevel());
    }

    /// <summary>
    /// Удаление объектов уровня
    /// </summary>
    public void DeletingLevelObjects()
    {
      if (blockObjects == null)
        return;

      myCoroutine = StartCoroutine(DeleteLevel());
    }

    /// <summary>
    /// Создание уровня
    /// </summary>
    private IEnumerator CreateLevel()
    {
      while (myCoroutine != null)
      {
        yield return null;
      }

      statesLevel = StatesLevel.Created;
      float timer = 0f;

      LevelData levelData = levelManager.GetCurrentLevelData();
      blockObjects = new Block[levelData.FieldSize.x, levelData.FieldSize.y, levelData.FieldSize.z];

      foreach (var levelObject in levelManager.GetCurrentLevelData().ListLevelObjects)
      {
        Block newBlockObject = Instantiate(_listBlockObjectTypes.GetBlockObject(levelObject.TypeObject, levelObject.IndexObject), transform);

        if (newBlockObject.GetTypeObject() != TypeObject.staticObject)
          newBlockObject.transform.localScale = Vector3.zero;

        newBlockObject.transform.position = levelObject.PositionObject;
        newBlockObject.SetPositionObject(levelObject.PositionObject);
        blockObjects[levelObject.PositionObject.x, levelObject.PositionObject.y, levelObject.PositionObject.z] = newBlockObject;
      }

      while (timer < 1)
      {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / 1);

        transform.localPosition = new Vector3(0, -2, 0) * animationCurve.Evaluate(1 - t);

        yield return null;
      }

      timer = 0f;

      while (timer < 1)
      {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / 1);

        foreach (var block in blockObjects)
        {
          if (block == null)
            continue;

          if (block.GetTypeObject() == TypeObject.staticObject)
            continue;

          block.transform.localScale = Vector3.one * animationCurve.Evaluate(t);
        }

        yield return null;
      }

      foreach (var block in blockObjects)
      {
        if (block == null)
          continue;

        block.GetBoxCollider().enabled = true;

        if (block.TryGetComponent(out DynamicObjects dynamicObjects))
        {
          dynamicObjects.rigidbody.useGravity = true;
        }

        if (block.TryGetComponent(out PlayerObjects playerObjects))
        {
          playerObjects.rigidbody.useGravity = true;
        }
      }

      FindAllFoodObjects();
      FindAllDoorObjects();
      statesLevel = StatesLevel.Completed;
      OnLevelCreated?.Invoke();
      levelManager.IsLevelStarted = true;
    }

    /// <summary>
    /// Удаление уровня
    /// </summary>
    private IEnumerator DeleteLevel()
    {
      if (blockObjects == null)
      {
        myCoroutine = null;
        yield break;
      }

      levelManager.IsLevelStarted = false;
      statesLevel = StatesLevel.Deleted;
      float timer = 0f;

      while (timer < 1)
      {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / 1);

        foreach (var block in blockObjects)
        {
          if (block == null)
            continue;

          if (block.GetTypeObject() == TypeObject.staticObject)
            continue;

          if (block.GetTypeObject() == TypeObject.dynamicObject || block.GetTypeObject() == TypeObject.playerObject)
            block.RemoveRigidbody();

          block.transform.localScale = Vector3.one * animationCurve.Evaluate(1 - t);
        }

        yield return null;
      }

      timer = 0;

      while (timer < 1)
      {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / 1);

        transform.localPosition = new Vector3(0, -2, 0) * animationCurve.Evaluate(t);
        yield return null;
      }

      foreach (var block in blockObjects)
      {
        if (block == null)
          continue;

        Destroy(block.gameObject);
      }

      myCoroutine = null;
      blockObjects = null;
    }

    /// <summary>
    /// True, если уровень Создается/Удаляется
    /// </summary>
    public bool GetStatesLevel()
    {
      return statesLevel == StatesLevel.Created || statesLevel == StatesLevel.Deleted;
    }

    //======================================

    /// <summary>
    /// Состояние уровня
    /// </summary>
    public enum StatesLevel
    {
      /// <summary>
      /// Уровень создан
      /// </summary>
      Completed,
      /// <summary>
      /// Уровень создается
      /// </summary>
      Created,
      /// <summary>
      /// Уровень удаляется
      /// </summary>
      Deleted
    }

    //======================================
  }
}