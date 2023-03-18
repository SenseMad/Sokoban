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

    //======================================

    public Button Button { get; set; }

    //======================================

    /// <summary>
    /// ������������� ������ ������ �������
    /// </summary>
    public void Initialize(Location parLocation)
    {
      Button.name = $"{parLocation}";
      _textLocationName.text = $"{parLocation}";
    }

    //======================================
  }
}