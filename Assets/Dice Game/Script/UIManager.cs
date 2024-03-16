using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public int star;
    public int laps;
    public bool skipAnimation;
    public TextMeshProUGUI timer;
    public GameObject popup,panels;
    public int[,] panelMap=new int[8,4];
    public List<GameObject> panelList = new List<GameObject>();
    public static readonly int[,] PANEL_MAP =
  {
        { 1,2,3,4,5,6,7,8 },
        {9,0,0,0,0,0,0,10 },
        {11,0,0,0,0,0,0,12},
        {13,14,15,16,17,18,19,20}
    };

    public Action<float,int> Reward(float value, float level)
    {
        Debug.Log("Reward " + value * level);
        return default(Action<float,int>);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
