using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>,IDiceListener
{
    public int star;
    public int laps;
    public Toggle skipAnimation;
    public TextMeshProUGUI timer;
    public GameObject popup;
    public Transform panels;
    public List<GameObject> cellList = new List<GameObject>();
    public static readonly int[,] CELL_MAP ={{ 2,4,6,5,9,10,1,3 }, {9,0,0,0,0,0,0,7 },{6,0,0,0,0,0,0,5},{ 1,3,4,5,10,7,3,9}};
    [HideInInspector]
    public List<Vector3> path = new();
    public Action<float,int> Reward(float value, float level)
    {
        Debug.Log("Reward " + value * level);
        return default(Action<float,int>);
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<CELL_MAP.GetLength(0);i++)
        {
            for(int j=0;j<CELL_MAP.GetLength(1);j++)
            {
                Vector3 v=Instantiate(cellList[CELL_MAP[i, j]], panels).transform.position;
                if(CELL_MAP[i, j]!=0)
                    path.Add(v);
            }
        }
    }
    public void WaitForResult(Dice dice)
    {

    }
    public void WaitForPublish(Dice dice)
    {

    }
    public void ReceiveResult(Dice dice, int result)
    {
        Debug.Log(dice.name + " " + dice.sides[result].value);
    }
}
