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

    [Header("ОБЪЕКТЫ")]
    [SerializeField, Tooltip("Объект номера уровня")]
    private GameObject _objectLevelNumber;
    [SerializeField, Tooltip("Объект времени на уровне")]
    private GameObject _objectTimeLevel;
    [SerializeField, Tooltip("Объект количества ходов")]
    private GameObject _objectNumberMoves;

    //--------------------------------------

    private LevelManager levelManager;

    //======================================

    private void Awake()
    {
      levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
      levelManager.IsLevelCompleted.AddListener(() => SetObject(false));
      levelManager.IsReloadLevel.AddListener(() => SetObject(true));
      levelManager.IsMenu.AddListener(() => SetObject(false));

      levelManager.IsNextLevelData.AddListener(UpdateText);
      levelManager.ChangeTimeOnLevel.AddListener(UpdateTextTimeLevel);
      levelManager.ChangeNumberMoves.AddListener(UpdateTextNumberMoves);
    }

    private void OnDisable()
    {
      levelManager.IsLevelCompleted.RemoveListener(() => SetObject(false));
      levelManager.IsReloadLevel.RemoveListener(() => SetObject(true));
      levelManager.IsMenu.RemoveListener(() => SetObject(false));

      levelManager.IsNextLevelData.RemoveListener(UpdateText);
      levelManager.ChangeTimeOnLevel.RemoveListener(UpdateTextTimeLevel);
      levelManager.ChangeNumberMoves.RemoveListener(UpdateTextNumberMoves);
    }

    //======================================

    private void UpdateText(LevelData parLevelData)
    {
      _textLevelNumber.text = $"LEVEL {parLevelData.LevelNumber}";
    }

    /// <summary>
    /// Обновить текст времени на уровне
    /// </summary>
    private void UpdateTextTimeLevel(float parValue)
    {
      _textTimeLevel.text = $"{levelManager.UpdateTextTimeLevel()}";
    }

    /// <summary>
    /// Обновить текст количества ходов
    /// </summary>
    private void UpdateTextNumberMoves(int parValue)
    {
      _textNumberMoves.text = $"{parValue}";
    }

    private void SetObject(bool parValue)
    {
      _objectLevelNumber.SetActive(parValue);
      _objectTimeLevel.SetActive(parValue);
      _objectNumberMoves.SetActive(parValue);
    }

    //======================================



    //======================================
  }
}