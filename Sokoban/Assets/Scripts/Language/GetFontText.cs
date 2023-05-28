using UnityEngine;
using TMPro;

using Sokoban.GameManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// Получить шрифт для текста
  /// </summary>
  public class GetFontText : MonoBehaviour
  {
    private TextMeshProUGUI textField;

    //--------------------------------------

    private GameManager gameManager;

    //======================================

    private void Awake()
    {
      textField = GetComponent<TextMeshProUGUI>();

      gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
      ChangeFont();

      gameManager.SettingsData.ChangeLanguage.AddListener(parValue => ChangeFont());
    }

    private void OnDisable()
    {
      gameManager.SettingsData.ChangeLanguage.RemoveListener(parValue => ChangeFont());
    }

    //======================================

    /// <summary>
    /// Сменить шрифт
    /// </summary>
    private void ChangeFont()
    {
      textField.font = LocalisationSystem.Instance.GetFont();
    }

    //======================================
  }
}