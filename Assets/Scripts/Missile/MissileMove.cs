using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    //�ړ����x
    [SerializeField]
    float MoveSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�ʒu�̍X�V
        transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
    }
}
