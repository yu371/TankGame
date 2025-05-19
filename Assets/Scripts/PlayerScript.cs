using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rigidbody2D;
    public float power = 1f;
    public float dashspeed;
    private float s;
    public float rpower = 1;
    private NetworkManeger networkManeger;
    public SpriteRenderer spriteRenderer;
    private int attack;
    public GameObject bullet;
    public float attackpower;
    public Transform bulletpoj;
    public bool isStart;
    public bool isEnd;
    void Start()
    {
        s = power;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        networkManeger = GameObject.FindWithTag("NM").GetComponent<NetworkManeger>();
        spriteRenderer.color = GetColorByPlayerId(int.Parse(gameObject.name));
        GetComponent<SpriteRenderer>().color = GetColorByPlayerId(int.Parse(gameObject.name));
    }
    public bool IsSpan = true;
    private float rotate;
    void Isspanfalse()
    {
        IsSpan = true;
    }
    void FixedUpdate()
    {

        moveInput = networkManeger.dic[gameObject.name];
        attack = networkManeger.Attackdic[gameObject.name];
        if (attack == 1)
        {
            attackpower += Time.deltaTime;
        }
        else
        {
            if (attackpower > 0 && IsSpan)
            {
                IsSpan = false;
                Invoke("Isspanfalse", 0.5f);
                GameObject b = Instantiate(bullet, bulletpoj.position, Quaternion.identity);
                b.GetComponent<Rigidbody2D>().AddForce((5f + 10f * attackpower) * transform.right, ForceMode2D.Impulse);
                b.GetComponent<SpriteRenderer>().color = GetColorByPlayerId(int.Parse(gameObject.name));
                b.name = gameObject.name;
                attackpower = 0;
                b.GetComponent<BulletScript>().isStart = isStart;
            }
        }
        if (moveInput.magnitude > 0)
        {
            rigidbody2D.AddForce(transform.right * moveInput.y * s, ForceMode2D.Impulse);
        }
        else if (rigidbody2D.velocity.magnitude > 0)
        {
            rigidbody2D.velocity -= rigidbody2D.velocity / 5;

            // rigidbody2D.velocity = Vector2.zero;
        }
        transform.Rotate(new Vector3(0, 0, moveInput.x * rpower));
    }
    public static Color GetColorByPlayerId(int id)
    {
        float hue = (id * 0.17f) % 1f;
        float saturation = 0.8f;
        float value = 0.9f;
        return Color.HSVToRGB(hue, saturation, value);
    }
    // public void OnPerformed(InputAction.CallbackContext context)
    // {
    //     // Actionの入力値を読み込む
    //     moveInput = context.ReadValue<Vector2>();
    // }
    // public void OnAttack(InputAction.CallbackContext context)
    // {
    //     GameObject b = Instantiate(bullet, bulletpoj.position, Quaternion.identity);
    //     b.GetComponent<Rigidbody2D>().AddForce((5f + 10f * attackpower) * transform.right, ForceMode2D.Impulse);
    //     b.transform.rotation = transform.rotation;
    //     b.name = gameObject.name;
    //     attackpower = 0;
    //     b.GetComponent<BulletScript>().isStart = isStart;
    // }
}
