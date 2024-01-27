
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    //現在のHP
    [Tooltip("プレイヤーのHP指定")]
    public float health;

    //最大HP(数値記入は基本無し)
    [Tooltip("数値の記入は基本的にしないでください")]
    public float maxHealth;

    //ヘルスバーの指定
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(maxHealth / health, 0, 1);    
    }
}
