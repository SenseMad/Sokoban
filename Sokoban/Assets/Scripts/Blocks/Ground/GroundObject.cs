using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : StaticObjects
{
  [SerializeField, Tooltip("��� �����")]
  private TypesGround _typeGround;

  //======================================
}

/// <summary>
/// ���� �����
/// </summary>
public enum TypesGround
{
  Ground,
  Ground1
}