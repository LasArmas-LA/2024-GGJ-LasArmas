using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("�U�����x")]
    public float attackSpeed;

    [Header("�U���͈�")]
    public float attackRange;

    [Header("�U���_���[�W")]
    public int attackDamage;

    [Header("�{�X�̃w���X")]
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
                    // �v���C���[���ˑR�Ƃ��� BoxCollider �̒��ɂ���ꍇ
                    // �T�E���h��ύX����
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
                // �N���A���̏���������
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
                    // �_�ł��Ă��΂炭���G�ɂȂ�Ƃ�
                    //�i�_�ł̂����̓v���C���[�̃X�N���v�g�Q�Ɓj
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
        // �U���A�j���[�V�������I��������A�U���\�ȏ�Ԃɂ���
        // (IsAttacking �̐錾���Ȃ����߁A�֘A���镔����K�v�ɉ����Ēǉ����邩�폜����)
    }
}
