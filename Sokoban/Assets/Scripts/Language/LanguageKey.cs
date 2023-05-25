using UnityEngine;
using TMPro;

public class LanguageKey : MonoBehaviour
{
  [SerializeField, Tooltip("Ключ перевода")]
  private string _key;

  //--------------------------------------

  private TextMeshProUGUI textField;

  //======================================

  private void Awake()
  {
    textField = GetComponent<TextMeshProUGUI>();
  }

  private void OnEnable()
  {
    UpdateText();
  }

  //======================================

  private void UpdateText()
  {
    if (_key == "") { return; }

    var localisationSystem = LocalisationSystem.Instance;
    string value = localisationSystem.GetLocalisedValue(_key);

    value = value.TrimStart(' ', '"');
    value = value.Replace("\"", "");
    textField.text = value;
    textField.font = localisationSystem.GetFont();
  }

  //======================================
}