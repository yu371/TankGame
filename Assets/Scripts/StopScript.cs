using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "wall")
        {
            Debug.Log("AA");
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
