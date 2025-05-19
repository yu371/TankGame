using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public NetworkManeger networkManeger;
    public PlayerData player;
    void Start()
    {
        player = new PlayerData();
        player.ish = 0;
    }
    public void ChangeName(int id)
    {
        player.id = id;
        Camera.main.backgroundColor = GetColorByPlayerId(id);
    }
    public static Color GetColorByPlayerId(int id)
    {
        float hue = (id * 0.17f) % 1f;
        float saturation = 0.8f;
        float value = 0.9f;
        return Color.HSVToRGB(hue, saturation, value);
    }

    public void Up() { player.y = 1; SendState(); }
    public void YCanceled() { player.y = 0; SendState(); }

    public void Down() { player.y = -1; SendState(); }
    public void Right() { player.x = 1; SendState(); }
    public void XCanceled() { player.x = 0; SendState(); }

    public void Left() { player.x = -1; SendState(); }
    public void Attack() { player.at = 1; SendState(); }
    public void AttackCanceled() { player.at = 0; SendState(); }

    private void SendState()
    {
        string json = JsonUtility.ToJson(player, true);
        Debug.Log(json);
        if (networkManeger != null)
            networkManeger.Send(json);
    }
}
