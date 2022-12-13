using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMove
{
    MoveA = 0,
}


public class EnemyMove : MonoBehaviour
{
    /// <summary></summary>
    [SerializeField] int _count;
    /// <summary></summary>
    [SerializeField] bool _change;
    /// <summary>タイプによって動きを変えるそのタイプを管理する変数</summary>
    [SerializeField] int _eMove = 0;
    /// <summary>スポーンしたＸ座標を取得するための変数</summary>
    public int _pointX;
    /// <summary>スポーンしたＺ座標を取得するための変数</summary>
    public int _pointZ;

    AreaController areaController;

    PlayerPresenter _playerPresenter;

    [SerializeField] EnemyPresenter _enemyPresenter = null;

    private void Start()
    {

        var gameObject = GameObject.Find("player(Clone)");

        if (gameObject == null)
        {
            Debug.Log("プレイヤーを取得できませんでした");
        }

        _playerPresenter = gameObject.GetComponent<PlayerPresenter>();

        _enemyPresenter.GetLisut();
        _enemyPresenter.Init();
    }

    public void MoveEnemy()
    {
        //タイプによって動きを変える
        if (_eMove == (int)EMove.MoveA)
        {
            MoveA();
        }
    }

    void MoveA()
    {
        if (_change == false && _count == 0 || _change == true && _count != 0)//反転がfalse且つcountが0だったらor反転していて且つcountが0じゃなったら
        {
            //行きたい方向の情報を確認したいので移動先のスクリプトを取得する
            areaController = MapManager._areas[_pointX + 1, _pointZ].GetComponent<AreaController>();

            //移動先の情報によって行動を決める
            if (areaController._onWall == true)
            {
                //反対に移動するかもしれない
            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //攻撃をする
                _playerPresenter.Damage(1);
            }
            else
            {
                areaController._onEnemy = true;
                areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                areaController._onEnemy = false;
                this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                _pointX = _pointX + 1;
            }
        }
        else if (_change == false && _count != 0 || _change == true && _count == 0) //反転がfalse且つcountが0じゃなかったらor反転していて且つcountが0だったら
        {
            //行きたい方向の情報を確認したいので移動先のスクリプトを取得する
            areaController = MapManager._areas[_pointX - 1, _pointZ].GetComponent<AreaController>();

            //移動先の情報によって行動を決める
            if (areaController._onWall == true)
            {

            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //プレイヤーに攻撃する
                _playerPresenter.Damage(1);
            }
            else
            {
                areaController._onEnemy = true;
                areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                areaController._onEnemy = false;
                this.transform.position = new Vector3(MapManager._areas[_pointX - 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                _pointX = _pointX - 1;
            }
        }

        _count++;

        if (_count == 2)
        {
            _count = 0;
            if (_change == false)
            {
                _change = true;
            }
            else
            {
                _change = false;
            }
        }

    }

    public void DeleteEnemy()
    {
        areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
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
