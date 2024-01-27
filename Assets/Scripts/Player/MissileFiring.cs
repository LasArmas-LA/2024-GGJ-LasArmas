using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissileFiring : MonoBehaviour
{
    //発射地点の移動速度
    [SerializeField]
    [Tooltip("弾の弾速")]
    float fMoveSpeed = 5.0f;

    //ミサイルのオブジェクト
    [SerializeField]
    GameObject MuscleMissile;

    //プレイヤーのオブジェクト
    [SerializeField]
    GameObject playerObj;

    //弾の位置
    Vector3 bulletPoint;

   void Start()
    {
        bulletPoint = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //ボタンを押されたら
        if(Input.GetMouseButtonDown(1))
        {
            //弾の生成 
            GameObject missileInstance = Instantiate(MuscleMissile, transform.position + bulletPoint, Quaternion.identity);

            // DestroyMissile コルーチンに生成されたミサイルのインスタンスを渡す
            StartCoroutine(DestroyMissile(missileInstance));
        }
    }

    //public void Fire(InputAction.CallbackContext context)
    //{
    //    //弾の生成 
    //    GameObject missileInstance = Instantiate(MuscleMissile, transform.position + bulletPoint, Quaternion.identity);

    //    // DestroyMissile コルーチンに生成されたミサイルのインスタンスを渡す
    //    StartCoroutine(DestroyMissile(missileInstance));

    //}

    IEnumerator DestroyMissile(GameObject missileInstance)
    {
        //3秒停止
        yield return new WaitForSeconds(1);

  // 生成されたミサイルのインスタンスを破棄
        DestroyImmediate(missileInstance, true);    }

}
