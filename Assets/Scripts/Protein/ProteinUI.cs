using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // �A�C�e���l������\������UI��\���܂��B
    public class ProteinUI : MonoBehaviour
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
            var itemCount = StageScene.Instance.ItemCount;
            for (int index = 0; index < values.Length; index++)
            {
                values[index].sprite = numbers[itemCount % 10];
                itemCount /= 10;
            }
        }
    }
}