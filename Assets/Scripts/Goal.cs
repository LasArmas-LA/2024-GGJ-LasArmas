using UnityEngine;

namespace RunGame
{
    // ステージクリアー判定のためのゴールを表します。
    public class Goal : MonoBehaviour
    {
        // トリガー内に侵入した際に呼び出されます。
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ステージクリアー判定
            if (collision.CompareTag("Player"))
            {
                StageScene.Instance.StageClear();
            }
        }
    }
}