using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Sokoban.LevelManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// ���������������� ��������� ������ ������ ������
  /// </summary>
  public class UILevelSelectButton : MonoBehaviour
  {
    [SerializeField, Tooltip("����� �������� ������")]
    private TextMeshProUGUI _textLevelName;

    //--------------------------------------

    private Image imageButton;

    //======================================

    public Button Button { get; set; }

    /// <summary>
    /// True, ���� ������� ������
    /// </summary>
    public bool IsLevelOpen { get; private set; }

    //======================================

    private void Awake()
    {
      imageButton = GetComponent<Image>();
    }

    //======================================

    /// <summary>
    /// ������������� ������ ������ �������
    /// </summary>
    public void Initialize(LevelData levelData)
    {
      Button.name = $"{levelData.LevelNumber}";
      _textLevelName.text = $"{levelData.LevelNumber}";
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    public void ChangeColor()
    {
      _textLevelName.color = ColorsGame.STANDART_COLOR;
      imageButton.color = ColorsGame.STANDART_COLOR;

      IsLevelOpen = true;
    }

    //======================================
  }
}