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

    //======================================

    public Button Button { get; set; }

    //======================================

    /// <summary>
    /// ������������� ������ ������ �������
    /// </summary>
    public void Initialize(LevelData levelData)
    {
      Button.name = $"{levelData.LevelNumber}";
      _textLevelName.text = $"{levelData.LevelNumber}";
    }

    //======================================
  }
}