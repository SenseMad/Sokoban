using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ������� ������
/// </summary>
public class ButtonObject : InteractiveObjects
{
  [SerializeField, Tooltip("������ � ������� ��������������� ������")]
  private GameObject _objectButtonInteracts;

  //======================================

  private void OnCollisionEnter(Collision collision)
  {
    // �������� ��� ������� ������
  }

  private void OnCollisionExit(Collision collision)
  {
    
  }

  //======================================
}