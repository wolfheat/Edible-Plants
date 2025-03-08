using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public List<Category> SelectedCategories { get; set; }
    public int SelectedCategoriesBinary { get; set; }
	public static Settings Instance { get; private set; }


    [Header("Answer Colors")]
    [SerializeField] public Color CorrectColor;
    [SerializeField] public Color WrongColor;


    private void Awake()
	{
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;

        Debug.Log("Created Settings");

		// Initiate with all
		AddAllCategories();
		Debug.Log("Binary:"+ Convert.ToString(SelectedCategoriesBinary, 2));
	}

    internal void AddOrRemoveCategory(int binaryValue)
    {
		Debug.Log("Combining SelectedCategories:"+Convert.ToString(SelectedCategoriesBinary,2)+" XOR "+ Convert.ToString(binaryValue, 2));
        SelectedCategoriesBinary = SelectedCategoriesBinary ^ binaryValue;
    }

    internal void AddAllCategories() => SelectedCategoriesBinary = Enum.GetValues(typeof(Category)).Cast<int>().Sum();
}
