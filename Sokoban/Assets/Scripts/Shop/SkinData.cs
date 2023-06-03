using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Skin Data", menuName = "Data/Skin Data", order = 51)]
public class SkinData : ScriptableObject
{
  [SerializeField, Tooltip("Индекс скина")]
  private int _indexSkin;

  [SerializeField, Tooltip("Цена скина")]
  private int _priceSkin;

  [SerializeField, Tooltip("Объект скина")]
  private GameObject _objectSkin;

  //--------------------------------------

  private Mesh mesh;
  private Material material;

  //======================================

  public Mesh GetMesh() => mesh;

  public Material GetMaterial() => material;

  //--------------------------------------

  /// <summary>
  /// Индекс скина
  /// </summary>
  public int IndexSkin { get => _indexSkin; private set => _indexSkin = value; }
  /// <summary>
  /// Цена скина
  /// </summary>
  public int PriceSkin { get => _priceSkin; set => _priceSkin = value; }
  /// <summary>
  /// Объект скина
  /// </summary>
  public GameObject ObjectSkin { get => _objectSkin; set => _objectSkin = value; }

  //======================================

  private void Awake()
  {
    mesh = ObjectSkin.GetComponentInChildren<MeshFilter>().sharedMesh;

    material = ObjectSkin.GetComponentInChildren<MeshRenderer>().sharedMaterials[0];
  }

  //======================================
}