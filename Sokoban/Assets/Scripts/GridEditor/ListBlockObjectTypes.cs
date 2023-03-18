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
    /// �������� ������ ����� �� ���� � ������� �������
    /// </summary>
    /// <param name="parTypeObject">��� �������</param>
    /// <param name="parIndex">������ �������</param>
    public Block GetBlockObject(TypeObject parTypeObject, int parIndex)
    {
      if (parIndex > _listBlockObjectTypes.Count)
      {
        Debug.LogError("������ ����� �� ������� �������!");
        return null;
      }

      for (int i = 0; i < _listBlockObjectTypes.Count; i++)
      {
        if (_listBlockObjectTypes[i].GetTypeObjects != parTypeObject)
          continue;

        return _listBlockObjectTypes[i].GetBlockObject(parIndex);
      }

      return null;
    }

    //======================================
  }
}