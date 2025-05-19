using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObs : MonoBehaviour
{
    private Vector3 poj;
    private float Length = 0.08f;
    private bool isDirection;
    private float time;
    private int span;
    void Start()
    {
        poj += transform.up * Length;
        span = Random.Range(0, 1);

    }
    private void FixedUpdate()
    {

        if (isDirection)
        {
            time += Time.deltaTime * (1 + (float)span / 10) / 3;
        }
        else
        {
            time -= Time.deltaTime * (1 + (float)span / 10) / 3;
        }
        if (time > 1)
        {
            isDirection = false;
        }
        if (time < 0)
        {
            isDirection = true;
        }
        transform.position = Vector3.Lerp(transform.position - poj, transform.position + poj, time);
    }
}
