
using UnityEngine;
using UnityEngine.UI;

public class SideSelectionWindow : MonoBehaviour
{
    public Button X;
    public RedDice[] dice=new RedDice[6];
    RedDice _dice;
    void Awake()
    {
        X.onClick.AddListener(Exit);
        foreach (RedDice d in dice)
        {
            d.GetComponent<Button>().onClick.AddListener(() => { X.interactable = true; _dice = d; });
        }
    }

    public void Exit()
    {
        Instantiate(_dice, UIManager.instance.transform).Roll();
        DestroyImmediate(gameObject);
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
