using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // 『タイトル』画面のシーンの進行を制御します。
    public class TitleScene : MonoBehaviour
    {
        // 次のシーンを読み込み可能な場合はtrue、それ以外はfalse
        bool isLoadable = false;

        // コンポーネントを事前に参照しておく変数
        Animator animator;
        // AnimatorパラメーターID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        // Start is called before the first frame update
        void Start()
        {
            // コンポーネントを事前に取得
            animator = GetComponent<Animator>();

            // ステージ途中でゲームを終了させた場合に対応するため、
            // アイテムの総獲得数をクリアーする
            PlayerPrefs.DeleteKey("TotalItemCountKey");

            StartCoroutine(OnStart());
        }

        // ２秒待機後に次のシーンを読込み可能にします。
        IEnumerator OnStart()
        {
            yield return new WaitForSeconds(2);
            isLoadable = true;
        }

        // 決定操作の際に呼び出され、次のシーンを読み込みます。
        private void LoadNextScene()
        {
            if (isLoadable)
            {
                StartCoroutine(OnLoadNextScene());
            }
        }

        IEnumerator OnLoadNextScene()
        {
            // フェードアウトのアニメーションへ切り替える
            animator.SetTrigger(fadeOutId);
            // FadeOutアニメーションが終了するまで1秒待機
            yield return new WaitForSeconds(1);
            // 次のシーンを読み込む
            SceneManager.LoadScene("NFG Test Macho");
        }


        // ゲームを終了します。
        public void ExitGame()
        {
            StartCoroutine(OnExitGame());
        }

        IEnumerator OnExitGame()
        {
            animator.SetTrigger(fadeOutId);
            yield return new WaitForSeconds(1);
            Application.Quit();
        }
    }
}