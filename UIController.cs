using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIController : MonoBehaviour
{
    public Text welcomeText, playerScoreText, deviceScoreText;
    public Image opponentImage, playerImage;
    public GameObject uiObject;
    public Material[] mats;
    public GameObject[] handObjects;

    string[] optionToggles = { "Batting", "Bowling" };
    float timeStart, timeLen = 3f, fingerTimeStart;
    int idxToss = ToggleController.indexOfToss, idxOption = ToggleController.indexOfOption, randInt, count = 0, battingInt = 0, numWickets = 0;
    int[] runArray = {1, 2, 3, 4, 5};
    float over = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Disable Hand Objects at first
        for(int i = 0; i < handObjects.Length; i++)
        {
            handObjects[i].SetActive(false);
        }

        int idx = Random.Range(0, optionToggles.Length);
        timeStart = Time.time;

        if (idx == idxToss)
        {
            welcomeText.text = "You Win the toss! Now you can play " + optionToggles[idxOption];
            playerImage.gameObject.GetComponent<Image>().material = mats[idxOption];
            opponentImage.gameObject.GetComponent<Image>().material = mats[Mathf.Abs(1 - idxOption)];
        }
        else
        {
            welcomeText.text = "You Loss the toss! Now you have to play " + optionToggles[Mathf.Abs(1 - idxOption)];
            opponentImage.gameObject.GetComponent<Image>().material = mats[idxOption];
            playerImage.gameObject.GetComponent<Image>().material = mats[Mathf.Abs(1 - idxOption)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeStart + timeLen)
        {
            uiObject.SetActive(false);
        }

        if(Time.time > fingerTimeStart + timeLen)
        {
            for(int i = 0; i < handObjects.Length; i++)
            {
                if (handObjects[i].activeInHierarchy)
                {
                    handObjects[i].SetActive(false);
                }
            }
        }
    }

    public void ScoreButtonPress(int idx)
    {
        randInt = Random.Range(0, 5);
        handObjects[randInt].SetActive(true);
        fingerTimeStart = Time.time;
        count += 1;

        if (count % 6 == 0)
        {
            over = 1f + (over - .1f);
            over = Mathf.Round(over);
        }
        else
        {
            over += 0.1f;
        }

        if(idxOption == 0)
        {
            // if player is batsman
            DecisionTextMethod(playerScoreText, deviceScoreText, idx, randInt);
        }
        else
        {
            // if player is bowler
            DecisionTextMethod(deviceScoreText, playerScoreText , idx, randInt);
        }
    }

    public void IntervalButton()
    {
        if (handObjects[randInt].activeInHierarchy)
        {
            handObjects[randInt].SetActive(false);
        }
    }

    void DecisionTextMethod(Text batText, Text bowlText, int idx, int rand)
    {
        if (idx != rand)
        {
            battingInt += runArray[idx];
            batText.text = battingInt.ToString();
            bowlText.text = over.ToString()+ "-" + numWickets.ToString();
        }
        else
        {
            numWickets += 1;
            batText.text = "Wicket";
            bowlText.text = over.ToString() + "-" + numWickets.ToString();
        }
    }
}
