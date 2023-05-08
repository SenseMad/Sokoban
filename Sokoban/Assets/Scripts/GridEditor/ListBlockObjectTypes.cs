using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.GridEditor
{
  /// <summary>
  /// ������ ����� ������� ��������
  /// </summary>
  [CreateAssetMenu(fileName = "New List Block Object Types", menuName = "Data/List Block Object Types")]
  public class ListBlockObjectTypes : ScriptableObject
  {
    [SerializeField, Tooltip("������ ����� ������� ��������")]
    private List<TypeBlockObjects> _listBlockObjectTypes = new List<TypeBlockObjects>();

    //======================================

    /// <summary>
    /// �������� ������ �����
    /// </summary>
    /// <param name="parTypeObject">��� �������</param>
    /// <param name="parObjectIndex">������ �������</param>
    public Block GetBlockObject(TypeObject parTypeObject, int parObjectIndex)
    {
      for (int i = 0; i < _listBlockObjectTypes.Count; i++)
      {
        if (_listBlockObjectTypes[i].GetTypeObjects != parTypeObject)
          continue;

        return _listBlockObjectTypes[i].GetBlockObjectFromList(parObjectIndex);
      }

      return null;
    }

    //======================================
  }
}