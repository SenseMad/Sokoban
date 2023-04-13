using UnityEngine;
using UnityEngine.Events;

namespace Sokoban.LevelManagement
{
  /// <summary>
  /// ������ ��������� �� ������
  /// </summary>
  [System.Serializable]
  public sealed class LevelProgressData
  {
    [SerializeField, Tooltip("���������� �����")]
    private int _numberMoves;

    [SerializeField, Tooltip("����� ����������� �� ������")]
    private float _timeOnLevel;

    //======================================

    /// <summary>
    /// ���������� �����
    /// </summary>
    internal int NumberMoves { get => _numberMoves; set => _numberMoves = value; }

    /// <summary>
    /// ����� ����������� �� ������
    /// </summary>
    internal float TimeOnLevel { get => _timeOnLevel; set => _timeOnLevel = value; }

    //======================================
  }
}