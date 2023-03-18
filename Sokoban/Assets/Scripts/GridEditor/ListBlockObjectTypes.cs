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
    /// Получить объект блока по типу и индексу объекта
    /// </summary>
    /// <param name="parTypeObject">Тип объекта</param>
    /// <param name="parIndex">Индекс объекта</param>
    public Block GetBlockObject(TypeObject parTypeObject, int parIndex)
    {
      if (parIndex > _listBlockObjectTypes.Count)
      {
        Debug.LogError("Индекс вышел за пределы массива!");
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