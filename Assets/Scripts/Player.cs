using UnityEngine;

namespace RunGame
{
    // ���[�U�[���͂��󂯎���ăv���C���[�𑀍삵�܂��B
    public class Player : MonoBehaviour
    {
        // �ʏ펞�̈ړ����x���w�肵�܂��B
        [SerializeField]
        [Tooltip("�ʏ펞�̈ړ����x���w�肵�܂��B")]
        private float walkSpeed = 4;
        // �_�b�V�����̈ړ����x���w�肵�܂��B
        [SerializeField]
        [Tooltip("�_�b�V�����̈ړ����x���w�肵�܂��B")]
        private float runSpeed = 8;
        // ���݂̈ړ����x
        float speed = 4;

        // �W�����v�͂��w�肵�܂��B
        [SerializeField]
        [Tooltip("�W�����v�͂��w�肵�܂��B")]
        private Vector2 jumpPower = new Vector2(0, 6);
        // ���ʏՓˎ��̃m�b�N�o�b�N�͂��w�肵�܂��B
        [SerializeField]
        [Tooltip("���ʏՓˎ��̃m�b�N�o�b�N�͂��w�肵�܂��B")]
        private Vector2 knockBackPower = new Vector2(-16, 4);

        // �]�|�ɂ��Q�[���I�[�o�[�Ɣ��肷��^�C���A�E�g���ԁi�b�j���w�肵�܂��B
        [SerializeField]
        private float tumbleTimeout = 1.5f;
        // �]�|���Ă���ݐώ���(�b)
        float tumbleTime = 0;

        // �n�ʂƐڐG���Ă���ꍇ��true�A�󒆂ɂ���ꍇ��false
        [SerializeField]
        private bool isGrounded = false;
        // �n�ʃ��C���[���w�肵�܂��B
        [SerializeField]
        private LayerMask groundLayer;
        // �n�ʂƂ̔�������邽�߂̃`�F�b�J�[���w�肵�܂��B
        [SerializeField]
        private Transform groundChecker = null;
        // �ǂƂ̔�������邽�߂̃`�F�b�J�[���w�肵�܂��B
        [SerializeField]
        private Transform wallChecker = null;
        // �_�b�V�����̃T�E���h���w�肵�܂��B
        [SerializeField]
        private AudioClip soundOnDash = null;

        // �v���C���[�̏�Ԃ�\���܂��B
        enum PlayerState
        {
            // �ʏ�̑��s���
            Walk,
            // �_�b�V�����̑��s���
            Run,
            // �W�����v�̗\������
            JumpReady,
            // �n�ʂ��瑫������ď㏸��
            Jumping,
        }
        // ���݂̃v���C���[���
        PlayerState currentState = PlayerState.Walk;

        // �R���|�[�l���g���Q�Ƃ��Ă����ϐ�
        new Rigidbody2D rigidbody;
        AudioSource audioSource;
        Animator animator;
        // Animator�̃p�����[�^�[ID
        static readonly int isRunId = Animator.StringToHash("isRun");
        static readonly int jumpId = Animator.StringToHash("Jump");
        static readonly int landingId = Animator.StringToHash("Landing");

        // �ŏ��̃t���[���X�V�����s�����O�ɌĂяo����܂��B
        void Start()
        {
            // �R���|�[�l���g�����O�ɎQ��
            rigidbody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();

            currentState = PlayerState.Walk;
            speed = walkSpeed;
        }

        // Walk�X�e�[�g�ɑJ�ڂ����܂��B
        void SetWalkState()
        {
            currentState = PlayerState.Walk;
            animator.SetBool(isRunId, false);
            speed = walkSpeed;
        }

        // Run�X�e�[�g�ɑJ�ڂ����܂��B
        void SetRunState()
        {
            currentState = PlayerState.Run;
            animator.SetBool(isRunId, true);
            speed = runSpeed;

            // �_�b�V�������Đ�
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = soundOnDash;
            audioSource.loop = true;
            audioSource.Play();
        }

        // ���������ꍇ�ɌĂяo����܂��B
        void Fall()
        {
            currentState = PlayerState.Jumping;
            animator.SetTrigger(jumpId);
        }

        // ���̃L�����N�^�[���W�����v�����܂��B
        public void Jump()
        {
            rigidbody.AddForce(jumpPower, ForceMode2D.Impulse);
            Debug.Log("�W�����v");
            currentState = PlayerState.JumpReady;
            animator.SetTrigger(jumpId);
        }

