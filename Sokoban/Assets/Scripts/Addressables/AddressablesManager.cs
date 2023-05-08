using Sokoban.LevelManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : MonoBehaviour
{
  [SerializeField, Tooltip("")]
  private AssetReference loadableLevelData;
  [SerializeField, Tooltip("")]
  private string _assetName;

  //--------------------------------------

  private AsyncOperationHandle<LevelData> handle;

  private InputHandler inputHandler;

  //======================================

  private void Awake()
  {
    inputHandler = InputHandler.Instance;
  }

  private void OnEnable()
  {
    inputHandler.AI_Player.UI.Select.performed += Select_performed;
  }

  private void Start()
  {
    //LoadAsset();
  }

  private void OnDisable()
  {
    inputHandler.AI_Player.UI.Select.performed -= Select_performed;
  }

  //======================================

  private void LoadAsset()
  {
    if (handle.IsValid())
    {
      Addressables.Release(handle);
    }

    handle = Addressables.LoadAssetAsync<LevelData>(_assetName);

    handle.Completed += OnLoadCompleted;
  }

  private void OnLoadCompleted(AsyncOperationHandle<LevelData> objectLevelData)
  {
    if (objectLevelData.Status == AsyncOperationStatus.Succeeded)
    {
      LevelData levelData = objectLevelData.Result;
      Debug.Log($"Loaded: {levelData.name}");
    }
    else
    {
      Debug.LogError($"Failed to load asset: {objectLevelData.OperationException.Message}");
    }

    handle = default;
  }

  //======================================

  private void Select_performed(InputAction.CallbackContext obj)
  {
    LoadAsset();
  }

  //======================================



  //======================================
}