
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    //���݂�HP
    [Tooltip("�v���C���[��HP�w��")]
    public float health;

    //�ő�HP(���l�L���͊�{����)
    [Tooltip("���l�̋L���͊�{�I�ɂ��Ȃ��ł�������")]
    public float maxHealth;

    //�w���X�o�[�̎w��
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(maxHealth / health, 0, 1);    
    }
}
