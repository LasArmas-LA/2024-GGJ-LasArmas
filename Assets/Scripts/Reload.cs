using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(OnLoadNextScene());
    }

    IEnumerator OnLoadNextScene()
    {
        // FadeOut�A�j���[�V�������I������܂�1�b�ҋ@
        yield return new WaitForSeconds(2);
        // ���̃V�[����ǂݍ���
        SceneManager.LoadScene("NFG Test Macho");
    }
}