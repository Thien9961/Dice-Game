
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WhiteRoller : Singleton<WhiteRoller>,IRoller,IDiceListener,IContainer
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
    public void WaitForPublish(Dice dice, int r)
    {

    }
    public void ReceiveResult(Dice dice, int result)
    {
        gameObject.SetActive(true);
    }

    public void Add(ResourceCell cell, float amount)
    {
        whiteDice += Mathf.RoundToInt(amount);
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
