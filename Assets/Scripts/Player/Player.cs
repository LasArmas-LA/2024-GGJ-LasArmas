using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [SerializeField, Header("�������x")]
    private float walkSpeed = 1;
    [SerializeField, Header("�W�����v���x")]
    private float jumpSpeed = 1;
    // �_�b�V�����̃T�E���h���w�肵�܂��B
    [SerializeField]
    private AudioClip soundOnJump = null;


    // �v���C���[�̏�Ԃ�\���܂��B
    enum PlayerState
    {
        // �������
        Stand,
        // �ʏ�̑��s���
        Walk,
        // �_�b�V�����̑��s���
        Jump,
        // �W�����v�̗\������
        JumpReady,
        // �n�ʂ��瑫������ď㏸��
        Jumping,
    }
    // ���݂̃v���C���[���
    PlayerState currentState = PlayerState.Stand;

    //�@�R���|�[�l���g���Q�Ƃ��Ă����ϐ�
    Animator animator;
    Vector2 inputDirection;
    Rigidbody2D rb;
    RunGame2023 inputSys;
    bool isJump;
    bool isWalk;
    AudioSource audioSource;


    // Animator�̃p�����[�^�[ID
    static readonly int isWalkId = Animator.StringToHash("isWalk");
    static readonly int isRunId = Animator.StringToHash("isRun");
    static readonly int jumpId = Animator.StringToHash("isJump");
    static readonly int landingId = Animator.StringToHash("Landing");

    // Jump�X�e�[�g�ɑJ�ڂ����܂��B
    void SetJumpState()
    {
        currentState = PlayerState.JumpReady;
        animator.SetBool(jumpId, true);

        // �W�����v�����Đ�
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
        // ��Ԃ��Ƃ̕��򏈗�
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

    // Walk��Ԃ̃t���[���X�V�����ł��B
    private void UpdateForWalkState()
    {
        rb.velocity = new Vector2(inputDirection.x * walkSpeed, rb.velocity.y);
        
        animator.SetBool(isWalkId, inputDirection.x != 0.0f);

        if(!isJump)
        {
            SetJumpState();
        }
    }

    // JumpReady��Ԃ̃t���[���X�V�����ł��B
    void UpdateForJumpReadyState()
    {
        Debug.Log("�W�����v");
        currentState = PlayerState.Jumping;
    }

    // Jumping��Ԃ̃t���[���X�V�����ł��B
    void UpdateForJumpingState()
    {
        Debug.Log("�W�����v����̒��n");
        animator.SetTrigger(landingId);
        currentState = PlayerState.Stand;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("�n��");
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
