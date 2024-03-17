using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public void Disable()
    {
        DestroyImmediate(gameObject);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
