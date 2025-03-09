using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    [SerializeField] private Image backgroundColor;
    [SerializeField] private TextMeshProUGUI textField;
    private Color activeColor = Color.green;
    private Color inActiveColor = Color.red;

    internal string ButtonText() => textField.text;

    internal void SetButtonActive(bool set) => backgroundColor.color = set ? activeColor : inActiveColor;
}
