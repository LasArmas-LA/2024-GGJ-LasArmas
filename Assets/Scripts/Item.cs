using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �A�C�e�����擾������Q�[���N���A�̏������J�n
            GameManager.Instance.GameClear();
        }
    }
}
