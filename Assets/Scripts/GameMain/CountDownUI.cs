using UnityEngine;
using UnityEngine.Events;

namespace RunGame
{
    // �X�e�[�W�J�n��̃J�E���g�_�E�����o�̐i�s������Ǘ����܂��B
    public class CountDownUI : MonoBehaviour
    {
        // GO!�\���̏u�ԂɎ��s���Ăق���������o�^���܂��B
        public UnityEvent onPlayGame;

        // Start�A�j���[�V�������̃C�x���g����Ăяo����܂��B
        public void OnPlayGameEvent()
        {
            onPlayGame.Invoke();
        }
    }
}