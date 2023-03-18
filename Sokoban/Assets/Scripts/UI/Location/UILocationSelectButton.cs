using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sokoban.UI
{
  /// <summary>
  /// Пользовательский интерфейс кнопка выбора локации
  /// </summary>
  public class UILocationSelectButton : MonoBehaviour
  {
    [SerializeField, Tooltip("Текст названия локации")]
    private TextMeshProUGUI _textLocationName;

    //======================================

    public Button Button { get; set; }

    //======================================

    /// <summary>
    /// Инициализация кнопки выбора локации
    /// </summary>
    public void Initialize(Location parLocation)
    {
      Button.name = $"{parLocation}";
      _textLocationName.text = $"{parLocation}";
    }

    //======================================
  }
}