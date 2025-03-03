using System;
using TMPro;
using UnityEngine;

public class ButtonOption : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI buttonText;
    private BaseGame game;

    public void UpdateText(string s) => buttonText.text = s;
    public void SetGame(BaseGame gameToSet) => game = gameToSet;

    public void ClickButton() => game.AnswerOption(buttonText.text);

    internal string GetText() => buttonText.text;
}
