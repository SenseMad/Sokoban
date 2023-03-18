using UnityEngine;

namespace Sokoban.GridEditor
{
  public class GridObject : MonoBehaviour
  {
    [SerializeField, Tooltip("��� �������")]
    private TypeObject _typeObject;

    [SerializeField, Tooltip("������� �������")]
    private Vector3Int _positionObject;

    //--------------------------------------

    private Rigidbody rigidbody;

    //======================================

    /// <summary>
    /// ��� �������
    /// </summary>
    public TypeObject TypeObject { get => _typeObject; set => _typeObject = value; }

    /// <summary>
    /// ������� �������
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
    /// ������� Rigidbody
    /// </summary>
    public void RemoveRigidbody()
    {
      if (rigidbody != null)
      {
        Destroy(rigidbody);
      }
    }

    /// <summary>
    /// ���/���� ���������� � �������
    /// </summary>
    /// <param name="parValue">��������</param>
    public void GravityObject(bool parValue)
    {
      if (rigidbody == null)
        return;

      rigidbody.useGravity = parValue;
    }

    //======================================
  }
}