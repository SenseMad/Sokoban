using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sokoban.GridEditor
{
  public class GridEditorUI : MonoBehaviour
  {
    [Header("������� ����")]
    [SerializeField, Tooltip("������ ��������� ������ ����")]
    private Button _gridLevelUpButton;
    [SerializeField, Tooltip("����� ������ ����")]
    private TextMeshProUGUI _gridLevelText;
    [SerializeField, Tooltip("������ ��������� ������ ����")]
    private Button _gridLevelDownButton;
    [SerializeField, Tooltip("���������� ��� ���������")]
    private Toggle _displayAllSubLevelToogle;

    [Header("������ ����")]
    [SerializeField, Tooltip("������ ���� X")]
    private TMP_InputField _fieldSizeX;
    [SerializeField, Tooltip("������ ���� Y")]
    private TMP_InputField _fieldSizeY;
    [SerializeField, Tooltip("������ ���� Z")]
    private TMP_InputField _fieldSizeZ;

    [Header("������")]
    [SerializeField, Tooltip("������ ���������")]
    private Button _saveButton;
    [SerializeField, Tooltip("������ ��������� �������")]
    private Button _loadButton;

    //--------------------------------------

    private GridEditor gridEditor;

    //======================================



    //======================================

    private void Awake()
    {
      gridEditor = FindObjectOfType<GridEditor>();
    }

    private void OnEnable()
    {
      UpdateTextGridLevel(gridEditor.GetGridLevel());
      UpdateTextFieldSize(gridEditor.GetFieldSize());

      _gridLevelUpButton.onClick.AddListener(() => ChangeGridLevel(true));
      _gridLevelDownButton.onClick.AddListener(() => ChangeGridLevel(false));
      _displayAllSubLevelToogle.onValueChanged.AddListener(parValue => ShowHideAllSublevels());

      _fieldSizeX.onValueChanged.AddListener(parValue => ChangeFieldSize());
      _fieldSizeY.onValueChanged.AddListener(parValue => ChangeFieldSize());
      _fieldSizeZ.onValueChanged.AddListener(parValue => ChangeFieldSize());
    }

    private void OnDisable()
    {
      _gridLevelUpButton.onClick.RemoveListener(() => ChangeGridLevel(true));
      _gridLevelDownButton.onClick.RemoveListener(() => ChangeGridLevel(false));
      _displayAllSubLevelToogle.onValueChanged.RemoveListener(parValue => ShowHideAllSublevels());

      _fieldSizeX.onValueChanged.RemoveListener(parValue => ChangeFieldSize());
      _fieldSizeY.onValueChanged.RemoveListener(parValue => ChangeFieldSize());
      _fieldSizeZ.onValueChanged.RemoveListener(parValue => ChangeFieldSize());
    }

    //======================================

    #region ������ ����

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    private void ChangeFieldSize()
    {
      if (int.TryParse(_fieldSizeX.text, out int x) && int.TryParse(_fieldSizeY.text, out int y) && int.TryParse(_fieldSizeZ.text, out int z))
      {
        gridEditor.ChangeFieldSize(new Vector3Int(x, y, z));

        UpdateTextFieldSize(gridEditor.GetFieldSize());
      }
    }

    /// <summary>
    /// �������� ����� ������� ����
    /// </summary>
    private void UpdateTextFieldSize(Vector3Int fieldSize)
    {
      _fieldSizeX.text = $"{fieldSize.x}";
      _fieldSizeY.text = $"{fieldSize.y}";
      _fieldSizeZ.text = $"{fieldSize.z}";
    }

    #endregion

    #region ������� ����

    /// <summary>
    /// �������� ������� ����
    /// </summary>
    private void ChangeGridLevel(bool parValue)
    {
      gridEditor.ChangeGridLevel(parValue);

      UpdateTextGridLevel(gridEditor.GetGridLevel());
    }
    /// <summary>
    /// �������� ����� ������ ����
    /// </summary>
    private void UpdateTextGridLevel(int gridLevel)
    {
      _gridLevelText.text = $"{gridLevel}";
    }

    #endregion

    private void ShowHideAllSublevels()
    {
      gridEditor.ShowHideAllSublevels(_displayAllSubLevelToogle.isOn);

      gridEditor.DisplayLevel = !_displayAllSubLevelToogle.isOn;
    }

    //======================================
  }
}