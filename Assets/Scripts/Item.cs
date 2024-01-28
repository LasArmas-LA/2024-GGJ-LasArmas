using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // アイテムを取得したらゲームクリアの処理を開始
            GameManager.Instance.GameClear();
        }
    }
}
