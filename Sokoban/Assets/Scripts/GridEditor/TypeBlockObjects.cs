using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.GridEditor
{
  [CreateAssetMenu(fileName = "New Type Block Objects", menuName = "Data/Type Block Objects", order = 51)]
  public class TypeBlockObjects : ScriptableObject
  {
    [SerializeField, Tooltip("Тип объектов")]
    private TypeObject _typeObjects;

    [SerializeField, Tooltip("Список блочных объектов")]
    private List<Block> _listBlockObjects = new List<Block>();

    //======================================

    /// <summary>
    /// Тип объектов
    /// </summary>
    public TypeObject GetTypeObjects { get => _typeObjects; }

    //======================================

    /// <summary>
    /// Получить объект блока по индексу
    /// </summary>
    /// <param name="parIndex">Индекс блока</param>
    public Block GetBlockObject(int parIndex)
    {
      if (parIndex > _listBlockObjects.Count - 1)
      {
        Debug.LogError("Индекс вышел за пределы массива!");
        return null;
      }

      return _listBlockObjects[parIndex];
    }

    //======================================
  }
}