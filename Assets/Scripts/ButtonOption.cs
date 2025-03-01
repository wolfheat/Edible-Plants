using System;
using TMPro;
using UnityEngine;

public class ButtonOption : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI buttonText;
    private int buttonIndex = -1;

    public void UpdateText(string s) => buttonText.text = s;
    public void UpdateIndex(int index) => buttonIndex = index;

    public void ClickButton() => TwoOptionGame.Instance.AnswerOption(buttonIndex);

    internal string GetText() => buttonText.text;
}
