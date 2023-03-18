using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

using LevelManagement;

namespace Sokoban.GridEditor
{
  public class GridEditor : MonoBehaviour
  {
    /// <summary>
    /// True, ���� ������� �������� �����
    /// </summary>
    public static bool GridEditorEnabled = false;

    [Header("���������")]
    [SerializeField, Tooltip("������ ����")]
    private Vector3Int _fieldSize;
    [SerializeField, Tooltip("������ ������")]
    private int _gridSize = 1;
    [SerializeField, Tooltip("������� ����")]
    private int _gridLevel = 0;

    [Header("�������")]
    [SerializeField, Tooltip("������� ��������� ��� �������")]
    private TypeObject _typeObject;
    [SerializeField, Tooltip("������ ���������� �������")]
    private int _indexObject = 0;
    [SerializeField, Tooltip("������ ����� ������� ��������")]
    private ListBlockObjectTypes _listBlockObjectsTypes;

    [Header("��������� ������")]
    [SerializeField, Tooltip("������� ��� ������� ������� �������")]
    private Location _location;

    //--------------------------------------

    private Block[,,] blockObjects;

    /// <summary>
    /// True, ���� ����� ������
    /// </summary>
    public bool hideGrid;

    /// <summary>
    /// True, ���� ���������� �� ������ ������
    /// </summary>
    public bool DisplayLevel { get; set; }

    /// <summary>
    /// ����� ��������������
    /// </summary>
    public bool EditingMode;
    /// <summary>
    /// ����� ����������� ������
    /// </summary>
    public bool CameraMovementMode;

    private BoxCollider boxCollider;

    //======================================

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    public Vector3Int GetFieldSize() => _fieldSize;

    /// <summary>
    /// �������� ������� ����
    /// </summary>
    public int GetGridLevel() => _gridLevel;

    //======================================

    private void Awake()
    {
      boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
      GridEditorEnabled = true;

      blockObjects = new Block[_fieldSize.x, _fieldSize.y, _fieldSize.z];

      ChangeSizeBoxCollider();
    }

    private void Update()
    {
      if (CameraMovementMode)
        return;

      if (Mouse.current.leftButton.wasPressedThisFrame)  // Mouse.current.leftButton.IsPressed()
      {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
          GetXYZ(hit.point, out Vector3Int position);
          RemoveObject(position);
          AddObject(position, _listBlockObjectsTypes.GetBlockObject(_typeObject, _indexObject));
        }
      }

