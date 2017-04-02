using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    private float timer = 0f;

    private int h = 0;

    private int m = 0;

    private int s = 0;

    private string timeStr = string.Empty;

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState==GameState.Playing)
            increaseTimer();
    }

    void increaseTimer()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            s++;
            timer = 0;
        }
        if (s >= 60)
        {
            m++;
            s = 0;
        }
        if (m >= 60)
        {
            h++;
            m = 0;
        }
        if (h >= 99)
        {
            h = 0;
        }
    }

    void OnGUI()
    {
        string str = "当前时间: ";
        string str2 = "当前分数: ";
        timeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
        string distanceStr = string.Format("{0:D1}", 100 * s);

        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;    //设置背景填充  
        fontStyle.normal.textColor = new Color(1, 1, 1);   //设置字体颜色  
       
        fontStyle.fontSize = 50;       //字体大小  

        GUI.Label(new Rect(100, 50, 800, 1600), str+timeStr,fontStyle);

        GUI.Label(new Rect(1600, 50, 800, 1600), str2 + distanceStr,fontStyle);

    }

}
