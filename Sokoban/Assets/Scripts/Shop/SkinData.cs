using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Skin Data", menuName = "Data/Skin Data", order = 51)]
public class SkinData : ScriptableObject
{
  [SerializeField] private int _indexSkin;

  [SerializeField] private int _priceSkin;

  [SerializeField] private GameObject _objectSkin;

  //--------------------------------------

  private Mesh mesh;

  private Material material;

  //======================================

  public Mesh Mesh => mesh;

  public Material Material => material;

  //--------------------------------------

  public int IndexSkin { get => _indexSkin; private set => _indexSkin = value; }

  public int PriceSkin { get => _priceSkin; set => _priceSkin = value; }

  public GameObject ObjectSkin { get => _objectSkin; set => _objectSkin = value; }

  //======================================

  private void Awake()
  {
    mesh = ObjectSkin.GetComponentInChildren<MeshFilter>().sharedMesh;

    material = ObjectSkin.GetComponentInChildren<MeshRenderer>().sharedMaterials[0];
  }

  //======================================
}