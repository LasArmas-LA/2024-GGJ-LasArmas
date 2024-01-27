using UnityEngine;

namespace RunGame
{
    // 対象を追尾するカメラの機能を提供します。
    public class ChaseCamera : MonoBehaviour
    {
        // 追尾する対象を指定します。
        [SerializeField]
        private Transform target = null;
        // 追尾対象からのオフセット値を指定します。
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
            // 追尾対象のx座標でカメラの座標を更新
            var position = transform.position;
            position.x = target.position.x + offset.x;
            // y軸方向に追尾しない場合は次の行をコメントアウト
            position.y = target.position.y + offset.y;
            transform.position = position;
        }
    }
}