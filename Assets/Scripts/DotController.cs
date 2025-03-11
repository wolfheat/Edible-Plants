using UnityEngine;

public class DotController : MonoBehaviour
{

    [SerializeField] private Dot dotPrefab; 
    
    private Dot[] dots = new Dot[0]; 

    void Start()
    {
        // Create All necessary dots
        int dotAmt = InformationDictionary.Instance.InformationToatal;
        dots = new Dot[dotAmt];

        for (int i = 0; i < dotAmt; i++) {
            dots[i] = Instantiate(dotPrefab,this.transform);
        }
        dots[0].SetAsHighlighted(true);
    }

    public void UpdateActiveDot()
    {
        int activeIndex = InformationDictionary.Instance.ActiveIndex;
        for (int i = 0; i < dots.Length; i++) {
            dots[i].SetAsHighlighted(i == activeIndex);
        }
    }

}
