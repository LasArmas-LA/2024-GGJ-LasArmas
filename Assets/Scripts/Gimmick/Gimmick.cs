using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed = 1f;

    // コンポーネントを参照しておく変数
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deth"))
        {

            //rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

}
