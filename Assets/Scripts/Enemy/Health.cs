// ���� Health �N���X�̗�
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // �v���C���[�����񂾎��̏���
            // �Ⴆ�΁A�v���C���[�̃A�j���[�V������ύX������A�Q�[���I�[�o�[��ʂ�\�������肷�鏈���������ɒǉ�����
        }
    }
}