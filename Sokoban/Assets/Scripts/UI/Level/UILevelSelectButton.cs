using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Sokoban.LevelManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// Пользовательский интерфейс кнопка выбора уровня
  /// </summary>
  public class UILevelSelectButton : MonoBehaviour
  {
    [SerializeField, Tooltip("Текст названия уровня")]
    private TextMeshProUGUI _textLevelName;

    //======================================

    public Button Button { get; set; }

    //======================================

    /// <summary>
    /// Инициализация кнопки выбора локации
    /// </summary>
    public void Initialize(LevelData levelData)
    {
      Button.name = $"{levelData.LevelNumber}";
      _textLevelName.text = $"{levelData.LevelNumber}";
    }

    //======================================
  }
}