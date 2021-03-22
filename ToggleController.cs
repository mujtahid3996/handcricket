using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleController : MonoBehaviour
{
    bool[] tossSettings;
    bool[] optionSettings;

    public static int indexOfToss, indexOfOption;

    public Toggle[] tossToggles;
    public Toggle[] optionToggles;


    // Start is called before the first frame update
    void Start()
    {
        optionSettings = new bool[optionToggles.Length];
        tossSettings = new bool[tossToggles.Length];

        for (int i = 0; i < optionToggles.Length; i++)
        {
            optionSettings[i] = true;
            tossSettings[i] = true;

            int index = i;

            Toggle optionToggle = optionToggles[i];
            Toggle tossToggle = tossToggles[i];

            optionToggle.onValueChanged.AddListener(
                    (bool check) =>
                    {
                        OptionCheckBox(index, check);
                    }
                );
            tossToggle.onValueChanged.AddListener(
                    (bool check) =>
                    {
                        TossCheckBox(index, check);
                    }
                );
        }
    }

    void OptionCheckBox(int idx, bool check)
    {
        indexOfOption = idx;
        optionSettings[idx] = check;
    }

    void TossCheckBox(int idx, bool check)
    {
        indexOfToss = idx;
        tossSettings[idx] = check;
    }

    public void ClickButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
