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
    [SerializeField, Tooltip("Текст количества открытых уровней")]
    private TextMeshProUGUI _textNumberLevels;

    [SerializeField, Tooltip("Image замка")]
    private Image _imageLock;

    [SerializeField, Tooltip("Спрайт стандартный")]
    private Sprite _spriteStandart;
    [SerializeField, Tooltip("Спрайт выделения")]
    private Sprite _spriteSelected;

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
      _imageLock.gameObject.SetActive(false);
      _textNumberLevels.gameObject.SetActive(true);

      _textLocationName.color = ColorsGame.STANDART_COLOR;
      imageButton.color = ColorsGame.SELECTED_COLOR_BLACK;
      //imageButton.color = ColorsGame.STANDART_COLOR;
    }

    /// <summary>
    /// Изменить текст количества уровней
    /// </summary>
    public void ChangeTextNumberLevels(string parText)
    {
      _textNumberLevels.text = parText;
    }

    /// <summary>
    /// Изменить спрайт
    /// </summary>
    public void ChangeSprite(bool parValue)
    {
      imageButton.sprite = parValue ? _spriteSelected : _spriteStandart;
    }

    //======================================
  }
}