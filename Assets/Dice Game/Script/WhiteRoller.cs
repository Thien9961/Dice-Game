using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WhiteRoller : Singleton<WhiteRoller>,IRoller,IDiceListener
{
    public RollerSetting settings { get; set; }
    public bool active { get; set; }
    public int whiteDice { get { return int.Parse(diceQty.text); } set { diceQty.text = value.ToString(); } }
    public TextMeshProUGUI diceQty;
    public static List<IDiceListener> listener { get; set; }
    public void Roll()
    {
        if (whiteDice > 0)
        {
            whiteDice--;
            Invoke(nameof(GetResult), settings.rollTime);
        }

    }
    public int GetResult()
    {
        listener.ForEach(x => x.Wait(this));
        return Random.Range(1, 7);
    }

    public void Refresh()
    {
        gameObject.SetActive(true);
    }

    public void Wait(IRoller roller)
    {
        Invoke(nameof(Refresh), roller.settings.showTime);
        gameObject.SetActive(false);
    }
    protected override void Awake()
    {
        base.Awake();
        whiteDice = settings.dice;
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