        // ���t���[���Ɉ�x���s�����X�V�����ł��B
        void Update()
        {
            // ��Ԃ��Ƃ̕��򏈗�
            switch (currentState)
            {
                case PlayerState.Walk:
                    UpdateForWalkState();
                    break;
                case PlayerState.Run:
                    UpdateForRunState();
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

        // Walk��Ԃ̃t���[���X�V�����ł��B
        private void UpdateForWalkState()
        {
            // �ڒn��Ԃ̏ꍇ
            if (isGrounded)
            {
                // �W�����v
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
                // �_�b�V��
                if (Input.GetButtonDown("Fire1"))
                {
                    SetRunState();
                }

                // �����x�^��
                var velocity = rigidbody.velocity;
                velocity.x = speed;
                rigidbody.velocity = velocity;
            }
            else
            {
                Debug.Log("Walk��Ԃ���̗���");
                Fall();
            }
        }

        // Run��Ԃ̃t���[���X�V�����ł��B
        private void UpdateForRunState()
        {
            // �ڒn��Ԃ̏ꍇ
            if (isGrounded)
            {
                // �_�b�V������
                if (Input.GetButtonUp("Fire1"))
                {
                    Debug.Log("�_�b�V������");
                    audioSource.Stop();
                    SetWalkState();
                }

                // �����x�^��
                var velocity = rigidbody.velocity;
                velocity.x = speed;
                rigidbody.velocity = velocity;
            }
            else
            {
                Debug.Log("Run��Ԃ���̗���");
                audioSource.Stop();
                Fall();
            }
        }

        // JumpReady��Ԃ̃t���[���X�V�����ł��B
        void UpdateForJumpReadyState()
        {
            // �W�����v�̗\�������A�������ꂽ�ꍇ
            if (!isGrounded)
            {
                Debug.Log("�W�����v����");
                currentState = PlayerState.Jumping;
            }
        }

        // Jumping��Ԃ̃t���[���X�V�����ł��B
        void UpdateForJumpingState()
        {
            // �W�����v��Ԃ���̒��n����
            if (isGrounded)
            {
                Debug.Log("�W�����v����̒��n");
                animator.SetTrigger(landingId);
                SetWalkState();
            }
        }

        // �Œ�t���[�����[�g�ŌĂяo�����X�V�����ł��B
        void FixedUpdate()
        {
            // �ڒn����
            isGrounded = Physics2D.OverlapBox(
                groundChecker.position,
                groundChecker.lossyScale,
                groundChecker.eulerAngles.z,
                groundLayer);
            if (isGrounded)
            {
                // �]�|���Ă���ꍇ�͐ڒn�ƔF�߂Ȃ�
                if (IsTumble())
                {
                    isGrounded = false;
                }
            }

            // �ǂƂ̏Փ˔���
            var result = Physics2D.OverlapBox(
                wallChecker.position,
                wallChecker.lossyScale,
                wallChecker.eulerAngles.z,
                groundLayer);
            // ����t���[���ŏՓ�
            if (result)
            {
                // �]�|���������āA�ǂƂ̋������Ƃ邽�߂Ƀm�b�N�o�b�N����
                if (!IsTumble())
                {
                    rigidbody.AddForce(transform.TransformDirection(knockBackPower), ForceMode2D.Force);
                }
            }

            // �]�|�ɂ��Q�[���I�[�o�[�𔻒�
            if (IsTumble())
            {
                // �]�|��Ԃ��L�[�v����Ă��鎞�Ԃ��v������
                tumbleTime += Time.fixedDeltaTime;
                if (tumbleTime > tumbleTimeout)
                {
                    StageScene.Instance.GameOver();
                }
            }
            else
            {
                tumbleTime = 0;
            }
        }

        // �]�|��Ԃ̏ꍇ��true��Ԃ��܂��B
        bool IsTumble()
        {
            var zAngle = Mathf.Repeat(rigidbody.rotation, 360);
            return zAngle > 60 && zAngle < 300;
        }

        // �v���C���[�����̃I�u�W�F�N�g�̃g���K�[�ɐN�������ۂɌĂяo����܂��B
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �Q�[���I�[�o�[����
            if (collision.CompareTag("DeadArea"))
            {
                StageScene.Instance.GameOver();
            }
        }
    }
}