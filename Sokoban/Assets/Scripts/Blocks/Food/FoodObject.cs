using UnityEngine;

using Sokoban.LevelManagement;
using Sokoban.GameManagement;

public class FoodObject : InteractiveObjects
{
  [SerializeField] private TypesFood _typeFood;

  //--------------------------------------

  private Transform meshTransform;

  //======================================

  protected override void Awake()
  {
    base.Awake();

    meshTransform = GetComponentInChildren<MeshFilter>().transform;
  }

  private void Update()
  {
    meshTransform.Rotate(new Vector3(0.0f, 30.0f * Time.deltaTime, 0.0f));
  }

  //======================================

  public bool IsFoodCollected { get; private set; }

  //======================================

  private void OnTriggerEnter(Collider other)
  {
    if (!other.GetComponent<PlayerObjects>())
      return;

    IsFoodCollected = true;

    if (Sound != null)
      AudioManager.Instance.OnPlaySound?.Invoke(Sound);

    LevelManager.Instance.IsFoodCollected();

    gameObject.SetActive(false);
  }

  //======================================
}

public enum TypesFood
{
  Hamburger,
  HotDog
}