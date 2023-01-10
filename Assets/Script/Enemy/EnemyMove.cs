using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMove
{
    A,
    B
}

abstract class MoveType
{
    public abstract void Move();
}

//動きのコードは別スクリプトに新しく書く
//アブストラクトクラス（ｘ（仮））を継承する

class B : MoveType
{
    public override void Move()
    {

    }
}

public class EnemyMove : MonoBehaviour
{
    /// <summary></summary>
    [SerializeField] int _count;
    /// <summary></summary>
    [SerializeField] bool _change;
    /// <summary>スポーンしたＸ座標を取得するための変数</summary>
    public int _pointX;
    /// <summary>スポーンしたＺ座標を取得するための変数</summary>
    public int _pointZ;

    /// <summary>プレイヤー仕様のプレゼンターを取得するための変数</summary>
    PlayerPresenter _playerPresenter;

    /// <summary>敵仕様のプレゼンターを参照するための変数</summary>
    [SerializeField] EnemyPresenter _enemyPresenter = null;

    /// <summary>敵の移動種類を管理しているオブジェクトを参照するための変数</summary>
    [SerializeField] GameObject _moveType;


    //ここで行動の変化を管理する
    private MoveType _x;
    public EMove EMove
    {
        get { return _eMove; }
        set
        {
            switch (value)
            {
                case EMove.A:
                    _x = new EnemyTypeA(this, _playerPresenter);
                    break;
                case EMove.B:
                    _x = new B();
                    break;
            }
            _eMove = value;
        }
    }
    /// <summary>タイプによって動きを変えるそのタイプを管理する変数</summary>
    [SerializeField] EMove _eMove = EMove.A;

    private void Start()
    {
        //プレイヤーの情報を取得
        var gameObject = GameObject.Find("player(Clone)");
        if (gameObject == null)
        {
            Debug.Log("プレイヤーを取得できませんでした");
        }

        _playerPresenter = gameObject.GetComponent<PlayerPresenter>();

        _enemyPresenter.GetLisut();
        _enemyPresenter.Init();

        EMove = _eMove;
    }

    public void MoveEnemy()
    {
        //敵を動かす
        _x.Move();
    }

    public void Attack()
    {
        //攻撃をする
        _playerPresenter.Damage(1);
    }


    public void DeleteEnemy()
    {
        var areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
        areaController._onEnemy = false;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int x = 0; x < MapManager._x; x++)
        {
            for (int z = 0; z < MapManager._z; z++)
            {
                if (MapManager._areas[x, z] == collision.gameObject)
                {
                    //現在の位置を調べる
                    _pointX = x;
                    _pointZ = z;
                    //Debug.Log("現在の配列番号" + _pointX + " , " + _pointZ);
                }
            }
        }
    }
}
