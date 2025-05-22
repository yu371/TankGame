using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Common;
using System.Runtime.InteropServices;
using UnityEngine.Assertions.Must;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NetworkManeger : MonoBehaviour
{
    public List<int> idlist;
    public Dictionary<string, Vector2> dic = new Dictionary<string, Vector2>();
    public Dictionary<string, int> Attackdic = new Dictionary<string, int>();
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void StartWebSocket(string url);

    [DllImport("__Internal")]
    private static extern void SendWebSocketMessage(string msg);
#endif
    private string url = "wss://render-two-n30r.onrender.com";
    public bool isHost = false;
    public ControllerScript controllerScript;
    public GameObject Player;
    public PlayerData playerdate;
    private ClientWebSocket ws;
    public bool isOnce;
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        Connection();
        if (isHost == true)
        {
            playerdate = new PlayerData();
            playerdate.ish = 1;
        }
        // Invoke("SendStart",5f);
    }
    public void HostSend(string msg)
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in Players)
        {
            Destroy(p);
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        SendWebSocketMessage(msg);
         string json = JsonUtility.ToJson(playerdate, true);
        SendWebSocketMessage(json);
#else
        Debug.Log("[Editor] Would send: " + msg);
#endif
    }
    public void Send(string msg)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SendWebSocketMessage(msg);
#else

#endif
    }
    public void Connection()
    {
        // url = textMeshProUGUI.text;
#if UNITY_WEBGL && !UNITY_EDITOR

    // スマホ WebGL クライアント用（送信用）
    //ws://192.168.11.6:8000
    StartWebSocket(url); // サーバーPCのIP
#else

#endif
    }
    public TextMeshProUGUI textMeshProUGUI;

    public void OnMessageFromServer(string msg)
    {
        if (isHost == false && isOnce == false)
        {
            isOnce = true;
            ID id = JsonUtility.FromJson<ID>(msg);
            controllerScript.ChangeName(int.Parse(id.id));
        }
        else
        {
            JObject obj = JObject.Parse(msg);
            if (obj.ContainsKey("txt"))
            {
                Close close = JsonUtility.FromJson<Close>(msg);
                Destroy(GameObject.Find(close.id.ToString()).gameObject);
            }
            else
            {
                ID data = JsonUtility.FromJson<ID>(msg);
                int playerId = int.Parse(data.id);
                if (idlist.Contains(playerId) == false && isStart == false)
                {
                    idlist.Add(playerId);
                    GameObject p = Instantiate(Player, Vector2.zero, Quaternion.identity);
                    p.name = playerId.ToString();
                    dic.Add(playerId.ToString(), new Vector2(0, 0));
                    Attackdic.Add(playerId.ToString(), 0);
                }
                else
                {
                    PlayerData playerData = JsonUtility.FromJson<PlayerData>(msg);
                    dic[playerData.id.ToString()] = new Vector2(playerData.x, playerData.y);
                    Attackdic[playerData.id.ToString()] = playerData.at;
                }
            }
        }
    }
}
public class ID
{
    public string id;
}
public class Close
{
    public string txt;
    public int id;
}