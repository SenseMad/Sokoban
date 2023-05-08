using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.LevelManagement
{
  /// <summary>
  /// ����� �������� ������ �� ������
  /// </summary>
  [System.Serializable]
  [CreateAssetMenu(fileName = "New Level Data", menuName = "Data/Level Data", order = 51)]
  public class LevelData : ScriptableObject
  {
    [SerializeField, Tooltip("����� ������")]
    private int _levelNumber;
    [SerializeField, Tooltip("������� ������")]
    private Location _location;

    [SerializeField, Tooltip("������ ����")]
    private Vector3Int _fieldSize;

    [SerializeField, Tooltip("������ �������� ������")]
    private List<GridData> _listLevelObjects;

    //======================================

    /// <summary>
    /// ����� ������
    /// </summary>
    public int LevelNumber { get => _levelNumber; set => _levelNumber = value; }
    /// <summary>
    /// ������� ������
    /// </summary>
    public Location Location { get => _location; set => _location = value; }

    /// <summary>
    /// ������ �������� ������
    /// </summary>
    public List<GridData> ListLevelObjects { get => _listLevelObjects; set => _listLevelObjects = value; }

    /// <summary>
    /// ������ ����
    /// </summary>
    public Vector3Int FieldSize { get => _fieldSize; set => _fieldSize = value; }

    //======================================

    [System.Serializable]
    public class GridData
    {
      [SerializeField, Tooltip("��� �������")]
      private TypeObject _typeObject;

      [SerializeField, Tooltip("������ �������")]
      private int _indexObject;

      [SerializeField, Tooltip("������� �������")]
      private Vector3Int _positionObject;

      //======================================

      /// <summary>
      /// ��� �������
      /// </summary>
      public TypeObject TypeObject { get => _typeObject; set => _typeObject = value; }

      /// <summary>
      /// ������ �������
      /// </summary>
      public int IndexObject { get => _indexObject; set => _indexObject = value; }

      /// <summary>
      /// ������� �������
      /// </summary>
      public Vector3Int PositionObject { get => _positionObject; set => _positionObject = value; }

      //======================================
    }

    //======================================
  }
}