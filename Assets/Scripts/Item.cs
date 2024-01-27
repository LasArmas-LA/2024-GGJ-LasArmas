using UnityEngine;

namespace RunGame
{
    // プレイヤーが取得できるアイテムを表します。
    public class Item : MonoBehaviour
    {
        // トリガー内に他のオブジェクトが侵入してきた際に呼び出されます。
        void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("ヒット");

            // Project Settings->Physics 2Dにてプレイヤーとアイテムのみが
            // 判定されるように設定されている。
            // このため侵入してきたオブジェクトについて、プレイヤーであるかを
            // 確認しなくても問題ない。
            StageScene.Instance.AddItem(1);
            Destroy(gameObject);
        }
    }
}