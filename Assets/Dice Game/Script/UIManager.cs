using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>,IDiceListener,IContainer
{
    public int star;
    public int laps { get { return _laps; } set { _laps = value; lapTxt.text = $"Vòng {_laps}/3"; } }
    public Toggle skipAnimation;
    public TextMeshProUGUI timer,lapTxt;
    public GameObject popup;
    public Transform panels,path;
    public Transform[] playableArea;
    int _laps;
    //public List<GameObject> cellList = new List<GameObject>();
    //public static readonly int[,] CELL_MAP ={{ 2,4,6,5,9,10,1,3 }, {9,0,0,0,0,0,0,7 },{6,0,0,0,0,0,0,5},{ 1,3,4,5,10,7,3,9}};
    public void Add(ResourceCell cell, float value)
    {
        var v=Instantiate(popup, transform);Image c;
        var t = v.GetComponentInChildren(typeof(TextMeshProUGUI), false) as TextMeshProUGUI;
        foreach (Transform child in v.transform)
            if (child != t)
            {
                c = child.GetComponent<Image>();
                c.sprite = cell.itemIcon;
                break;
            }      
        t.text = value.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        //for(int i=0;i<CELL_MAP.GetLength(0);i++)
        //{
        //    for(int j=0;j<CELL_MAP.GetLength(1);j++)
        //    {
        //        var v=Instantiate(cellList[CELL_MAP[i,j]], panels).GetComponent<ICell>();
        //    }
        //}
        Instantiate(Resources.Load<Character>("Tsunade"), transform).GetComponent<Character>().walkablePath=path;
    }
    public void WaitForResult(Dice dice)
    {

    }
    public void WaitForPublish(Dice dice, int r)
    {

    }
    public void ReceiveResult(Dice dice, int result)
    {
        Debug.Log(dice.name + " result: " + dice.sides[result].value);
    }

}
