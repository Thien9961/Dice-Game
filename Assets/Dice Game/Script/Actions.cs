using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public void Disable()
    {
        Destroy(gameObject);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
