using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // ステージ画面の進行を制御します。
    public class StageScene : MonoBehaviour
    {
        // ゲームオーバー表示用のUIを指定します。
        [SerializeField]
        private GameOverUI gameOverUI = null;
        // ステージクリアー表示用のUIを指定します。
        [SerializeField]
        private StageClearUI stageClearUI = null;
        // ポーズUIを指定します。
        [SerializeField]
        private PauseUI pauseUI = null;

        // ステージ画面内の進行状態を表します。
        enum SceneState
        {
            // ステージ開始演出中
            Start,
            // ステージプレイ中
            Play,
            // ゲームオーバーが確定していて演出中
            GameOver,
            // ステージクリアーが確定していて演出中
            StageClear,
        }
        SceneState gameState = SceneState.Start;

        // コンポーネントを参照しておく変数
        Animator animator;
        // AnimatorパラメーターID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        #region インスタンスへのstaticなアクセスポイント
        // 自分自身のインスタンスを取得します。
        public static StageScene Instance { get; private set; } = null;

        // Start()よりも先に実行されます。
        private void Awake()
        {
            // 自分自身をインスタンスとして保存
            Instance = this;
        }
        #endregion

        #region ステージ開始演出
        // Start is called before the first frame update
        void Start()
        {
            // コンポーネントを参照
            animator = GetComponent<Animator>();

            gameState = SceneState.Start;

            // ポーズUIを初期化
            pauseUI.OnClickResumeButton.AddListener(Resume);
            pauseUI.OnClickRestartButton.AddListener(Retry);
            pauseUI.OnClickExitButton.AddListener(Exit);
            pauseUI.Hide();
        }

        // ステージを開始します。
        public void PlayGame()
        {
            gameState = SceneState.Play;
        }
        #endregion

        #region ゲームオーバー
        // このステージをゲームオーバーとします。
        public void GameOver()
        {
            // ステージプレイ中のみ
            if (gameState == SceneState.Play)
            {
                gameState = SceneState.GameOver;
                gameOverUI.Show();
            }
        }
        #endregion

        #region ステージクリアー
        // このステージをステージクリアーとします。
        public void StageClear()
        {
            // ステージプレイ中のみ
            if (gameState == SceneState.Play)
            {
                gameState = SceneState.StageClear;

                // このステージでのアイテム獲得数を次のシーンまで保存する
                var totalItemCount = PlayerPrefs.GetInt("TotalItemCountKey", 0);
                totalItemCount += ItemCount;
                PlayerPrefs.SetInt("TotalItemCountKey", totalItemCount);

                stageClearUI.Show();
            }
        }
        #endregion

        // Update is called once per frame
        void Update()
        {
            switch (gameState)
            {
                case SceneState.Start:
                    break;
                case SceneState.Play:
                    // ポーズ
                    if (Input.GetButtonDown("Cancel"))
                    {
                        if (!IsPaused)
                        {
                            Pause();
                        }
                        else
                        {
                            Resume();
                        }
                    }
                    // プレイ時間を計測する
                    PlayTime -= Time.deltaTime;
                    // タイムアウト判定
                    if (PlayTime < 0)
                    {
                        PlayTime = 0;
                        GameOver();
                    }
                    break;
                case SceneState.GameOver:
                    break;
                case SceneState.StageClear:
                    break;
                default:
                    break;
            }
        }

        #region ポーズ
        // ポーズ状態の場合はtrue、プレイ状態の場合はfalse
        public bool IsPaused { get; private set; } = false;

        // ゲームを一時停止します。
        public void Pause()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                Time.timeScale = 0;
                pauseUI.Show();
            }
        }

        // ゲームの一時停止を解除します。
        public void Resume()
        {
            if (IsPaused)
            {
                IsPaused = false;
                Time.timeScale = 1;
                pauseUI.Hide();
            }
        }
        #endregion

        #region シーン読み込み
        // ステージを終了させてタイトル画面を読み込みます。
        public void Exit()
        {
            LoadScene("Title");
        }

        // ステージを終了させてタイトル画面を読み込みます。
        public void Retry()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        // 指定したシーンを読み込みます。
        public void LoadScene(string sceneName)
        {
            // ポーズ状態の場合は、コルーチン内で処理が
            // 流れなくなるためポーズ解除する
            if (IsPaused)
            {
                Resume();
            }
            StartCoroutine(OnLoadScene(sceneName));
        }

        IEnumerator OnLoadScene(string sceneName)
        {
            // FadeOutアニメーションに切り替える
            animator.SetTrigger(fadeOutId);
            // アニメーションが終了するまで待機
            yield return new WaitForSeconds(1);

            // シーンをロードする
            SceneManager.LoadScene(sceneName);
        }
        #endregion

        #region アイテム獲得数
        // 現在のアイテム獲得数を取得します。
        public int ItemCount { get; private set; }

        // 指定した個数のアイテムを獲得します。
        public void AddItem(int value)
        {
            ItemCount += value;
        }
        #endregion

        #region プレイ時間
        // プレイ時間を取得します。
        public float PlayTime { get => playTime; private set => playTime = value; }
        // このステージのプレイ時間を指定します。
        [SerializeField]
        private float playTime = 30;
        #endregion
    }
}