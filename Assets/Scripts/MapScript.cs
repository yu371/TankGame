using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class MapScript : MonoBehaviour
{
    private Camera camera;
    public GameObject obs;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    public void Map()
    {
        Vector2 prev = Vector2.zero;
        int c = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        camera.orthographicSize += players.Length;
        transform.localScale *= 1 + (float)players.Length / 7;
        GameObject up = transform.Find("up").gameObject;
        GameObject right = transform.Find("right").gameObject;
        Vector2 mapsize = new Vector2(up.transform.position.y - transform.position.y, right.transform.position.x - transform.position.x) / 2;
        float sx = Mathf.Sin(0) * mapsize.y * 1.3f;
        float sy = Mathf.Cos(0) * mapsize.x * 1.3f;

        foreach (GameObject player in players)
        {
            float x = Mathf.Sin(Mathf.PI * 2 * c / players.Length) * mapsize.y * 1.3f;
            float y = Mathf.Cos(Mathf.PI * 2 * c / players.Length) * mapsize.x * 1.3f;
            player.transform.position = new Vector2(x, y);
            if (prev != Vector2.zero)
            {
                GameObject o = Instantiate(obs, (new Vector2(x, y) + prev) / 2, Quaternion.identity);
                o.transform.localScale *= 1 + (float)players.Length / 7;
                o.transform.Rotate(0, 0, 360 * c / players.Length + 90);
                // o.transform.Find("Collider").GetComponent<Rigidbody2D>().AddForce(-o.transform.up * 10, ForceMode2D.Impulse);
            }
            prev = player.transform.position;
            c++;
        }
        GameObject s = Instantiate(obs, (new Vector2(sx, sy) + prev) / 2, Quaternion.identity);
        s.transform.localScale *= 1 + (float)players.Length / 7;
        s.transform.Rotate(0, 0, 360 * c / players.Length + 90f);
        // s.transform.Find("Collider").GetComponent<Rigidbody2D>().AddForce(-s.transform.up * 100, ForceMode2D.Impulse);
    }
}
