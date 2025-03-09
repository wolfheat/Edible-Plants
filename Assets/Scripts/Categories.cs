using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;


[Flags]
public enum Category {Trees=1,Flowers=2,Roots=4,Berries=8, Raw=16, Toxic=32}

public class Categories : MonoBehaviour
{

    private List<Category> categories;
    [SerializeField] private List<CategoryButton> categoryButtons;
    [SerializeField] private TextMeshProUGUI selectedText;

    private void OnEnable()
    {
        // Every time this is enabled update info from the Settings?
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        int categories = Settings.Instance.SelectedCategoriesBinary;
        StringBuilder sb = new("Selected: ");
        for (int i = 0; i < categoryButtons.Count; i++) {
            bool enabled = (categories & (1 << i)) != 0;
            categoryButtons[i].SetButtonActive(enabled);
            sb.Append(enabled ? " "+(categoryButtons[i].ButtonText()) : "");
            //sb.Append(enabled ? " "+(Category)(1<<i) : "");
        }
        selectedText.text = sb.ToString();
    }

    public void OnClickButton(CategoryButton categoryButton)
    {
        Debug.Log("Clicking "+categoryButton.name);
        string categoryName = categoryButton.name;
        if (categoryName == "All") {
            Settings.Instance.AddAllCategories();
            UpdateVisuals();
            return;
        }

        if (Enum.TryParse(categoryName, out Category categoryEnum)) {
            int binaryValue = (int)categoryEnum;
            Debug.Log("Binary values for "+categoryName+" = "+Convert.ToString(binaryValue,2));
            Settings.Instance.AddOrRemoveCategory(binaryValue);

            UpdateVisuals();
        }

    }

}
