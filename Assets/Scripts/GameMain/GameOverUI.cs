using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // �Q�[���I�[�o�[UI��\���܂��B
    public class GameOverUI : MonoBehaviour
    {
        // �����I���I�u�W�F�N�g���w�肵�܂��B
        [SerializeField]
        private Selectable firstSelected = null;

        // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
        Animator animator;
        // Animator�p�����[�^�[ID
        static readonly int showId = Animator.StringToHash("Show");

        void Start()
        {
            // �R���|�[�l���g���Q�Ƃ��Ă���
            animator = GetComponent<Animator>();
        }

        // ����UI��\�����܂��B
        public void Show()
        {
            animator.SetBool(showId, true);
            StartCoroutine(OnShow());
        }

        IEnumerator OnShow()
        {
            // 2�b�ԑҋ@
            yield return new WaitForSeconds(2);
            // YES�{�^����I����Ԃɐݒ肷��
            firstSelected.Select();
        }
    }
}