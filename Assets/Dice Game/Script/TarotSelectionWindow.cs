using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotSelectionWindow : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform displayer;
    public int maxCard=3;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < maxCard; i++) 
            Instantiate(deck[Random.Range(0,deck.Count)],displayer);
    }

}
