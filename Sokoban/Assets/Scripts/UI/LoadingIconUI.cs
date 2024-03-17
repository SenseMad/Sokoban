using UnityEngine;

public class LoadingIconUI : MonoBehaviour
{
  private RectTransform _rectTransform;

  private void Awake()
  {
    _rectTransform = GetComponent<RectTransform>();
  }

  private void Update()
  {
    _rectTransform.Rotate(-new Vector3(0, 0, 128.0f * Time.deltaTime));
  }
}