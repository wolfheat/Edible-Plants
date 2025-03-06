using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    [SerializeField] private Image backgroundColor;
    private Color activeColor = Color.green;
    private Color inActiveColor = Color.red;

    internal void SetButtonActive(bool set) => backgroundColor.color = set ? activeColor : inActiveColor;
}
