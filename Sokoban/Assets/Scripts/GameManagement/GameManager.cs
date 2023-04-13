using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Sokoban.LevelManagement;

namespace Sokoban.GameManagement
{
  public sealed class GameManager : SingletonInGame<GameManager>
  {
    private static GameManager _instance;

    //======================================

    /*public static GameManager Instance
    {
      get
      {
        if (_instance == null)
        {
          GameObject obj = new GameObject("GameManager");
          _instance = obj.AddComponent<GameManager>();
          DontDestroyOnLoad(obj);
        }

        return _instance;
      }
    }*/

    /// <summary>
    /// Данные о прогрессе игрока
    /// </summary>
    public ProgressData ProgressData { get; set; } = new ProgressData();

    //======================================

    /*private void Awake()
    {
      if (_instance == null)
      {
        _instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
        return;
      }
    }*/

    private void Start()
    {
      Levels.GetFullNumberLevelsLocation();
    }

    //======================================

    /// <summary>
    /// Движение
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Performed:
          //OpenNextLevelGame(selectedLocation, numberSelectedLevel);
          //OpenNextLocation(selectedLocation);
          break;
      }
    }

    //======================================



    //======================================
  }
}