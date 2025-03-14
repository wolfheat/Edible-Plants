using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationImage : MonoBehaviour
{

    [SerializeField] private Image image;

    public void SetImageSprite(Sprite sprite) => image.sprite = sprite;

}
