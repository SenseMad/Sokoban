using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.GridEditor
{
  [CreateAssetMenu(fileName = "New Type Block Objects", menuName = "Data/Type Block Objects", order = 51)]
  public class TypeBlockObjects : ScriptableObject
  {
    [SerializeField, Tooltip("Тип объектов из списка")]
    private TypeObject _typeObjects;

    [SerializeField, Tooltip("Список блочных объектов")]
    private List<Block> _listBlockObjects = new List<Block>();

    //======================================

    /// <summary>
    /// Получить тип объектов из списка
    /// </summary>
    public TypeObject GetTypeObjects => _typeObjects;

    //======================================

    /// <summary>
    /// Получить объект блока из списка
    /// </summary>
    /// <param name="parObjectIndex">Индекс объекта</param>
    public Block GetBlockObjectFromList(int parObjectIndex)
    {
      if (parObjectIndex > _listBlockObjects.Count - 1)
      {
        Debug.LogError("Индекс вышел за пределы массива!");
        return null;
      }

      return _listBlockObjects[parObjectIndex];
    }

    //======================================
  }
}