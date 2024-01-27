using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    //�ړ����x
    [SerializeField]
    float MoveSpeed = 20.0f;

    //�v���C���[Prefab,�I�u�W�F�N�g�̔z�u
    [SerializeField]
    [Tooltip("�v���C���[Prefab�A�I�u�W�F�N�g��z�u���Ă�������")]
    PlayerHealthUI pHealth;

    //�_���[�W�̏���
    [SerializeField]
    float damage;

    // Update is called once per frame
    void Update()
    {
        //�ʒu�̍X�V
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
