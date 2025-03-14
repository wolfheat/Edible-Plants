using TMPro;
using UnityEngine;

public class KeyboardKey : MonoBehaviour
{
    public void OnKeyPressed()
    {
        FindFirstObjectByType<DigitalKeyboard>()?.OnButtonClicked(GetComponentInChildren<TextMeshProUGUI>()?.text);

    }
}
