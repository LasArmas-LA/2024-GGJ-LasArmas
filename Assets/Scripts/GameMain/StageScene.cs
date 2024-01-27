using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // �X�e�[�W��ʂ̐i�s�𐧌䂵�܂��B
    public class StageScene : MonoBehaviour
    {
        // �Q�[���I�[�o�[�\���p��UI���w�肵�܂��B
        [SerializeField]
        private GameOverUI gameOverUI = null;
        // �X�e�[�W�N���A�[�\���p��UI���w�肵�܂��B
        [SerializeField]
        private StageClearUI stageClearUI = null;
        // �|�[�YUI���w�肵�܂��B
        [SerializeField]
        private PauseUI pauseUI = null;

        // �X�e�[�W��ʓ��̐i�s��Ԃ�\���܂��B
        enum SceneState
        {
            // �X�e�[�W�J�n���o��
            Start,
            // �X�e�[�W�v���C��
            Play,
            // �Q�[���I�[�o�[���m�肵�Ă��ĉ��o��
            GameOver,
            // �X�e�[�W�N���A�[���m�肵�Ă��ĉ��o��
            StageClear,
        }
        SceneState gameState = SceneState.Start;

        // �R���|�[�l���g���Q�Ƃ��Ă����ϐ�
        Animator animator;
        // Animator�p�����[�^�[ID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        #region �C���X�^���X�ւ�static�ȃA�N�Z�X�|�C���g
        // �������g�̃C���X�^���X���擾���܂��B
        public static StageScene Instance { get; private set; } = null;

        // Start()������Ɏ��s����܂��B
        private void Awake()
        {
            // �������g���C���X�^���X�Ƃ��ĕۑ�
            Instance = this;
        }
        #endregion

        #region �X�e�[�W�J�n���o
        // Start is called before the first frame update
        void Start()
        {
            // �R���|�[�l���g���Q��
            animator = GetComponent<Animator>();

            gameState = SceneState.Start;

            // �|�[�YUI��������
            pauseUI.OnClickResumeButton.AddListener(Resume);
            pauseUI.OnClickRestartButton.AddListener(Retry);
            pauseUI.OnClickExitButton.AddListener(Exit);
            pauseUI.Hide();
        }

        // �X�e�[�W���J�n���܂��B
        public void PlayGame()
        {
            gameState = SceneState.Play;
        }
        #endregion

        #region �Q�[���I�[�o�[
        // ���̃X�e�[�W���Q�[���I�[�o�[�Ƃ��܂��B
        public void GameOver()
        {
            // �X�e�[�W�v���C���̂�
            if (gameState == SceneState.Play)
            {
                gameState = SceneState.GameOver;
                gameOverUI.Show();
            }
        }
        #endregion

        #region �X�e�[�W�N���A�[
        // ���̃X�e�[�W���X�e�[�W�N���A�[�Ƃ��܂��B
        public void StageClear()
        {
            // �X�e�[�W�v���C���̂�
            if (gameState == SceneState.Play)
            {
                gameState = SceneState.StageClear;

                // ���̃X�e�[�W�ł̃A�C�e���l���������̃V�[���܂ŕۑ�����
                var totalItemCount = PlayerPrefs.GetInt("TotalItemCountKey", 0);
                totalItemCount += ItemCount;
                PlayerPrefs.SetInt("TotalItemCountKey", totalItemCount);

                stageClearUI.Show();
            }
        }
        #endregion

        // Update is called once per frame
        void Update()
        {
            switch (gameState)
            {
                case SceneState.Start:
                    break;
                case SceneState.Play:
                    // �|�[�Y
                    if (Input.GetButtonDown("Cancel"))
                    {
                        if (!IsPaused)
                        {
                            Pause();
                        }
                        else
                        {
                            Resume();
                        }
                    }
                    // �v���C���Ԃ��v������
                    PlayTime -= Time.deltaTime;
                    // �^�C���A�E�g����
                    if (PlayTime < 0)
                    {
                        PlayTime = 0;
                        GameOver();
                    }
                    break;
                case SceneState.GameOver:
                    break;
                case SceneState.StageClear:
                    break;
                default:
                    break;
            }
        }

        #region �|�[�Y
        // �|�[�Y��Ԃ̏ꍇ��true�A�v���C��Ԃ̏ꍇ��false
        public bool IsPaused { get; private set; } = false;

        // �Q�[�����ꎞ��~���܂��B
        public void Pause()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                Time.timeScale = 0;
                pauseUI.Show();
            }
        }

        // �Q�[���̈ꎞ��~���������܂��B
        public void Resume()
        {
            if (IsPaused)
            {
                IsPaused = false;
                Time.timeScale = 1;
                pauseUI.Hide();
            }
        }
        #endregion

        #region �V�[���ǂݍ���
        // �X�e�[�W���I�������ă^�C�g����ʂ�ǂݍ��݂܂��B
        public void Exit()
        {
            LoadScene("Title");
        }

        // �X�e�[�W���I�������ă^�C�g����ʂ�ǂݍ��݂܂��B
        public void Retry()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        // �w�肵���V�[����ǂݍ��݂܂��B
        public void LoadScene(string sceneName)
        {
            // �|�[�Y��Ԃ̏ꍇ�́A�R���[�`�����ŏ�����
            // ����Ȃ��Ȃ邽�߃|�[�Y��������
            if (IsPaused)
            {
                Resume();
            }
            StartCoroutine(OnLoadScene(sceneName));
        }

        IEnumerator OnLoadScene(string sceneName)
        {
            // FadeOut�A�j���[�V�����ɐ؂�ւ���
            animator.SetTrigger(fadeOutId);
            // �A�j���[�V�������I������܂őҋ@
            yield return new WaitForSeconds(1);

            // �V�[�������[�h����
            SceneManager.LoadScene(sceneName);
        }
        #endregion

        #region �A�C�e���l����
        // ���݂̃A�C�e���l�������擾���܂��B
        public int ItemCount { get; private set; }

        // �w�肵�����̃A�C�e�����l�����܂��B
        public void AddItem(int value)
        {
            ItemCount += value;
        }
        #endregion

        #region �v���C����
        // �v���C���Ԃ��擾���܂��B
        public float PlayTime { get => playTime; private set => playTime = value; }
        // ���̃X�e�[�W�̃v���C���Ԃ��w�肵�܂��B
        [SerializeField]
        private float playTime = 30;
        #endregion
    }
}