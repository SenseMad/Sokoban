using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Sokoban.LevelManagement;
using Sokoban.GameManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// ���������������� ��������� ���� ������ ������
  /// </summary>
  public class UILevelSelectMenu : SingletonInSceneNoInstance<UILevelSelectMenu>
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

    private new void Awake()
    {
      gameManager = GameManager.Instance;
    }

    //======================================

    /// <summary>
    /// ���������� ������ ������ ������ � ����������
    /// </summary>
    public void DisplayLevelSelectionButtonsUI(Location parLocation)
    {
      ClearButtonsUI();

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

      for (int i = 0; i < listUILevelSelectButton.Count; i++)
      {
        Destroy(listUILevelSelectButton[i].gameObject);
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