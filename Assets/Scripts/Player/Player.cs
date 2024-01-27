using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [SerializeField, Header("歩く速度")]
    private float walkSpeed = 1;
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed = 1;
    // ダッシュ時のサウンドを指定します。
    [SerializeField]
    private AudioClip soundOnJump = null;


    // プレイヤーの状態を表します。
    enum PlayerState
    {
        // 立ち状態
        Stand,
        // 通常の走行状態
        Walk,
        // ダッシュ時の走行状態
        Jump,
        // ジャンプの予備動作
        JumpReady,
        // 地面から足が離れて上昇中
        Jumping,
    }
    // 現在のプレイヤー状態
    PlayerState currentState = PlayerState.Stand;

    //　コンポーネントを参照しておく変数
    Animator animator;
    Vector2 inputDirection;
    Rigidbody2D rb;
    RunGame2023 inputSys;
    bool isJump;
    bool isWalk;
    AudioSource audioSource;


    // AnimatorのパラメーターID
    static readonly int isWalkId = Animator.StringToHash("isWalk");
    static readonly int isRunId = Animator.StringToHash("isRun");
    static readonly int jumpId = Animator.StringToHash("isJump");
    static readonly int landingId = Animator.StringToHash("Landing");

    // Jumpステートに遷移させます。
    void SetJumpState()
    {
        currentState = PlayerState.JumpReady;
        animator.SetBool(jumpId, true);

        // ジャンプ音を再生
        //if (audioSource.isPlaying)
        //{
        //    audioSource.Stop();
        //}
        //audioSource.PlayOneShot(soundOnJump);
        //audioSource.Play();
    }

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
        // 状態ごとの分岐処理
        switch (currentState)
        {
            case PlayerState.Stand:
                UpdateForStandState();
                break;
            case PlayerState.Walk:
                UpdateForWalkState();
                break;
            case PlayerState.JumpReady:
                UpdateForJumpReadyState();
                break;
            case PlayerState.Jumping:
                UpdateForJumpingState();
                break;
            default:
                break;
        }

    }

    private void UpdateForStandState()
    {
        currentState = PlayerState.Walk;
    }

    // Walk状態のフレーム更新処理です。
    private void UpdateForWalkState()
    {
        rb.velocity = new Vector2(inputDirection.x * walkSpeed, rb.velocity.y);
        
        animator.SetBool(isWalkId, inputDirection.x != 0.0f);

        if(!isJump)
        {
            SetJumpState();
        }
    }

    // JumpReady状態のフレーム更新処理です。
    void UpdateForJumpReadyState()
    {
        Debug.Log("ジャンプ");
        currentState = PlayerState.Jumping;
    }

    // Jumping状態のフレーム更新処理です。
    void UpdateForJumpingState()
    {
        Debug.Log("ジャンプからの着地");
        animator.SetTrigger(landingId);
        currentState = PlayerState.Stand;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("地面");
            isJump = false;
            //animator.SetBool(jumpId, isJump);
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
}
