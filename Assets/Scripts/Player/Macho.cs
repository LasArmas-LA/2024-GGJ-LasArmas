using RunGame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Macho : MonoBehaviour
{
    // 初期選択オブジェクトを指定します。
    [SerializeField]
    private Selectable firstSelected = null;
    [SerializeField, Header("歩く速度")]
    private float walkSpeed = 1;
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed = 1;
    [SerializeField, Header("踏みつけジェンプ速度")]
    private float jSpeed = 1;
    [SerializeField, Header("足速度")]
    private float DSpeed = 1;
    [SerializeField, Header("Enemy")]
    private GameObject enemy;
    [SerializeField, Header("足")]
    private GameObject feet;
    // ジャンプのサウンドを指定します。
    [SerializeField, Header("ジャンプサウンド")]
    private AudioClip soundOnJump = null;
    [SerializeField]
    private GameOverUI gameOverUI;
    [SerializeField]
    private AudioSource audio = null;

    //　コンポーネントを参照しておく変数
    Animator animator;
    Vector2 inputDirection;
    Rigidbody2D rb;
    RunGame2023 inputSys;
    bool isJump;
    bool isWalk;
    bool isFeet;
    AudioSource audioSource;

    // AnimatorのパラメーターID
    static readonly int isWalkId = Animator.StringToHash("isWalk");
    static readonly int isRunId = Animator.StringToHash("isRun");
    static readonly int jumpId = Animator.StringToHash("isJump");
    static readonly int landingId = Animator.StringToHash("Landing");
    static readonly int showId = Animator.StringToHash("Show");


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputSys = new RunGame2023();
        inputSys.Enable();
        isJump = false;
    }

    private void Update()
    {
        Walk();
        LookMove();
        if(isFeet)
        {
            feet.transform.position -= DSpeed * transform.right * Time.deltaTime;
        }
    }
    private void Walk()
    {
        rb.velocity = new Vector2(inputDirection.x * walkSpeed, rb.velocity.y);

        animator.SetBool(isWalkId, inputDirection.x != 0.0f);

        //if (!isJump)
        //{
        //    SetJumpState();
        //}

    }

    private void LookMove()
    {
        if(inputDirection.x > 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (inputDirection.x < 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("空中");
            isJump = false;
            animator.SetBool(jumpId, isJump);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
            Debug.Log("痛い");
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            Destroy(enemy);
        }
        else if (collision.gameObject.CompareTag("Damage"))
        {
            Debug.Log("fvf");
            Destroy(this);
            Show();
            audio.Stop();
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            feet.SetActive(true);
            isFeet = true;
            //rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        animator.SetBool(isWalkId, true);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || isJump)
        {
            return;
        }

        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        isJump = true;
        animator.SetBool(jumpId, isJump);
    }

    //public void EnemyDamege()
    //{
    //    rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    //}

    // このUIを表示します。
    public void Show()
    {
        animator.SetTrigger(showId);
        StartCoroutine(OnShow());
    }

    IEnumerator OnShow()
    {
        // 2秒間待機
        yield return new WaitForSeconds(2);
        // YESボタンを選択状態に設定する
        firstSelected.Select();
    }

}
