using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBehaviour : MonoBehaviour, IBehaviour
{
    public float Evalaute()
    {
        return 0.5f;
    }

    public void Behave()
    {
        Debug.Log("WAIT");
    }
}
