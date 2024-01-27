using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // �A�C�e���̑��l������\������UI��\���܂��B
    public class TotalItemUI : MonoBehaviour
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
            var totalItemCount = PlayerPrefs.GetInt("TotalItemCountKey", 0);
            for (int index = 0; index < values.Length; index++)
            {
                values[index].sprite = numbers[totalItemCount % 10];
                totalItemCount /= 10;
            }
        }
    }
}