using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : StaticObjects
{
  [SerializeField, Tooltip("Тип земли")]
  private TypesGround _typeGround;

  //======================================
}

/// <summary>
/// Типы земли
/// </summary>
public enum TypesGround
{
  Ground,
  Ground1
}