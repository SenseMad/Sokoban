using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using LevelManagement;
using GameManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// ���������������� ��������� ���� ������ ������
  /// </summary>
  public class UILevelSelectMenu : MonoBehaviour
  {
    [SerializeField, Tooltip("������, ���� ����� ����������� ������")]
    private RectTransform _levelSelectPanel;

    [Header("������")]
    [SerializeField, Tooltip("������ ������ ������ ������")]
    private UILevelSelectButton _prefabButtonLevelSelect;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// ������ ������ ������ �������
    /// </summary>
    private List<UILevelSelectButton> listUILevelSelectButton = new List<UILevelSelectButton>();

    //======================================

    private void Awake()
    {
      gameManager = GameManager.Instance;
    }

    //======================================

    /// <summary>
    /// ���������� ������ ������ ������ � ����������
    /// </summary>
    public void DisplayLevelSelectionButtonsUI(Location parLocation)
    {
      foreach (var levelData in Levels.GetListLevelData(parLocation))
      {
        UILevelSelectButton button = Instantiate(_prefabButtonLevelSelect, _levelSelectPanel);

        button.Button = button.GetComponent<Button>();
        if (gameManager.ProgressData.GetNumberLevelsCompleted(parLocation) >= levelData.LevelNumber - 1)
        {
          button.Button.interactable = true;
          button.Button.onClick.AddListener(() => SelectLevel(levelData));
        }

        listUILevelSelectButton.Add(button);
        button.Initialize(levelData);
      }
    }

    /// <summary>
    /// �������� ������ ������
    /// </summary>
    public void ClearButtonsUI()
    {
      if (listUILevelSelectButton.Count < 1)
        return;

      foreach (var levelSelectButton in listUILevelSelectButton)
      {
        Destroy(levelSelectButton.gameObject);
      }

      listUILevelSelectButton = new List<UILevelSelectButton>();
    }

    //======================================

    /// <summary>
    /// ������� �������
    /// </summary>
    private void SelectLevel(LevelData levelData)
    {
      Levels.CurrentSelectedLevelData = levelData;

      SceneManager.LoadScene($"GameScene");
    }

    //======================================
  }
}