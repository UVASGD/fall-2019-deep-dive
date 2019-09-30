using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelXP : MonoBehaviour
{
    public int currentLevel;            //player's current level
    public int startingLevel = 1;        //starting level of the player
    public int currentXP;                //player's current XP
    public int xpToNextLevel;            //xp needed to get to new level
    public Text levelText;
    public Text xpText;

    public bool debug = false;


    // Start is called before the first frame update
    void Start()
    {
        levelText = transform.Find("level_text").GetComponent<Text>();
        xpText = transform.Find("xp_text").GetComponent<Text>();

        levelText.text = currentLevel.ToString("0");
        currentXP = 0;
        currentLevel = startingLevel;
        xpToNextLevel = 5 * currentLevel + 50;
        xpText.text = currentXP.ToString("0") + "/" + xpToNextLevel.ToString("0");

    }

    void XpGain(int amountGained)
    {
        currentXP += amountGained;

    }

    void XpLoss(int amountLost)
    {
        currentXP -= amountLost;
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = currentLevel.ToString("0");
        xpText.text = currentXP.ToString("0") + "/" + xpToNextLevel.ToString("0");
        //XpGain(1);
        xpToNextLevel = 5 * currentLevel + 50;
        if (currentXP >= xpToNextLevel)
        {
            currentLevel++;
            currentXP -= xpToNextLevel;
        }
    }
}
