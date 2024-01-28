using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // シングルトンパターンを使用してGameManagerを設計
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameClear()
    {
        // ゲームクリア時の処理
        Debug.Log("Game Cleared!");

        // クリアシーンに移行
        SceneManager.LoadScene("ClearScene");
    }
}
