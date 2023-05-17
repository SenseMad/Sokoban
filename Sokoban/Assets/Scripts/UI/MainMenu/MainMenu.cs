using UnityEngine;
using UnityEngine.UI;

namespace Sokoban.UI
{
  /// <summary>
  /// Главное меню
  /// </summary>
  public class MainMenu : MenuUI
  {
    protected override void OnEnable()
    {
      indexActiveButton = 0;

      base.OnEnable();
    }

    //======================================

    /// <summary>
    /// Выйти из игры
    /// </summary>
    public void ExitGame()
    {
      Application.Quit();
    }

    //======================================
  }
}