using UnityEngine;
using UnityEngine.SceneManagement;

// ステージクリアー判定のためのゴールを表します。
public class Goal : MonoBehaviour
{
    // トリガー内に侵入した際に呼び出されます。
     void OnTriggerEnter2D(Collider2D collision)
    {
        // ステージクリアー判定
        if (collision.CompareTag("Player"))
        {
            // ゲームクリア時の処理
            Debug.Log("Game Cleared!");

            // クリアシーンに移行
            SceneManager.LoadScene("GameClear");
        }
    }
}