using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideSelectionWindow : Singleton<SideSelectionWindow>
{
    public Button X;
    public Button[] side=new Button[RedRoller.instance.dice.sides.Length];
    RedDice _dice;
    protected override void Awake()
    {
        base.Awake();
        X.onClick.AddListener(Exit);
        X.interactable = false;
        _dice = Instantiate(RedRoller.instance.dice, UIManager.instance.transform);
        for (int i = 0; i<side.Length; i++)
        {
            side[i].onClick.AddListener(() => { X.interactable = true;_dice.guarantee = i; });
        }
    }

    public void Exit()
    {
        DestroyImmediate(gameObject);
        _dice.Roll();
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
