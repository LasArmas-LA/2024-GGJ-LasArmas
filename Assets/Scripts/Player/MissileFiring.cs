using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissileFiring : MonoBehaviour
{
    //���˒n�_�̈ړ����x
    [SerializeField]
    [Tooltip("�e�̒e��")]
    float fMoveSpeed = 5.0f;

    //�~�T�C���̃I�u�W�F�N�g
    [SerializeField]
    GameObject MuscleMissile;

    //�v���C���[�̃I�u�W�F�N�g
    [SerializeField]
    GameObject playerObj;

    //�e�̈ʒu
    Vector3 bulletPoint;

   void Start()
    {
        bulletPoint = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //�{�^���������ꂽ��
        if(Input.GetMouseButtonDown(1))
        {
            //�e�̐��� 
            GameObject missileInstance = Instantiate(MuscleMissile, transform.position + bulletPoint, Quaternion.identity);

            // DestroyMissile �R���[�`���ɐ������ꂽ�~�T�C���̃C���X�^���X��n��
            StartCoroutine(DestroyMissile(missileInstance));
        }
    }

    //public void Fire(InputAction.CallbackContext context)
    //{
    //    //�e�̐��� 
    //    GameObject missileInstance = Instantiate(MuscleMissile, transform.position + bulletPoint, Quaternion.identity);

    //    // DestroyMissile �R���[�`���ɐ������ꂽ�~�T�C���̃C���X�^���X��n��
    //    StartCoroutine(DestroyMissile(missileInstance));

    //}

    IEnumerator DestroyMissile(GameObject missileInstance)
    {
        //3�b��~
        yield return new WaitForSeconds(1);

  // �������ꂽ�~�T�C���̃C���X�^���X��j��
        DestroyImmediate(missileInstance, true);    }

}
