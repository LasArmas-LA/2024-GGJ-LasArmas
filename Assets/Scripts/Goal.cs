using UnityEngine;
using UnityEngine.SceneManagement;

// �X�e�[�W�N���A�[����̂��߂̃S�[����\���܂��B
public class Goal : MonoBehaviour
{
    // �g���K�[���ɐN�������ۂɌĂяo����܂��B
     void OnTriggerEnter2D(Collider2D collision)
    {
        // �X�e�[�W�N���A�[����
        if (collision.CompareTag("Player"))
        {
            // �Q�[���N���A���̏���
            Debug.Log("Game Cleared!");

            // �N���A�V�[���Ɉڍs
            SceneManager.LoadScene("GameClear");
        }
    }
}