using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sokoban.UI
{
  /// <summary>
  /// ���������������� ��������� ������ ������ �������
  /// </summary>
  public class UILocationSelectButton : MonoBehaviour
  {
    [SerializeField, Tooltip("����� �������� �������")]
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
    /// ������������� ������ ������ �������
    /// </summary>
    public void Initialize(Location parLocation)
    {
      Button.name = $"{parLocation}";
      _textLocationName.text = $"{parLocation}";

      Location = parLocation;
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    public void ChangeColor()
    {
      _textLocationName.color = ColorsGame.STANDART_COLOR;
      imageButton.color = ColorsGame.STANDART_COLOR;
    }

    //======================================
  }
}