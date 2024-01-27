using UnityEngine;

namespace RunGame
{
    // �Ώۂ�ǔ�����J�����̋@�\��񋟂��܂��B
    public class ChaseCamera : MonoBehaviour
    {
        // �ǔ�����Ώۂ��w�肵�܂��B
        [SerializeField]
        private Transform target = null;
        // �ǔ��Ώۂ���̃I�t�Z�b�g�l���w�肵�܂��B
        [SerializeField]
        private Vector2 offset = new Vector2(6.5f, 1.5f);

        // Start is called before the first frame update
        void Start()
        {
            var position = transform.position;
            position.x = target.position.x + offset.x;
            position.y = target.position.y + offset.y;
            transform.position = position;
        }

        // Update is called once per frame
        void Update()
        {
            // �ǔ��Ώۂ�x���W�ŃJ�����̍��W���X�V
            var position = transform.position;
            position.x = target.position.x + offset.x;
            // y�������ɒǔ����Ȃ��ꍇ�͎��̍s���R�����g�A�E�g
            position.y = target.position.y + offset.y;
            transform.position = position;
        }
    }
}