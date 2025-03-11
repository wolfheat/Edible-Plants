using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    [SerializeField] private Image image;
        
    public void SetAsHighlighted(bool set) => image.color = set ? Color.white : Color.gray; 
}
