using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("攻撃速度")]
    public float attackSpeed;

    [Header("攻撃範囲")]
    public float attackRange;

    [Header("攻撃ダメージ")]
    public int attackDamage;

    [Header("ボスのヘルス")]
    public int HitPoint;

    AudioSource audioSource;
    private BossState nowState = BossState.StartEnsyutu;

    private float bossRange = 10.0f;

    private GameObject macho;

    private GameObject itemPrefab;

    private bool playerEnteredBoxCollider = false;

    public enum BossState
    {
        StartEnsyutu,
        Battle,
        ClearEnsyutu,
    }

    void Start()
    {
        macho = GameObject.FindGameObjectWithTag("Player");
        itemPrefab = Resources.Load("Prefabs/Item") as GameObject;
        audioSource = GetComponent<AudioSource>();
    }

    bool IsHitByMissile()
    {
        GameObject missile = GameObject.FindGameObjectWithTag("Missile");

        if (missile != null)
        {
            float distance = Vector3.Distance(missile.transform.position, transform.position);
            return distance < missile.GetComponent<SphereCollider>().radius + GetComponent<SphereCollider>().radius;
        }

        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredBoxCollider = true;
            if (nowState != BossState.Battle)
            {
                nowState = BossState.StartEnsyutu;
                if (audioSource != null)
                    audioSource.PlayOneShot(Resources.Load<AudioClip>("BossStart"));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredBoxCollider = false;
        }
    }

    void Update()
    {
        switch (nowState)
        {
            case BossState.StartEnsyutu:
                if (playerEnteredBoxCollider)
                {
                    // プレイヤーが依然として BoxCollider の中にいる場合
                    // サウンドを変更する
                    if (audioSource != null)
                        audioSource.PlayOneShot(Resources.Load<AudioClip>("BossStart"));
                }
                break;
            case BossState.Battle:
                if (HitPoint <= 0)
                {
                    GameObject item = Instantiate(itemPrefab, transform.position, transform.rotation);
                    nowState = BossState.ClearEnsyutu;
                    if (audioSource != null)
                        audioSource.PlayOneShot(Resources.Load<AudioClip>("BossClear"));
                }
                break;
            case BossState.ClearEnsyutu:
                // クリア時の処理を書く
                break;
        }

        if (HitPoint > 0)
        {
            if (IsHitByMissile())
            {
                HitPoint -= 10;
                if (HitPoint <= 0)
                {
                    nowState = BossState.ClearEnsyutu;
                }
                else
                {
                    // 点滅してしばらく無敵になるとか
                    //（点滅のやり方はプレイヤーのスクリプト参照）
                }
            }
        }
    }

    void OnAttack()
    {
        if (macho != null && Vector3.Distance(transform.position, macho.transform.position) < attackRange)
        {
            macho.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }

    void OnAnimationEnd()
    {
        // 攻撃アニメーションが終了したら、攻撃可能な状態にする
        // (IsAttacking の宣言がないため、関連する部分を必要に応じて追加するか削除する)
    }
}
