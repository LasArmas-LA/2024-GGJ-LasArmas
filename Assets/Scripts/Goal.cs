using UnityEngine;

namespace RunGame
{
    // �X�e�[�W�N���A�[����̂��߂̃S�[����\���܂��B
    public class Goal : MonoBehaviour
    {
        // �g���K�[���ɐN�������ۂɌĂяo����܂��B
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �X�e�[�W�N���A�[����
            if (collision.CompareTag("Player"))
            {
                StageScene.Instance.StageClear();
            }
        }
    }
}