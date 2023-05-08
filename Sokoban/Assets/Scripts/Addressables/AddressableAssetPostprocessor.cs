using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.AddressableAssets;
using UnityEditor;
using UnityEngine;
using Sokoban.LevelManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;

public class AddressableAssetPostprocessor : MonoBehaviour
{
  public string assetFolder = "Assets/MyFolder";
  public string groupPrefix = "MyGroup_";

  //======================================

  private void Start()
  {
    /*string[] assetPaths = Directory.GetFiles(assetFolder, "*.asset", SearchOption.AllDirectories);

    foreach (string path in assetPaths)
    {
      ScriptableObject asset = LoadScriptableObjectFromFile(path);
      if (asset == null)
        continue;

      string groupName = groupPrefix + asset.GetType().Name;
      AddressableAssetSettingsDefaultObject.Settings.CreateGroup(groupName, false, true, false, null);

      //AsyncOperationHandle handle = Addressables.(asset, groupName);
    }*/

  }

  //======================================



  //======================================
}