using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("生成するオブジェクト")]
    public GameObject fallingObjectPrefab;

    [Header("生成間隔（秒）")]
    public float spawnInterval = 1f;

    private float timer;

    void Update()
    {
        // タイマーを進める
        timer += Time.deltaTime;

        // spawnIntervalが経過した場合のみオブジェクトを生成
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;  // タイマーをリセット
        }
    }

    void SpawnObject()
    {
        // fallingObjectPrefab を使用し、transform.position で生成
        GameObject obj = Instantiate(fallingObjectPrefab, transform.position, Quaternion.identity);

        // FallingObjectコンポーネントを取得
        FallingObject fObj = obj.GetComponent<FallingObject>();

        // TextMeshProコンポーネントを子オブジェクトから取得
        fObj.scoreText = obj.GetComponentInChildren<TextMeshPro>();
    }
}
