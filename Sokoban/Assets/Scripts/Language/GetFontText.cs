using UnityEngine;
using TMPro;

namespace Sokoban.UI
{
  /// <summary>
  /// Получить шрифт для текста
  /// </summary>
  public class GetFontText : MonoBehaviour
  {
    private TextMeshProUGUI textField;

    //======================================

    private void Awake()
    {
      textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
      ChangeLanguage();
    }

    //======================================

    /// <summary>
    /// Сменить язык
    /// </summary>
    private void ChangeLanguage()
    {
      textField.font = LocalisationSystem.Instance.GetFont();
    }

    //======================================
  }
}