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

    //--------------------------------------

    private Image imageButton;

    //======================================

    public Button Button { get; set; }

    public Location Location { get; private set; }

    //======================================

    private void Awake()
    {
      imageButton = GetComponent<Image>();
    }

    //======================================

    /// <summary>
    /// Инициализация кнопки выбора локации
    /// </summary>
    public void Initialize(Location parLocation)
    {
      Button.name = $"{parLocation}";
      _textLocationName.text = $"{parLocation}";

      Location = parLocation;
    }

    /// <summary>
    /// Изменить цвет
    /// </summary>
    public void ChangeColor()
    {
      _textLocationName.color = ColorsGame.STANDART_COLOR;
      imageButton.color = ColorsGame.STANDART_COLOR;
    }

    //======================================
  }
}