using UnityEngine;

public class SpikeObject : InteractiveObjects
{
  [Header("СОСТОЯНИЯ")]
  [SerializeField, Tooltip("Обычное состояние")]
  private GameObject _normalState;

  [SerializeField, Tooltip("Активированное состояние")]
  private GameObject _activatedState;

  //--------------------------------------

  /// <summary>
  /// True, если игрок стоит на шипах
  /// </summary>
  private bool isPlayerStandsSpikes;
  
  /// <summary>
  /// Время задержки
  /// </summary>
  private readonly float delayTime = 5.0f;
  /// <summary>
  /// Текущее время задержки
  /// </summary>
  private float currentDelayTime;

  //======================================

  /// <summary>
  /// True, если шип активирован
  /// </summary>
  public bool IsSpikeActivated { get; private set; }

  //======================================

  private void Update()
  {
    if (IsSpikeActivated)
      return;

    if (isPlayerStandsSpikes)
    {
      currentDelayTime += Time.deltaTime;

      if (currentDelayTime >= delayTime)
      {
        ActivateSpikes();
      }
    }
  }

  //======================================

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Block>().GetTypeObject() != TypeObject.playerObject)
      return;

    if (IsSpikeActivated)
    {
      if (other.TryGetComponent(out PlayerObjects playerObjects))
      {
        playerObjects.OnPlayerDeathEvent?.Invoke();
        return;
      }
    }

    isPlayerStandsSpikes = true;
    currentDelayTime = 0;
  }

  private void OnTriggerStay(Collider other)
  {
    if (other.GetComponent<Block>().GetTypeObject() != TypeObject.playerObject)
      return;

    if (IsSpikeActivated)
    {
      if (other.TryGetComponent(out PlayerObjects playerObjects))
      {
        playerObjects.OnPlayerDeathEvent?.Invoke();
        return;
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.GetComponent<Block>().GetTypeObject() != TypeObject.playerObject)
      return;

    ActivateSpikes();
  }

  //======================================

  /// <summary>
  /// Активировать шипы
  /// </summary>
  private void ActivateSpikes()
  {
    isPlayerStandsSpikes = false;
    IsSpikeActivated = true;
    _normalState.SetActive(false);
    _activatedState.SetActive(true);
  }

  //======================================
}