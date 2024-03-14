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

    [Space(10)]
    [SerializeField] private GameObject _topCameraRotate;

    //--------------------------------------

    private LevelManager levelManager;

    //======================================

    private void Awake()
    {
      levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
      levelManager.OnLevelCompleted += SetObjectFalse;

      levelManager.IsReloadLevel.AddListener(SetObjectTrue);
      levelManager.IsMenu.AddListener(SetObjectFalse);

      levelManager.IsNextLevelData.AddListener(UpdateText);
      levelManager.ChangeTimeOnLevel.AddListener(UpdateTextTimeLevel);
      levelManager.ChangeNumberMoves.AddListener(UpdateTextNumberMoves);
    }

    private void OnDisable()
    {
      levelManager.OnLevelCompleted -= SetObjectFalse;

      levelManager.IsReloadLevel.RemoveListener(SetObjectTrue);
      levelManager.IsMenu.RemoveListener(SetObjectFalse);

      levelManager.IsNextLevelData.RemoveListener(UpdateText);
      levelManager.ChangeTimeOnLevel.RemoveListener(UpdateTextTimeLevel);
      levelManager.ChangeNumberMoves.RemoveListener(UpdateTextNumberMoves);
    }

    //======================================

    private void UpdateText(LevelData parLevelData)
    {
      if (_textLevelNumber == null)
        return;

      _textLevelNumber.text = $"LEVEL {parLevelData.LevelNumber}";
    }

    private void UpdateTextTimeLevel(float parValue)
    {
      if (_textTimeLevel == null)
        return;

      _textTimeLevel.text = $"{levelManager.UpdateTextTimeLevel()}";
    }

    private void UpdateTextNumberMoves(int parValue)
    {
      if (_textNumberMoves == null)
        return;

      _textNumberMoves.text = $"{parValue}";
    }

    private void SetObjectTrue() => SetObject(true);
    private void SetObjectFalse() => SetObject(false);

    private void SetObject(bool parValue)
    {
      //_objectLevelNumber.SetActive(parValue);
      _objectTimeLevel.SetActive(parValue);
      _topCameraRotate.SetActive(parValue);

      //_objectNumberMoves.SetActive(parValue);
    }

    //======================================
  }
}