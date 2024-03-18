using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RedRoller : Singleton<RedRoller>, IRoller,IDiceListener,IContainer
{
    public SideSelectionWindow selectionWindow;
    public int redDice { get { return int.Parse(diceQty.text); } set { diceQty.text = value.ToString(); } }
    public TextMeshProUGUI diceQty;
    public void Roll()
    {
        if (redDice > 0)
        {
            redDice--;
            Instantiate(selectionWindow.transform,UIManager.instance.transform);
        }
    }
    public void WaitForResult(Dice dice)
    {
        gameObject.SetActive(false);
    }
    public void WaitForPublish(Dice dice)
    {

    }

    public void Add(ResourceCell cell, float amount)
    {
        redDice += Mathf.RoundToInt(amount);
        Debug.Log($"Gained {amount} red dice");
    }
    public void ReceiveResult(Dice dice, int result)
    {
        gameObject.SetActive(true);
    }
    protected override void Awake()
    {
        base.Awake();
        GetComponent<Button>().onClick.AddListener(Roll);
    }

}
