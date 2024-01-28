using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // �w�^�C�g���x��ʂ̃V�[���̐i�s�𐧌䂵�܂��B
    public class TitleScene : MonoBehaviour
    {
        // ���̃V�[����ǂݍ��݉\�ȏꍇ��true�A����ȊO��false
        bool isLoadable = false;

        // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
        Animator animator;
        // Animator�p�����[�^�[ID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        // Start is called before the first frame update
        void Start()
        {
            // �R���|�[�l���g�����O�Ɏ擾
            animator = GetComponent<Animator>();

            // �X�e�[�W�r���ŃQ�[�����I���������ꍇ�ɑΉ����邽�߁A
            // �A�C�e���̑��l�������N���A�[����
            PlayerPrefs.DeleteKey("TotalItemCountKey");

            StartCoroutine(OnStart());
        }

        // �Q�b�ҋ@��Ɏ��̃V�[����Ǎ��݉\�ɂ��܂��B
        IEnumerator OnStart()
        {
            yield return new WaitForSeconds(2);
            isLoadable = true;
        }

        // ���葀��̍ۂɌĂяo����A���̃V�[����ǂݍ��݂܂��B
        private void LoadNextScene()
        {
            if (isLoadable)
            {
                StartCoroutine(OnLoadNextScene());
            }
        }

        IEnumerator OnLoadNextScene()
        {
            // �t�F�[�h�A�E�g�̃A�j���[�V�����֐؂�ւ���
            animator.SetTrigger(fadeOutId);
            // FadeOut�A�j���[�V�������I������܂�1�b�ҋ@
            yield return new WaitForSeconds(1);
            // ���̃V�[����ǂݍ���
            SceneManager.LoadScene("NFG Test Macho");
        }


        // �Q�[�����I�����܂��B
        public void ExitGame()
        {
            StartCoroutine(OnExitGame());
        }

        IEnumerator OnExitGame()
        {
            animator.SetTrigger(fadeOutId);
            yield return new WaitForSeconds(1);
            Application.Quit();
        }
    }
}