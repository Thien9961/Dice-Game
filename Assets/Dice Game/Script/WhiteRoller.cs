using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WhiteRoller : Singleton<WhiteRoller>,IRoller,IDiceListener
{
    public Dice dice;
    public int whiteDice { get { return int.Parse(diceQty.text); } set { diceQty.text = value.ToString(); } }
    public TextMeshProUGUI diceQty;
    public void Roll()
    {
        if (whiteDice > 0)
        {
            whiteDice--;
            Instantiate(dice,UIManager.instance.transform).Roll();
        }
    }
    public void WaitForResult(Dice dice)
    {
        gameObject.SetActive(false);
    }
    public void WaitForPublish(Dice dice)
    {

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
