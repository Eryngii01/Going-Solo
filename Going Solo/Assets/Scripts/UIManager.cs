using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider fatigueBar, reputationBar;
    public Text athleticism, intelligence, charisma;
    public Text timeTracker;
    private PlayerStats playerStats;

    private float currentTime = 0, lastTime = 0;
    private int hour = 7, minute = 0;
    private bool isMorning, isTicking;

    private static bool UIExists;

    // Start is called before the first frame update
    void Start()
    {
        isMorning = true; 
        isTicking = true;
        playerStats = FindObjectOfType<PlayerStats>();

        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (isTicking == true && currentTime - lastTime >= 4)
        {
            lastTime = currentTime;
            if (minute == 5 && hour == 11)
            {
                minute = 0;
                hour += 1;
                isMorning = !isMorning;
            }
            else if (minute == 5 && hour == 12)
            {
                minute = 0;
                hour = 1;
            }
            else if (minute == 5)
            {
                minute = 0;
                hour += 1;
            }
            else
                minute += 1;
        }

        fatigueBar.value = playerStats.currentFatigue;
        reputationBar.value = playerStats.currentReputation;

        // athleticism.text = "Athleticism: " + playerStats.athleticism;
        // intelligence.text = "Intelligence: " + playerStats.intelligence;
        // charisma.text = "Charisma: " + playerStats.charisma;

        if (isMorning)
        {
            timeTracker.text = "AM " + hour + ":" + minute + "0";
        }
        else
            timeTracker.text = "PM " + hour + ":" + minute + "0";
    }
}
