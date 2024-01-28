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
        // FadeOutアニメーションが終了するまで1秒待機
        yield return new WaitForSeconds(2);
        // 次のシーンを読み込む
        SceneManager.LoadScene("NFG Test Macho");
    }
}