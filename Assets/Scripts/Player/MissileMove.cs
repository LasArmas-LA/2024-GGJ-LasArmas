using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    //移動速度
    [SerializeField]
    float MoveSpeed = 20.0f;

    //プレイヤーPrefab,オブジェクトの配置
    [SerializeField]
    [Tooltip("プレイヤーPrefab、オブジェクトを配置してください")]
    PlayerHealthUI pHealth;

    //ダメージの処理
    [SerializeField]
    float damage;

    // Update is called once per frame
    void Update()
    {
        //位置の更新
        transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthUI>().health -= damage;
        }
    }
}
