using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    //移動速度
    [SerializeField]
    float MoveSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //位置の更新
        transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
    }
}
