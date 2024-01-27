using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField, Header("歩く速度")]
    private float walkSpeed = 1;
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed = 1;


    //　先に宣言しておく変数
    Animator animator;
    Vector2 inputDirection;
    Rigidbody2D rb;
    RunGame2023 inputSys;
    bool isJump;

    // AnimatorのパラメーターID
    static readonly int isWalkId = Animator.StringToHash("isWalk");
    static readonly int isRunId = Animator.StringToHash("isRun");
    static readonly int jumpId = Animator.StringToHash("Jump");
    static readonly int landingId = Animator.StringToHash("Landing");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputSys = new RunGame2023();
        inputSys.Enable();
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * walkSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        animator.SetBool(isWalkId, true);
    }

    public void OnJumo(InputAction.CallbackContext context)
    {
        if (!context.performed || isJump)
        {
            return;
        }

        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        isJump = true;
        animator.SetBool(isRunId, true);
    }
}
