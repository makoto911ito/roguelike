using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>�G�L�������Ǘ����郊�X�g</summary>
    List<GameObject> _enemys = new List<GameObject>();

    /// <summary>
    /// �G�I�u�W�F�N�g�����X�g�ɓ���邽�߂̊֐�
    /// </summary>
    /// <param name="enemy">�G�I�u�W�F�N�g</param>
    public void Enemy(GameObject enemy)
    {
        _enemys.Add(enemy);
    }

    /// <summary>
    /// �G�𓮂������߂̊֐�
    /// </summary>
    public void GoEnemyMove()
    {
        for (var i = 0; i < _enemys.Count; i++)
        {
            EnemyMove _enemyMove = _enemys[i].GetComponent<EnemyMove>();
            _enemyMove.MoveEnemy();
        }
    }
}
