using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RedRoller : Singleton<RedRoller>, IRoller,IDiceListener
{
    public GameObject selectionWindow;
    public int guarantee;
    public RollerSetting settings { get; set; }
    public bool active { get; set; }
    public int redDice { get { return int.Parse(diceQty.text); } set { diceQty.text = value.ToString(); } }
    public TextMeshProUGUI diceQty;
    public static List<IDiceListener> listener { get; set; }
    public void Roll()
    {
        if (redDice > 0)
        {
            redDice--;
            Invoke(nameof(GetResult), settings.rollTime);
        }
    }
    public int GetResult()
    {
        listener.ForEach(x => x.Wait(this));
        return guarantee;
    }
    public void Refresh()
    {
        gameObject.SetActive(true);
        ResourceCell cell = new ResourceCell();
    }

    public void Wait(IRoller roller)
    {
        Invoke(nameof(Refresh), roller.settings.showTime);
        
        gameObject.SetActive(false);
    }
    protected override void Awake()
    {
        base.Awake();
        redDice = settings.dice;
        Instantiate(selectionWindow);
        GetComponent<Button>().onClick.AddListener(() => { SideSelectionWindow.instance.gameObject.SetActive(true); });
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
