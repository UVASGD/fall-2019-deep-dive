using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelXP : ExpandingBarUI
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
        currentXP = 0;
        currentLevel = startingLevel;
        xpToNextLevel = 5 * currentLevel + 50;
        UpdateHitPointUI();
    }

    public void XpGain(int amountGained)
    {
        currentXP += amountGained;
        if (currentXP >= xpToNextLevel)
        {
            currentLevel++;
            currentXP -= xpToNextLevel;
            xpToNextLevel = 5 * currentLevel + 50;
        }
        UpdateHitPointUI();
    }

    public void XpLoss(int amountLost)
    {
        currentXP -= amountLost;
        UpdateHitPointUI();
    }

    public int getXp()
    {
        return currentXP;
    }

    public int getXpToNextLevel()
    {
        return xpToNextLevel;
    }

    public int getLevel()
    {
        return currentLevel;
    }

    void UpdateHitPointUI()
    {
        levelText.text = "Level " + currentLevel.ToString("0");
        xpText.text = currentXP + "/" + xpToNextLevel + " XP";
        UpdateBarUI(xpToNextLevel, currentXP);
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            XpGain(1);
        }
    }
}
