using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Sokoban.LevelManagement;
using Sokoban.GameManagement;
using TMPro;

namespace Sokoban.UI
{
  /// <summary>
  /// Пользовательский интерфейс меню выбора уровня
  /// </summary>
  public class UILevelSelectMenu : MenuUI
  {
    [SerializeField, Tooltip("Панель, куда будут создаваться кнопки")]
    private RectTransform _levelSelectPanel;

    [Header("ПРЕФАБ")]
    [SerializeField, Tooltip("Префаб кнопки выбора уровня")]
    private UILevelSelectButton _prefabButtonLevelSelect;

    [Header("ТЕКСТ")]
    [SerializeField, Tooltip("Текст количества пройденных уровней")]
    private TextMeshProUGUI _textNumberCompletedLevels;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// Список кнопок выбора уровней
    /// </summary>
    private List<UILevelSelectButton> listUILevelSelectButton = new List<UILevelSelectButton>();

    private Location currentLocation;

    //======================================

    protected override void Awake()
    {
      base.Awake();

      gameManager = GameManager.Instance;
    }

    protected override void Update()
    {
      MoveMenuHorizontally();
    }

    //======================================

    protected override void MoveMenuHorizontally()
    {
      if (_listButtons.Count == 0)
        return;

      if (Time.time > nextTimeMoveNextValue)
      {
        nextTimeMoveNextValue = Time.time + timeMoveNextValue;

        if (inputHandler.GetChangingValuesInput() > 0)
        {
          IsSelected = false;

          listUILevelSelectButton[indexActiveButton].ChangeSprite(false);

          indexActiveButton++;

          if (indexActiveButton > _listButtons.Count - 1) indexActiveButton = 0;
          if (gameManager.ProgressData.GetNumberLevelsCompleted(currentLocation) < indexActiveButton)
            indexActiveButton = 0;

          listUILevelSelectButton[indexActiveButton].ChangeSprite(true);

          Sound();
          IsSelected = true;
        }

        if (inputHandler.GetChangingValuesInput() < 0)
        {
          IsSelected = false;

          listUILevelSelectButton[indexActiveButton].ChangeSprite(false);

          indexActiveButton--;

          if (indexActiveButton < 0) indexActiveButton = _listButtons.Count - 1;
          while (gameManager.ProgressData.GetNumberLevelsCompleted(currentLocation) < indexActiveButton)
            indexActiveButton--;

          listUILevelSelectButton[indexActiveButton].ChangeSprite(true);

          Sound();
          IsSelected = true;
        }
      }

      if (inputHandler.GetChangingValuesInput() == 0)
      {
        nextTimeMoveNextValue = Time.time;
      }
    }

    /// <summary>
    /// Отобразить кнопки выбора уровня в интерфейсе
    /// </summary>
    public void DisplayLevelSelectionButtonsUI(Location parLocation)
    {
      ClearButtonsUI();

      indexActiveButton = -1;

      foreach (var levelData in Levels.GetListLevelData(parLocation))
      {
        UILevelSelectButton button = Instantiate(_prefabButtonLevelSelect, _levelSelectPanel);

        button.Button = button.GetComponent<Button>();
        if (gameManager.ProgressData.GetNumberLevelsCompleted(parLocation) >= levelData.LevelNumber - 1)
        {
          button.ChangeColor();
          button.Button.interactable = true;
          button.Button.onClick.AddListener(() => SelectLevel(levelData));
          indexActiveButton++;
        }

        listUILevelSelectButton.Add(button);
        _listButtons.Add(button.GetComponent<Button>());
        button.Initialize(levelData);
      }

      currentLocation = parLocation;

      listUILevelSelectButton[indexActiveButton].ChangeSprite(true);

      _textNumberCompletedLevels.text = $"{gameManager.ProgressData.GetNumberLevelsCompleted(parLocation)}/{Levels.GetNumberLevelsLocation(parLocation)}";

      base.OnEnable();
    }

    /// <summary>
    /// Очистить список кнопок
    /// </summary>
    private void ClearButtonsUI()
    {
      for (int i = 0; i < listUILevelSelectButton.Count; i++)
      {
        Destroy(listUILevelSelectButton[i].gameObject);
      }

      listUILevelSelectButton = new List<UILevelSelectButton>();
      _listButtons = new List<Button>();
    }

    protected override void OnSelected()
    {
      if (_listButtons.Count == 0)
        return;

      var listButtons = _listButtons[indexActiveButton];

      var rectTransform = listButtons.GetComponent<RectTransform>();
      rectTransform.localScale = new Vector3(1.1f, 1.1f, 1);
    }

    protected override void OnDeselected()
    {
      if (_listButtons.Count == 0)
        return;

      var listButtons = _listButtons[indexActiveButton];

      var rectTransform = listButtons.GetComponent<RectTransform>();
      rectTransform.localScale = new Vector3(1, 1, 1);
    }

    //======================================

    /// <summary>
    /// Выбрать уровень
    /// </summary>
    private void SelectLevel(LevelData levelData)
    {
      //Levels.CurrentSelectedLevelData = levelData;

      PanelController.Instance.CloseAllPanels1();

      LevelManager.Instance.OnPauseEvent?.Invoke(false);

      LevelManager.Instance.ReloadLevel(levelData);

      //SceneManager.LoadScene($"GameScene");
    }

    //======================================
  }
}