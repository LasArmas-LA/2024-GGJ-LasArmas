using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    /// <summary>
    /// 判定内にプレイヤーがいる
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private string playerTag = "Player";

    //ボス演出
    private BossState nowState = BossState.StartEnsyutu;

    private enum BossState  //←「BossState」の部分はenumの名前（自分で定義する）
    {
        OnTriggerPlayer,
        StartEnsyutu,  //←好きなように名前をつけることができる
        Battle,
        ClearEnsyutu,
    }

    // Update is called once per frame
    void Update()
    {
        switch (nowState)  //←nowStateには現在の状態が入っている
        {
            case BossState.StartEnsyutu:
                //登場演出時の処理を書く
                if (isOn == true)
                {  
                    


                    nowState = BossState.Battle;
                }
                break;
            case BossState.Battle:
                //戦闘中の処理を書く



                nowState = BossState.ClearEnsyutu;
                break;
            case BossState.ClearEnsyutu:


                break;

        }
    }
}
