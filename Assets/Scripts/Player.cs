using UnityEngine;

namespace RunGame
{
    // ユーザー入力を受け取ってプレイヤーを操作します。
    public class Player : MonoBehaviour
    {
        // 通常時の移動速度を指定します。
        [SerializeField]
        [Tooltip("通常時の移動速度を指定します。")]
        private float walkSpeed = 4;
        // ダッシュ時の移動速度を指定します。
        [SerializeField]
        [Tooltip("ダッシュ時の移動速度を指定します。")]
        private float runSpeed = 8;
        // 現在の移動速度
        float speed = 4;

        // ジャンプ力を指定します。
        [SerializeField]
        [Tooltip("ジャンプ力を指定します。")]
        private Vector2 jumpPower = new Vector2(0, 6);
        // 側面衝突時のノックバック力を指定します。
        [SerializeField]
        [Tooltip("側面衝突時のノックバック力を指定します。")]
        private Vector2 knockBackPower = new Vector2(-16, 4);

        // 転倒によるゲームオーバーと判定するタイムアウト時間（秒）を指定します。
        [SerializeField]
        private float tumbleTimeout = 1.5f;
        // 転倒している累積時間(秒)
        float tumbleTime = 0;

        // 地面と接触している場合はtrue、空中にいる場合はfalse
        [SerializeField]
        private bool isGrounded = false;
        // 地面レイヤーを指定します。
        [SerializeField]
        private LayerMask groundLayer;
        // 地面との判定をするためのチェッカーを指定します。
        [SerializeField]
        private Transform groundChecker = null;
        // 壁との判定をするためのチェッカーを指定します。
        [SerializeField]
        private Transform wallChecker = null;
        // ダッシュ時のサウンドを指定します。
        [SerializeField]
        private AudioClip soundOnDash = null;

        // プレイヤーの状態を表します。
        enum PlayerState
        {
            // 通常の走行状態
            Walk,
            // ダッシュ時の走行状態
            Run,
            // ジャンプの予備動作
            JumpReady,
            // 地面から足が離れて上昇中
            Jumping,
        }
        // 現在のプレイヤー状態
        PlayerState currentState = PlayerState.Walk;

        // コンポーネントを参照しておく変数
        new Rigidbody2D rigidbody;
        AudioSource audioSource;
        Animator animator;
        // AnimatorのパラメーターID
        static readonly int isRunId = Animator.StringToHash("isRun");
        static readonly int jumpId = Animator.StringToHash("Jump");
        static readonly int landingId = Animator.StringToHash("Landing");

        // 最初のフレーム更新が実行される前に呼び出されます。
        void Start()
        {
            // コンポーネントを事前に参照
            rigidbody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();

            currentState = PlayerState.Walk;
            speed = walkSpeed;
        }

        // Walkステートに遷移させます。
        void SetWalkState()
        {
            currentState = PlayerState.Walk;
            animator.SetBool(isRunId, false);
            speed = walkSpeed;
        }

        // Runステートに遷移させます。
        void SetRunState()
        {
            currentState = PlayerState.Run;
            animator.SetBool(isRunId, true);
            speed = runSpeed;

            // ダッシュ音を再生
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = soundOnDash;
            audioSource.loop = true;
            audioSource.Play();
        }

        // 落下した場合に呼び出されます。
        void Fall()
        {
            currentState = PlayerState.Jumping;
            animator.SetTrigger(jumpId);
        }

        // このキャラクターをジャンプさせます。
        public void Jump()
        {
            rigidbody.AddForce(jumpPower, ForceMode2D.Impulse);
            Debug.Log("ジャンプ");
            currentState = PlayerState.JumpReady;
            animator.SetTrigger(jumpId);
        }

        // 毎フレームに一度実行される更新処理です。
        void Update()
        {
            // 状態ごとの分岐処理
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

        // Walk状態のフレーム更新処理です。
        private void UpdateForWalkState()
        {
            // 接地状態の場合
            if (isGrounded)
            {
                // ジャンプ
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
                // ダッシュ
                if (Input.GetButtonDown("Fire1"))
                {
                    SetRunState();
                }

                // 等速度運動
                var velocity = rigidbody.velocity;
                velocity.x = speed;
                rigidbody.velocity = velocity;
            }
            else
            {
                Debug.Log("Walk状態からの落下");
                Fall();
            }
        }

        // Run状態のフレーム更新処理です。
        private void UpdateForRunState()
        {
            // 接地状態の場合
            if (isGrounded)
            {
                // ダッシュ解除
                if (Input.GetButtonUp("Fire1"))
                {
                    Debug.Log("ダッシュ解除");
                    audioSource.Stop();
                    SetWalkState();
                }

                // 等速度運動
                var velocity = rigidbody.velocity;
                velocity.x = speed;
                rigidbody.velocity = velocity;
            }
            else
            {
                Debug.Log("Run状態からの落下");
                audioSource.Stop();
                Fall();
            }
        }

        // JumpReady状態のフレーム更新処理です。
        void UpdateForJumpReadyState()
        {
            // ジャンプの予備動作後、足が離れた場合
            if (!isGrounded)
            {
                Debug.Log("ジャンプ離陸");
                currentState = PlayerState.Jumping;
            }
        }

        // Jumping状態のフレーム更新処理です。
        void UpdateForJumpingState()
        {
            // ジャンプ状態からの着地判定
            if (isGrounded)
            {
                Debug.Log("ジャンプからの着地");
                animator.SetTrigger(landingId);
                SetWalkState();
            }
        }

        // 固定フレームレートで呼び出される更新処理です。
        void FixedUpdate()
        {
            // 接地判定
            isGrounded = Physics2D.OverlapBox(
                groundChecker.position,
                groundChecker.lossyScale,
                groundChecker.eulerAngles.z,
                groundLayer);
            if (isGrounded)
            {
                // 転倒している場合は接地と認めない
                if (IsTumble())
                {
                    isGrounded = false;
                }
            }

            // 壁との衝突判定
            var result = Physics2D.OverlapBox(
                wallChecker.position,
                wallChecker.lossyScale,
                wallChecker.eulerAngles.z,
                groundLayer);
            // 今回フレームで衝突
            if (result)
            {
                // 転倒時を除いて、壁との距離をとるためにノックバックする
                if (!IsTumble())
                {
                    rigidbody.AddForce(transform.TransformDirection(knockBackPower), ForceMode2D.Force);
                }
            }

            // 転倒によるゲームオーバーを判定
            if (IsTumble())
            {
                // 転倒状態がキープされている時間を計測する
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

        // 転倒状態の場合はtrueを返します。
        bool IsTumble()
        {
            var zAngle = Mathf.Repeat(rigidbody.rotation, 360);
            return zAngle > 60 && zAngle < 300;
        }

        // プレイヤーが他のオブジェクトのトリガーに侵入した際に呼び出されます。
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ゲームオーバー判定
            if (collision.CompareTag("DeadArea"))
            {
                StageScene.Instance.GameOver();
            }
        }
    }
}