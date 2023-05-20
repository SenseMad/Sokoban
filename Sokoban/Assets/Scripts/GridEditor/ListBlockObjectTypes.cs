using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.GridEditor
{
  /// <summary>
  /// Список типов блочных объектов
  /// </summary>
  [CreateAssetMenu(fileName = "New List Block Object Types", menuName = "Data/List Block Object Types")]
  public class ListBlockObjectTypes : ScriptableObject
  {
    [SerializeField, Tooltip("Список типов блочных объектов")]
    private List<TypeBlockObjects> _listBlockObjectTypes = new List<TypeBlockObjects>();

    //======================================

    /// <summary>
    /// Получить список всех блоков
    /// </summary>
    public List<Block> GetListAllBlock()
    {
      var tempListBlock = new List<Block>();

      foreach (var blockObjectType in _listBlockObjectTypes)
      {
        foreach (var block in blockObjectType.GetListBlockObjects())
        {
          tempListBlock.Add(block);
        }
      }

      return tempListBlock;
    }

    /// <summary>
    /// Получить объект блока
    /// </summary>
    /// <param name="parTypeObject">Тип объекта</param>
    /// <param name="parObjectIndex">Индекс объекта</param>
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