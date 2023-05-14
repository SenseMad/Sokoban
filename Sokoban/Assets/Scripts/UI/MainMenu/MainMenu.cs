using UnityEngine;
using UnityEngine.UI;

namespace Sokoban.UI
{
  /// <summary>
  /// Главное меню
  /// </summary>
  public class MainMenu : MenuUI
  {


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