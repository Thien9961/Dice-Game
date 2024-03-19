using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(SkeletonGraphic))]

public class Card : MonoBehaviour
{
    public Spell spell;
    public float revealTime;
    public TextMeshProUGUI description;
    public SkeletonGraphic animator;
    public static readonly string OPEN = "Open", CLOSED="Closed",GLOW="Loop";
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Reveal);
        animator = GetComponent<SkeletonGraphic>();
        animator.AnimationState.AddAnimation(0, CLOSED, true,0);
    }

    public void Reveal()
    {
        animator.AnimationState.SetAnimation(0, GLOW, true);
        spell.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        Extension.WaitForSeconds(spell, revealTime, spell.StartEffect);
    }
}

public class Open : State
{
    public Card card;
    public Open(Card card)
    {
        this.card = card;
        Queue<string> q = new();
        q.Enqueue(Card.OPEN); q.Enqueue(Card.CLOSED);
        foreach (string s in q)
            if(s!=Card.CLOSED)
                this.card.animator.AnimationState.AddAnimation(0, s, false,0);
            else
                this.card.animator.AnimationState.AddAnimation(0, s, true, 0);
    }
}

