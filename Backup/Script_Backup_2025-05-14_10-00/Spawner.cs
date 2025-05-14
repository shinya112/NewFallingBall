using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("��������I�u�W�F�N�g")]
    public GameObject fallingObjectPrefab;

    [Header("�����Ԋu�i�b�j")]
    public float spawnInterval = 1f;

    private float timer;

    void Update()
    {
        // �^�C�}�[��i�߂�
        timer += Time.deltaTime;

        // spawnInterval���o�߂����ꍇ�̂݃I�u�W�F�N�g�𐶐�
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;  // �^�C�}�[�����Z�b�g
        }
    }

    void SpawnObject()
    {
        // fallingObjectPrefab ���g�p���Atransform.position �Ő���
        GameObject obj = Instantiate(fallingObjectPrefab, transform.position, Quaternion.identity);

        // FallingObject�R���|�[�l���g���擾
        FallingObject fObj = obj.GetComponent<FallingObject>();

        // TextMeshPro�R���|�[�l���g���q�I�u�W�F�N�g����擾
        fObj.scoreText = obj.GetComponentInChildren<TextMeshPro>();
    }
}
