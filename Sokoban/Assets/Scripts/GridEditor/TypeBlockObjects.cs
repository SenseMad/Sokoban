using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Sokoban.GridEditor
{
  [CreateAssetMenu(fileName = "New Type Block Objects", menuName = "Data/Type Block Objects", order = 51)]
  public class TypeBlockObjects : ScriptableObject
  {
    [SerializeField, Tooltip("��� �������� �� ������")]
    private TypeObject _typeObjects;

    [SerializeField, Tooltip("������ ������� ��������")]
    private List<Block> _listBlockObjects = new List<Block>();

    //======================================

    /// <summary>
    /// �������� ��� �������� �� ������
    /// </summary>
    public TypeObject GetTypeObjects => _typeObjects;

    //======================================

    /// <summary>
    /// �������� ������ ����� �� ������
    /// </summary>
    /// <param name="parObjectIndex">������ �������</param>
    public Block GetBlockObjectFromList(int parObjectIndex)
    {
      if (parObjectIndex > _listBlockObjects.Count - 1)
      {
        Debug.LogError("������ ����� �� ������� �������!");
        return null;
      }

      return _listBlockObjects[parObjectIndex];
    }

    //======================================
  }
}