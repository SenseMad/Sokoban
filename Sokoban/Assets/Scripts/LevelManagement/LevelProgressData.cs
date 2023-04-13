using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.LevelManagement
{
  /// <summary>
  /// Данные прогресса на уровне
  /// </summary>
  [System.Serializable]
  public sealed class LevelProgressData
  {
    [SerializeField, Tooltip("Количество ходов")]
    private int _numberMoves;

    [SerializeField, Tooltip("Время проведенное на уровне")]
    private float _timeOnLevel;

    //======================================

    /// <summary>
    /// Количество ходов
    /// </summary>
    internal int NumberMoves { get => _numberMoves; set => _numberMoves = value; }

    /// <summary>
    /// Время проведенное на уровне
    /// </summary>
    internal float TimeOnLevel { get => _timeOnLevel; set => _timeOnLevel = value; }

    //======================================
  }
}