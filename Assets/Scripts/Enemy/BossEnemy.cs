using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    /// <summary>
    /// ������Ƀv���C���[������
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private string playerTag = "Player";

    //�{�X���o
    private BossState nowState = BossState.StartEnsyutu;

    private enum BossState  //���uBossState�v�̕�����enum�̖��O�i�����Œ�`����j
    {
        OnTriggerPlayer,
        StartEnsyutu,  //���D���Ȃ悤�ɖ��O�����邱�Ƃ��ł���
        Battle,
        ClearEnsyutu,
    }

    // Update is called once per frame
    void Update()
    {
        switch (nowState)  //��nowState�ɂ͌��݂̏�Ԃ������Ă���
        {
            case BossState.StartEnsyutu:
                //�o�ꉉ�o���̏���������
                if (isOn == true)
                {  
                    


                    nowState = BossState.Battle;
                }
                break;
            case BossState.Battle:
                //�퓬���̏���������



                nowState = BossState.ClearEnsyutu;
                break;
            case BossState.ClearEnsyutu:


                break;

        }
    }
}
