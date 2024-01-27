using UnityEngine;
using UnityEngine.Events;

namespace RunGame
{
    // ステージ開始後のカウントダウン演出の進行制御を管理します。
    public class CountDownUI : MonoBehaviour
    {
        // GO!表示の瞬間に実行してほしい処理を登録します。
        public UnityEvent onPlayGame;

        // Startアニメーション内のイベントから呼び出されます。
        public void OnPlayGameEvent()
        {
            onPlayGame.Invoke();
        }
    }
}