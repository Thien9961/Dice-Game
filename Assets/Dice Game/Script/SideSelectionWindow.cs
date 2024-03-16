using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideSelectionWindow : Singleton<SideSelectionWindow>,IWindow
{
    public Button X;
    public GameObject dice;
    public Button exit { get; set; }
    protected override void Awake()
    {
        base.Awake();
        X.onClick.AddListener(Exit);
        Button side;
        for(int i = 0; i<dice.transform.childCount; i++)
        {
            if (dice.transform.GetChild(i).TryGetComponent(out side) && i < 6)
            {
                side.onClick.AddListener(() => { RedRoller.instance.guarantee = i + 1; Debug.Log("Guarantee " + i); });
            }
            else if (i >= 6)
                break;
            
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
        RedRoller.instance.Roll();
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
