using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RunGame
{
    // ポーズUIの進行制御を管理します。
    public class PauseUI : MonoBehaviour
    {
        // ResumeButtonの決定イベントを取得または設定します。
        public UnityEvent OnClickResumeButton
        {
            get { return onClickResumeButton; }
            set { onClickResumeButton = value; }
        }
        // ResumeButtonの決定イベントです。
        [SerializeField]
        private UnityEvent onClickResumeButton = null;

        // RestartButtonの決定イベントを取得または設定します。
        public UnityEvent OnClickRestartButton
        {
            get { return onClickRestartButton; }
            set { onClickRestartButton = value; }
        }
        // RestartButtonの決定イベントです。
        [SerializeField]
        private UnityEvent onClickRestartButton = null;

        // ExitButtonの決定イベントを取得または設定します。
        public UnityEvent OnClickExitButton
        {
            get { return onClickExitButton; }
            set { onClickExitButton = value; }
        }
        // ExitButtonの決定イベントです。
        [SerializeField]
        private UnityEvent onClickExitButton = null;

        // ResumeButtonを指定します。
        [SerializeField]
        private Button resumeButton = null;
        // RestartButtonを指定します。
        [SerializeField]
        private Button restartButton = null;
        // ExitButtonを指定します。
        [SerializeField]
        private Button exitButton = null;

        // 初期選択状態に設定するボタンを指定します。
        [SerializeField]
        private Selectable selectedButton = null;

        Image backgroundImage = null;

        void Awake()
        {
            // 他のオブジェクトのStart()でHide()やShow()が呼び出される
            // 可能性があるため、Awake()で初期化は完了させる必要がある。
            backgroundImage = GetComponent<Image>();
            resumeButton.onClick.AddListener(() => { onClickResumeButton.Invoke(); });
            restartButton.onClick.AddListener(() => { onClickRestartButton.Invoke(); });
            exitButton.onClick.AddListener(() => { onClickExitButton.Invoke(); });
        }

        // このUIを非表示に設定します。
        public void Hide()
        {
            backgroundImage.enabled = false;
            // 子オブジェクトをすべて非アクティブ化
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        // このUIを表示します。
        public void Show()
        {
            backgroundImage.enabled = true;
            // 子オブジェクトをすべてアクティブ化
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            selectedButton.Select();
        }
    }
}