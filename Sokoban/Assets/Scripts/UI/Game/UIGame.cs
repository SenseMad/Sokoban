using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Sokoban.LevelManagement;

namespace Sokoban.UI
{
  public class UIGame : MonoBehaviour
  {
    [Header("ТЕКСТ")]
    [SerializeField, Tooltip("Текст номера уровня")]
    private TextMeshProUGUI _textLevelNumber;
    [SerializeField, Tooltip("Текст времени на уровне")]
    private TextMeshProUGUI _textTimeLevel;
    [SerializeField, Tooltip("Текст количества ходов")]
    private TextMeshProUGUI _textNumberMoves;

    //--------------------------------------

    private LevelManager levelManager;

    //======================================

    private void Awake()
    {
      levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
      levelManager.IsNextLevelData.AddListener(UpdateText);
      levelManager.ChangeTimeOnLevel.AddListener(UpdateTextTimeLevel);
      levelManager.ChangeNumberMoves.AddListener(UpdateTextNumberMoves);
    }

    private void OnDisable()
    {
      levelManager.IsNextLevelData.RemoveListener(UpdateText);
      levelManager.ChangeTimeOnLevel.RemoveListener(UpdateTextTimeLevel);
      levelManager.ChangeNumberMoves.RemoveListener(UpdateTextNumberMoves);
    }

    //======================================

    private void UpdateText(LevelData parLevelData)
    {
      _textLevelNumber.text = $"LEVEL: {parLevelData.LevelNumber}";
    }

    /// <summary>
    /// Обновить текст времени на уровне
    /// </summary>
    private void UpdateTextTimeLevel(float parValue)
    {
      _textTimeLevel.text = $"TIME: {levelManager.UpdateTextTimeLevel()}";
    }

    /// <summary>
    /// Обновить текст количества ходов
    /// </summary>
    private void UpdateTextNumberMoves(int parValue)
    {
      _textNumberMoves.text = $"NUMBER MOVES: {parValue}";
    }

    //======================================



    //======================================
  }
}