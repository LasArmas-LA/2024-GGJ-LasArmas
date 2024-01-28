using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    //[SerializeField]
    //private Macho player;
    [SerializeField, Header("ジャンプの高さ")]
    private float jumpSpeed = 1f;

    // コンポーネントを参照しておく変数
    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= speed * transform.right * Time.deltaTime;
    }


}
