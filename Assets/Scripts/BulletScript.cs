using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool isStart = false;
    private string name;
    private int count;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player") && isStart == true && other.gameObject.name != name)
        {
            GameScript gameFinish = GameObject.FindWithTag("game").GetComponent<GameScript>();
            gameFinish.players.Add(other.gameObject);
            other.gameObject.SetActive(false);
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
            if (objs.Length == 1)
            {
                gameFinish.Finish();
            }
        }
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("wall"))
        {
            count += 1;
            if (count > 2)
            {
                Destroy(gameObject);
            }
        }
    }

}
