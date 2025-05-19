using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameScript : MonoBehaviour
{

    private float time;
    public TextMeshProUGUI textMeshProUGUI;
    private bool isStart;
    public GameObject startbutton;
    public List<GameObject> players;
    public Transform Winnerpoj;
    public GameObject resetbutton;
    public TextMeshProUGUI textMeshPro;
    public GameObject winner;
    public void Finish()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in objs)
        {
            if (players.Contains(obj) == false)
            {
                winner = obj;
                textMeshPro.text = "WINNER!!";
                obj.transform.position = Winnerpoj.transform.position;
                Invoke("Reset", 1f);
            }
        }
    }
    public void Reset()
    {
        players = null;
        winner.GetComponent<PlayerScript>().isStart = false;
        foreach (GameObject player in players)
        {
            player.SetActive(true);
            player.GetComponent<PlayerScript>().isStart = false;
        }
        textMeshPro.text = "";
        resetbutton.SetActive(true);
    }
    public void FixedUpdate()
    {
        if (isStart == true)
        {
            time += Time.deltaTime;
            if (3.5 - time > 0.5)
            {
                textMeshProUGUI.text = (3.5 - time).ToString("N0");
            }
            else if (3.5 - time > 0)
            {
                textMeshProUGUI.text = "START!!";
            }
            else
            {
                textMeshProUGUI.text = "";
                isStart = false;
                time = 0;
                GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
                startbutton.SetActive(false);
                foreach (GameObject obj2 in obj)
                {
                    obj2.GetComponent<PlayerScript>().isStart = true;
                }
            }
        }
    }

    public void Gamestart()
    {
        isStart = true;
    }
}
