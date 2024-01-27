using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RunGame
{
    // �|�[�YUI�̐i�s������Ǘ����܂��B
    public class PauseUI : MonoBehaviour
    {
        // ResumeButton�̌���C�x���g���擾�܂��͐ݒ肵�܂��B
        public UnityEvent OnClickResumeButton
        {
            get { return onClickResumeButton; }
            set { onClickResumeButton = value; }
        }
        // ResumeButton�̌���C�x���g�ł��B
        [SerializeField]
        private UnityEvent onClickResumeButton = null;

        // RestartButton�̌���C�x���g���擾�܂��͐ݒ肵�܂��B
        public UnityEvent OnClickRestartButton
        {
            get { return onClickRestartButton; }
            set { onClickRestartButton = value; }
        }
        // RestartButton�̌���C�x���g�ł��B
        [SerializeField]
        private UnityEvent onClickRestartButton = null;

        // ExitButton�̌���C�x���g���擾�܂��͐ݒ肵�܂��B
        public UnityEvent OnClickExitButton
        {
            get { return onClickExitButton; }
            set { onClickExitButton = value; }
        }
        // ExitButton�̌���C�x���g�ł��B
        [SerializeField]
        private UnityEvent onClickExitButton = null;

        // ResumeButton���w�肵�܂��B
        [SerializeField]
        private Button resumeButton = null;
        // RestartButton���w�肵�܂��B
        [SerializeField]
        private Button restartButton = null;
        // ExitButton���w�肵�܂��B
        [SerializeField]
        private Button exitButton = null;

        // �����I����Ԃɐݒ肷��{�^�����w�肵�܂��B
        [SerializeField]
        private Selectable selectedButton = null;

        Image backgroundImage = null;

        void Awake()
        {
            // ���̃I�u�W�F�N�g��Start()��Hide()��Show()���Ăяo�����
            // �\�������邽�߁AAwake()�ŏ������͊���������K�v������B
            backgroundImage = GetComponent<Image>();
            resumeButton.onClick.AddListener(() => { onClickResumeButton.Invoke(); });
            restartButton.onClick.AddListener(() => { onClickRestartButton.Invoke(); });
            exitButton.onClick.AddListener(() => { onClickExitButton.Invoke(); });
        }

        // ����UI���\���ɐݒ肵�܂��B
        public void Hide()
        {
            backgroundImage.enabled = false;
            // �q�I�u�W�F�N�g�����ׂĔ�A�N�e�B�u��
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        // ����UI��\�����܂��B
        public void Show()
        {
            backgroundImage.enabled = true;
            // �q�I�u�W�F�N�g�����ׂăA�N�e�B�u��
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            selectedButton.Select();
        }
    }
}