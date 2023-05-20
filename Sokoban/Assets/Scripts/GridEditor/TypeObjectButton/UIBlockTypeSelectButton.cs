using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sokoban.GridEditor
{
  public class UIBlockTypeSelectButton : MonoBehaviour
  {
    [SerializeField, Tooltip("Image объекта")]
    private Image _imageObject;

    [SerializeField, Tooltip("Текст названия объекта")]
    private TextMeshProUGUI _textNameObject;

    //======================================

    public Button Button { get; set; }

    public TypeObject TypeObject { get; set; }

    //======================================

    /// <summary>
    /// Инициализация кнопки
    /// </summary>
    public void InitializeButton(TypeObject parTypeObject, Sprite parSprite, string parText)
    {
      TypeObject = parTypeObject;
      _imageObject.sprite = parSprite;
      _textNameObject.text = parText;
    }

    //======================================



    //======================================
  }
}