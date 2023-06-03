using System.Collections.Generic;
using UnityEngine;

public class ShopData : SingletonInSceneNoInstance<ShopData>
{
  [SerializeField, Tooltip("Данные о скинах")]
  private List<SkinData> _skinDatas = new List<SkinData>();

  //======================================

  /// <summary>
  /// Данные о скинах
  /// </summary>
  public List<SkinData> SkinDatas { get => _skinDatas; private set => _skinDatas = value; }

  //======================================



  //======================================



  //======================================
}