      if (Mouse.current.rightButton.wasPressedThisFrame) // Mouse.current.rightButton.IsPressed()
      {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
          GetXYZ(hit.point, out Vector3Int position);
          RemoveObject(position);
        }
      }
    }

    //======================================

    /// <summary>
    /// �������� ������
    /// </summary>
    private void AddObject(Vector3Int position, Block blockObject)
    {
      Block newBlockObject = Instantiate(blockObject, transform);

      newBlockObject.RemoveRigidbody();
      newBlockObject.transform.position = position;
      newBlockObject.SetPositionObject(position);
      //newBlockObject.PositionObject = position;

      blockObjects[position.x, position.y, position.z] = newBlockObject;
    }

    /// <summary>
    /// ������� ������
    /// </summary>
    private void RemoveObject(Vector3Int position)
    {
      if (blockObjects[position.x, position.y, position.z] == null)
        return;

      Destroy(blockObjects[position.x, position.y, position.z].gameObject);
      blockObjects[position.x, position.y, position.z] = null;
    }

    /// <summary>
    /// �������� ��������� ���������� XYZ
    /// </summary>
    /// <param name="worldPosition">������� ������� ����</param>
    /// <param name="vector3Int">��������� ������� ����</param>
    private void GetXYZ(Vector3 worldPosition, out Vector3Int vector3Int)
    {
      vector3Int = new Vector3Int
      {
        x = Mathf.RoundToInt((worldPosition.x - transform.position.x) / _gridSize),
        y = _gridLevel,
        z = Mathf.RoundToInt((worldPosition.z - transform.position.z) / _gridSize)
      };

      if (vector3Int.x > _fieldSize.x - 1) vector3Int.x -= 1;
      if (vector3Int.z > _fieldSize.z - 1) vector3Int.z -= 1;
    }

    private void ChangeSizeBoxCollider()
    {
      boxCollider.size = new Vector3(_fieldSize.x, 1, _fieldSize.z) * _gridSize;
      boxCollider.center = (boxCollider.size - new Vector3(_gridSize, _gridSize, _gridSize)) * 0.5f;
      boxCollider.center += new Vector3(0, _gridLevel, 0);
    }

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    public void ChangeFieldSize(Vector3Int fieldSize)
    {
      _fieldSize = fieldSize;

      var tempBlockObjects = blockObjects;
      blockObjects = new Block[_fieldSize.x, _fieldSize.y, _fieldSize.z];
      if (blockObjects.Length > tempBlockObjects.Length)
      {
        for (int i = 0; i < tempBlockObjects.GetLength(0); i++)
        {
          for (int j = 0; j < tempBlockObjects.GetLength(1); j++)
          {
            for (int k = 0; k < tempBlockObjects.GetLength(2); k++)
            {
              blockObjects[i, j, k] = tempBlockObjects[i, j, k];
            }
          }
        }
      }
      else
      {
        for (int i = 0; i < blockObjects.GetLength(0); i++)
        {
          for (int j = 0; j < blockObjects.GetLength(1); j++)
          {
            for (int k = 0; k < blockObjects.GetLength(2); k++)
            {
              blockObjects[i, j, k] = tempBlockObjects[i, j, k];
            }
          }
        }
      }

      if (_gridLevel + 1 > _fieldSize.y)
        _gridLevel = _fieldSize.y - 1;

      ChangeSizeBoxCollider();
    }

    /// <summary>
    /// �������� ������� ����
    /// </summary>
    public void ChangeGridLevel(bool parValue)
    {
      ShowHideSublevels(false);

      if (parValue && _gridLevel + 1 <= _fieldSize.y - 1)
        _gridLevel++;
      else if (!parValue && _gridLevel - 1 >= 0)
        _gridLevel--;

      ShowHideSublevels(true);

      ChangeSizeBoxCollider();
    }

    /// <summary>
    /// ����������/������ ����������
    /// </summary>
    public void ShowHideSublevels(bool parValue)
    {
      if (!DisplayLevel)
        return;

      for (int i = 0; i < blockObjects.GetLength(0); i++)
      {
        for (int k = 0; k < blockObjects.GetLength(2); k++)
        {
          if (blockObjects[i, _gridLevel, k] != null)
            blockObjects[i, _gridLevel, k].gameObject.SetActive(parValue);
        }
      }
    }

    /// <summary>
    /// ����������/������ ��� ���������
    /// </summary>
    public void ShowHideAllSublevels(bool parValue)
    {
      for (int i = 0; i < blockObjects.GetLength(0); i++)
      {
        for (int j = 0; j < blockObjects.GetLength(1); j++)
        {
          for (int k = 0; k < blockObjects.GetLength(2); k++)
          {
            if (blockObjects[i, j, k] != null)
            {
              if (parValue)
              {
                blockObjects[i, j, k].gameObject.SetActive(true);
              }
              else if (!parValue && j != _gridLevel)
              {
                blockObjects[i, j, k].gameObject.SetActive(false);
              }
            }
          }
        }
      }
    }

    //======================================

    /// <summary>
    /// �������� ������ ������
    /// </summary>
    public void CreateLevelData()
    {
      LevelData levelData = ScriptableObject.CreateInstance<LevelData>();

      int numLevel = 1;
      string path = Levels.GetPathToStarageLevels(_location, numLevel);

      // ���������, ���������� �� ScriptableObject
      ScriptableObject scriptable = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
      while (scriptable != null)
      {
        numLevel++;
        path = Levels.GetPathToStarageLevels(_location, numLevel);
        scriptable = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
      }

      levelData.name = $"{_location}_{numLevel}";
      levelData.LevelNumber = numLevel;

      levelData.ListLevelObjects = new List<LevelData.GridData>();

      foreach (var blockObject in blockObjects)
      {
        if (blockObject != null)
        {
          levelData.ListLevelObjects.Add(new LevelData.GridData()
          {
            TypeObject = blockObject.GetObjectType(),
            PositionObject = blockObject.GetObjectPosition()
          });
        }
      }

      AssetDatabase.CreateAsset(levelData, path);
      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
    }

    //======================================

    private void OnDrawGizmos()
    {
      if (hideGrid)
        return;

      Gizmos.color = Color.white;

      for (int x = 0; x < _fieldSize.x; x++)
      {
        for (int y = 0; y < _fieldSize.z; y++)
        {
          Gizmos.DrawWireCube(transform.position + new Vector3(x * _gridSize, _gridLevel, y * _gridSize), new Vector3(_gridSize, _gridSize, _gridSize));
        }
      }
    }

    //======================================
  }
}