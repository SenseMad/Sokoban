using UnityEngine;
using TMPro;

namespace Sokoban.UI
{
  /// <summary>
  /// �������� ����� ��� ������
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
    /// ������� ����
    /// </summary>
    private void ChangeLanguage()
    {
      textField.font = LocalisationSystem.Instance.GetFont();
    }

    //======================================
  }
}