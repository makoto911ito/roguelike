using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>敵キャラを管理するリスト</summary>
    List<GameObject> _enemys = new List<GameObject>();

    /// <summary>
    /// 敵オブジェクトをリストに入れるための関数
    /// </summary>
    /// <param name="enemy">敵オブジェクト</param>
    public void Enemy(GameObject enemy)
    {
        _enemys.Add(enemy);
    }

    /// <summary>
    /// 敵を動かすための関数
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
