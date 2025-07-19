using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : MonoBehaviour, IBehaviour
{

    public CharacterHealth characterHealth;

    private void Start()
    {
        characterHealth = FindObjectOfType<CharacterHealth>();
    }

    public float Evalaute()
    {
        if (characterHealth == null) return 0;
        
        return 3 / (this.gameObject.transform.position - characterHealth.transform.position).magnitude;
    }

    public void Behave()
    {
        transform.Rotate(Vector3.up, 10);
    }
}
