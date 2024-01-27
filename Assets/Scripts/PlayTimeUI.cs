using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // �v���C���Ԃ�\������UI��\���܂��B
    public class PlayTimeUI : MonoBehaviour
    {
        // ���l�\���p�̃X�v���C�g�摜���w�肵�܂��B
        [SerializeField]
        private Sprite[] numbers = new Sprite[10];
        // �l��\������Image���w�肵�܂��B
        [SerializeField]
        private Image[] values = null;

        // Start is called before the first frame update
        void Start()
        {
            UpdateValue();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateValue();
        }

        // UI��\���X�V���܂��B
        private void UpdateValue()
        {
            // 100����1�b�P�ʂɕϊ�
            var playTime = (int)(StageScene.Instance.PlayTime * 100);
            for (int index = 0; index < values.Length; index++)
            {
                values[index].sprite = numbers[playTime % 10];
                playTime /= 10;
            }
        }
    }
}