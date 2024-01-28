using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �V���O���g���p�^�[�����g�p����GameManager��݌v
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameClear()
    {
        // �Q�[���N���A���̏���
        Debug.Log("Game Cleared!");

        // �N���A�V�[���Ɉڍs
        SceneManager.LoadScene("ClearScene");
    }
}
