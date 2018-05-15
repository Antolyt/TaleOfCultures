using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeController : MonoBehaviour {

    public Light sun;
    public Text text;

    public int startHour;
    public int startMinute;
    public int startDay;
    public int startMonth;

    public int daysPerMonth;
    public int monthPerYear;

    public int timeSpeed;
    public int timeRoundingInvervallInMinutes;

    public AnimationCurve lightRed;
    public AnimationCurve lightGreen;
    public AnimationCurve lightBlue;
    public AnimationCurve lightIntensity;

    static float time;
    static int hours;
    static int minutes;
    static int day;
    static int month;

    public delegate void newDay();
    public static event newDay OnNewDay;
    public delegate void newMonth();
    public static event newMonth OnNewMonth;
    public delegate void newYear();
    public static event newYear OnNewYear;

    private float pastSeconds;

    const int lightUpdateOffset = 1;

    // Use this for initialization
    void Start()
    {
        // Set time from saveData
        if (Savegame.savegameData != null)
        {
            time = Savegame.savegameData.time;

            hours = (int)Savegame.savegameData.time;
            minutes = (int)((Savegame.savegameData.time - hours) * 60);
            day = Savegame.savegameData.day;
            month = Savegame.savegameData.month;
        }
        else
        {
            hours = startHour;
            minutes = startMinute;
            day = startDay;
            month = startMonth;
        }

        UpdateLight();
        UpdateClock();
    }
	
	// Update is called once per frame
	void Update ()
    {
        pastSeconds += Time.deltaTime * timeSpeed;
        // Minutes
		if(pastSeconds >= timeRoundingInvervallInMinutes)
        {
            pastSeconds = 0;

            // Hours
            if (minutes == 60 - timeRoundingInvervallInMinutes)
            {
                minutes = 0;

                // Day
                if (hours == 23)
                {
                    hours = 0;

                    // Month
                    if (day == daysPerMonth)
                    {
                        day = 1;

                        // Year
                        if (month == monthPerYear)
                        {
                            month = 1;

                            OnNewYear();
                        }
                        else
                            month++;

                        OnNewYear();
                    }
                    else
                        day++;

                    OnNewDay();
                }
                else
                    hours++;
            }
            else
                minutes += timeRoundingInvervallInMinutes;

            time = (minutes / 60f) + hours;
            UpdateLight();
            UpdateClock();
        }
	}

    public void SetTime(float time)
    {
        hours = (int)time;
        minutes = (int)((time - hours) * 60);
    }

    public void UpdateClock()
    {
        string hoursText = hours < 10 ? "0" + hours : "" + hours;
        string minutesText = minutes < 10 ? "0" + minutes : "" + minutes;

        text.text = hoursText + ":" + minutesText;
    }

    public void UpdateLight()
    {
        sun.intensity = lightIntensity.Evaluate(time / 24);
        sun.color = new Color(lightRed.Evaluate(time / 24), lightBlue.Evaluate(time / 24), lightGreen.Evaluate(time / 24));
    }
}
