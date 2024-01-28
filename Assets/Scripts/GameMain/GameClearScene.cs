using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // 『ゲームクリア―』画面のシーンの進行を制御します。
    public class GameClearScene : MonoBehaviour
    {
        // コンポーネントを事前に参照しておく変数
        Animator animator;
        // AnimatorパラメーターID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        void Start()
        {
            // コンポーネントを事前に取得
            animator = GetComponent<Animator>();

            StartCoroutine(OnStart());
        }

        // ２秒待機後に次のシーンを読込み可能にします。
        IEnumerator OnStart()
        {
            yield return new WaitForSeconds(2);

            StartCoroutine(OnLoadNextScene());
        }

        IEnumerator OnLoadNextScene()
        {
            // フェードアウトのアニメーションへ切り替える
            animator.SetTrigger(fadeOutId);
            // FadeOutアニメーションが終了するまで1秒待機
            yield return new WaitForSeconds(1);
            // 次のシーンを読み込む
            SceneManager.LoadScene("Title");
        }
    }
}