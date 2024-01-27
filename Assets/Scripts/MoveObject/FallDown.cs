using UnityEngine;

public class FallDown : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;
    public BoxCollider2D boxCollider2D;
    public string tag;

    // 床が落ち始めるまでのカウント
    private int fallCount;

    // 床がPlayerTagに触れたかどうか
    private bool floor_touch;

    // 初期化処理
    void Start()
    {
        rigidbody2D.isKinematic = true;
        boxCollider2D.isTrigger = true;
    }

    // 更新処理
    void Update()
    {
        if (floor_touch)
        {
            fallCount++;
            if (fallCount >= 10)
            {
                // 床を落とす
                rigidbody2D.isKinematic = false;
                rigidbody2D.velocity = new Vector2(0, -10);
            }
        }
    }

    // 床がPlayerTagに触れたときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == )
        {
            floor_touch = true;
        }
    }
}