using UnityEngine;

public class FallDown : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;
    public BoxCollider2D boxCollider2D;
    public string tag;

    // ���������n�߂�܂ł̃J�E���g
    private int fallCount;

    // ����PlayerTag�ɐG�ꂽ���ǂ���
    private bool floor_touch;

    // ����������
    void Start()
    {
        rigidbody2D.isKinematic = true;
        boxCollider2D.isTrigger = true;
    }

    // �X�V����
    void Update()
    {
        if (floor_touch)
        {
            fallCount++;
            if (fallCount >= 10)
            {
                // ���𗎂Ƃ�
                rigidbody2D.isKinematic = false;
                rigidbody2D.velocity = new Vector2(0, -10);
            }
        }
    }

    // ����PlayerTag�ɐG�ꂽ�Ƃ��̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == )
        {
            floor_touch = true;
        }
    }
}