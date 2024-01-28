using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // �w�Q�[���N���A�\�x��ʂ̃V�[���̐i�s�𐧌䂵�܂��B
    public class GameClearScene : MonoBehaviour
    {
        // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
        Animator animator;
        // Animator�p�����[�^�[ID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        void Start()
        {
            // �R���|�[�l���g�����O�Ɏ擾
            animator = GetComponent<Animator>();

            StartCoroutine(OnStart());
        }

        // �Q�b�ҋ@��Ɏ��̃V�[����Ǎ��݉\�ɂ��܂��B
        IEnumerator OnStart()
        {
            yield return new WaitForSeconds(2);

            StartCoroutine(OnLoadNextScene());
        }

        IEnumerator OnLoadNextScene()
        {
            // �t�F�[�h�A�E�g�̃A�j���[�V�����֐؂�ւ���
            animator.SetTrigger(fadeOutId);
            // FadeOut�A�j���[�V�������I������܂�1�b�ҋ@
            yield return new WaitForSeconds(1);
            // ���̃V�[����ǂݍ���
            SceneManager.LoadScene("Title");
        }
    }
}