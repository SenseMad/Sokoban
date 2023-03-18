using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using LevelManagement;
using GameManagement;

namespace Sokoban.UI
{
  public class UILevelComplete : MonoBehaviour
  {
    [Header("ПАНЕЛЬ")]
    [SerializeField, Tooltip("Панель пройденного уровня")]
    private Panel _levelCompletePanel;
    [SerializeField, Tooltip("PanelController")]
    private PanelController _panelController;

    [Header("ТЕКСТ")]
    [SerializeField, Tooltip("Текст времени прохождения уровня")]
    private TextMeshProUGUI _textLevelCompletedTime;
    [SerializeField, Tooltip("Текст количества ходов")]
    private TextMeshProUGUI _textNumberMoves;

    //--------------------------------------

    private LevelManager levelManager;

    //======================================

    private void Awake()
    {
      _panelController = GetComponent<PanelController>();

      levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
      levelManager.IsNextLevel.AddListener(CloseMenu);
      levelManager.IsLevelCompleted.AddListener(UpdateText);
    }

    private void OnDisable()
    {
      levelManager.IsNextLevel.RemoveListener(CloseMenu);
      levelManager.IsLevelCompleted.RemoveListener(UpdateText);
    }

    //======================================

    /// <summary>
    /// Обновить текст при завершении уровня
    /// </summary>
    private void UpdateText()
    {
      UpdateTextTime();
      UodateTextNumberMoves();

      _panelController.ShowPanel(_levelCompletePanel);
    }

    /// <summary>
    /// Обновить текст времени проведенного на уровне
    /// </summary>
    private void UpdateTextTime()
    {
      int time = (int)(levelManager.TimeOnLevel * 1000f);
      float formatedTime = (float)time;

      string min = $"{(int)formatedTime / 1000 / 60}";
      string sec = ((int)formatedTime / 1000f % 60f).ToString("f3");

      _textLevelCompletedTime.text = $"ВРЕМЯ ПРОХОЖДЕНИЯ УРОВНЯ: {min}:{sec}";
    }

    /// <summary>
    /// Обновить текст количества ходов
    /// </summary>
    private void UodateTextNumberMoves()
    {
      _textNumberMoves.text = $"КОЛИЧЕСТВО ХОДОВ: {levelManager.NumberMoves}";
    }

    //======================================

    /// <summary>
    /// Кнопка перезугрузки
    /// </summary>
    public void RestartButton()
    {
      _panelController.CloseAllPanels();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// Закрыть меню
    /// </summary>
    private void CloseMenu()
    {
      _panelController.CloseAllPanels();
    }

    //======================================



    //======================================
  }
}