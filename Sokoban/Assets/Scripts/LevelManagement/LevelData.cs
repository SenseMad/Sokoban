using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.LevelManagement
{
  /// <summary>
  /// Класс хранящий данные об уровне
  /// </summary>
  [System.Serializable]
  [CreateAssetMenu(fileName = "New Level Data", menuName = "Data/Level Data", order = 51)]
  public class LevelData : ScriptableObject
  {
    [SerializeField, Tooltip("Номер уровня")]
    private int _levelNumber;
    [SerializeField, Tooltip("Локация уровня")]
    private Location _location;

    [SerializeField, Tooltip("Размер поля")]
    private Vector3Int _fieldSize;

    [SerializeField, Tooltip("Список объектов уровня")]
    private List<GridData> _listLevelObjects;

    //======================================

    /// <summary>
    /// Номер уровня
    /// </summary>
    public int LevelNumber { get => _levelNumber; set => _levelNumber = value; }
    /// <summary>
    /// Локация уровня
    /// </summary>
    public Location Location { get => _location; set => _location = value; }

    /// <summary>
    /// Список объектов уровня
    /// </summary>
    public List<GridData> ListLevelObjects { get => _listLevelObjects; set => _listLevelObjects = value; }

    /// <summary>
    /// Размер поля
    /// </summary>
    public Vector3Int FieldSize { get => _fieldSize; set => _fieldSize = value; }

    //======================================

    [System.Serializable]
    public class GridData
    {
      [SerializeField, Tooltip("Тип объекта")]
      private TypeObject _typeObject;

      [SerializeField, Tooltip("Индекс объекта")]
      private int _indexObject;

      [SerializeField, Tooltip("Позиция объекта")]
      private Vector3Int _positionObject;

      //======================================

      /// <summary>
      /// Тип объекта
      /// </summary>
      public TypeObject TypeObject { get => _typeObject; set => _typeObject = value; }

      /// <summary>
      /// Индекс объекта
      /// </summary>
      public int IndexObject { get => _indexObject; set => _indexObject = value; }

      /// <summary>
      /// Позиция объекта
      /// </summary>
      public Vector3Int PositionObject { get => _positionObject; set => _positionObject = value; }

      //======================================
    }

    //======================================
  }
}