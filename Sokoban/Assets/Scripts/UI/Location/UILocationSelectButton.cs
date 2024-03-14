using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sokoban.UI
{
  public class UILocationSelectButton : MonoBehaviour
  {
    [SerializeField, Tooltip("Текст названия локации")]
    private TextMeshProUGUI _textLocationName;
    [SerializeField, Tooltip("Текст количества открытых уровней")]
    private TextMeshProUGUI _textNumberLevels;

    [SerializeField, Tooltip("Image замка")]
    private Image _imageLock;

    //======================================

    public Button Button { get; set; }

    public Location Location { get; private set; }

    //======================================

    public void Initialize(Location parLocation)
    {
      Button.name = $"{parLocation}";
      _textLocationName.text = $"{parLocation}";

      Location = parLocation;
    }

    public void ChangeColor()
    {
      _imageLock.gameObject.SetActive(false);
      _textNumberLevels.gameObject.SetActive(true);

      //_textLocationName.color = ColorsGame.STANDART_COLOR;
      //imageButton.color = ColorsGame.SELECTED_COLOR_BLACK;
      //imageButton.color = ColorsGame.STANDART_COLOR;
    }

    public void ChangeTextNumberLevels(string parText)
    {
      _textNumberLevels.text = parText;
    }

    public void ChangeSprite(bool parValue)
    {
      if (parValue)
      {
        _textLocationName.color = ColorsGame.SELECTED_COLOR;
        _textNumberLevels.color = ColorsGame.SELECTED_COLOR;
      }
      else
      {
        _textLocationName.color = ColorsGame.STANDART_COLOR;
        _textNumberLevels.color = ColorsGame.STANDART_COLOR;
      }
      //imageButton.sprite = parValue ? _spriteSelected : _spriteStandart;
    }

    //======================================
  }
}