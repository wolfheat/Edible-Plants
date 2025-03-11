using System.Collections;
using UnityEngine;

public class DotController : MonoBehaviour
{

    [SerializeField] private Dot dotPrefab; 
    
    private Dot[] dots = new Dot[0]; 

    void OnEnable()
    {
        // Create All necessary dots
        int dotAmt = InformationDictionary.Instance.InformationToatal;
        dots = new Dot[dotAmt];

        foreach (Transform child in transform) {
            if (child == transform) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < dotAmt; i++) {
            dots[i] = Instantiate(dotPrefab,this.transform);
        }
        dots[0].SetAsHighlighted(true);
    }

    public IEnumerator UpdateActiveDot()
    {
        yield return null;
        yield return null;
        int activeIndex = InformationDictionary.Instance.ActiveIndex;
        for (int i = 0; i < dots.Length; i++) {
            dots[i].SetAsHighlighted(i == activeIndex);
        }
    }

}
