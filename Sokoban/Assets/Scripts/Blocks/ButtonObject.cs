using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс объекта кнопки
/// </summary>
public class ButtonObject : InteractiveObjects
{
  [SerializeField, Tooltip("Объект с которым взаимодействует кнопка")]
  private GameObject _objectButtonInteracts;

  //======================================

  private void OnCollisionEnter(Collision collision)
  {
    // Действие при нажатии кнопки
  }

  private void OnCollisionExit(Collision collision)
  {
    
  }

  //======================================
}