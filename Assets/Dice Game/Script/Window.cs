using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public List<Button> buttons=new();
    private void Awake()
    {
        foreach (var button in buttons)
            button.onClick.AddListener(() => { gameObject.SetActive(false); });
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
