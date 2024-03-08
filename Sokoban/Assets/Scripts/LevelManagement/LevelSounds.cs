using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.GameManagement;

namespace Sokoban.LevelManagement
{
  public class LevelSounds : MonoBehaviour
  {
    [SerializeField] private AudioClip _levelComplete;

    //======================================

    public AudioClip LevelComplete => _levelComplete;

    //======================================

    private void PlaySound(AudioClip parAudioClip)
    {
      //PlaySound(parAudioClip);
    }

    //======================================
  }
}