using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // ゲームオーバーUIを表します。
    public class GameOverUI : MonoBehaviour
    {
        // 初期選択オブジェクトを指定します。
        [SerializeField]
        private Selectable firstSelected = null;

        // コンポーネントを事前に参照しておく変数
        Animator animator;
        // AnimatorパラメーターID
        static readonly int showId = Animator.StringToHash("Show");

        void Start()
        {
            // コンポーネントを参照しておく
            animator = GetComponent<Animator>();
        }

        // このUIを表示します。
        public void Show()
        {
            animator.SetBool(showId, true);
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
}