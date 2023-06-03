using System.Collections.Generic;
using UnityEngine;

public class ShopData : SingletonInSceneNoInstance<ShopData>
{
  [SerializeField, Tooltip("������ � ������")]
  private List<SkinData> _skinDatas = new List<SkinData>();

  //======================================

  /// <summary>
  /// ������ � ������
  /// </summary>
  public List<SkinData> SkinDatas { get => _skinDatas; private set => _skinDatas = value; }

  //======================================



  //======================================



  //======================================
}