using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] targets;
    // Start is called before the first frame update    

    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }
    void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}
