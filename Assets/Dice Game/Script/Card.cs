using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class Card : MonoBehaviour
{
    public Spell spell;
    public float revealTime;
    public TextMeshProUGUI description;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Reveal);
    }

    public void Reveal()
    {
        spell.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        Extension.WaitForSeconds(spell, revealTime, spell.StartEffect);
    }
}



