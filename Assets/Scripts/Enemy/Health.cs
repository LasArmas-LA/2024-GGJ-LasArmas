// 仮の Health クラスの例
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // プレイヤーが死んだ時の処理
            // 例えば、プレイヤーのアニメーションを変更したり、ゲームオーバー画面を表示したりする処理をここに追加する
        }
    }
}