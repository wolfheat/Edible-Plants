using UnityEngine;

public class EnableAtStart : MonoBehaviour
{
    [SerializeField] private GameObject[] enableObjects; 
    [SerializeField] private GameObject[] disableObjects; 

    void Start()
    {
        foreach (var item in enableObjects) {
            item.SetActive(true);
        }        
        foreach (var item in disableObjects) {
            item.SetActive(false);
        }        

    }

}
