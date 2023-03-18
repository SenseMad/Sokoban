using UnityEngine;

namespace Sokoban.GridEditor
{
  public class GridObject : MonoBehaviour
  {
    [SerializeField, Tooltip("Тип объекта")]
    private TypeObject _typeObject;

    [SerializeField, Tooltip("Позиция объекта")]
    private Vector3Int _positionObject;

    //--------------------------------------

    private Rigidbody rigidbody;

    //======================================

    /// <summary>
    /// Тип объекта
    /// </summary>
    public TypeObject TypeObject { get => _typeObject; set => _typeObject = value; }

    /// <summary>
    /// Позиция объекта
    /// </summary>
    public Vector3Int PositionObject { get => _positionObject; set => _positionObject = value; }

    //======================================

    private void Awake()
    {
      if (_typeObject == TypeObject.playerObject || _typeObject == TypeObject.dynamicObject)
      {
        rigidbody = GetComponent<Rigidbody>();
      }
    }

    //======================================

    /// <summary>
    /// Удалить Rigidbody
    /// </summary>
    public void RemoveRigidbody()
    {
      if (rigidbody != null)
      {
        Destroy(rigidbody);
      }
    }

    /// <summary>
    /// Вкл/Выкл гравитацию у объекта
    /// </summary>
    /// <param name="parValue">Значение</param>
    public void GravityObject(bool parValue)
    {
      if (rigidbody == null)
        return;

      rigidbody.useGravity = parValue;
    }

    //======================================
  }
}