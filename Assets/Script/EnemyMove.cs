using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMove
{
    MoveA = 0,
}


public class EnemyMove : MonoBehaviour
{
    Rigidbody _rd;
    /// <summary></summary>
    [SerializeField] int _count;
    /// <summary></summary>
    [SerializeField] bool _change;
    /// <summary>タイプによって動きを変えるそのタイプを管理する変数</summary>
    [SerializeField] EMove _eMove = EMove.MoveA;
    /// <summary>プレイヤーのオブジェクトを取得</summary>
    [SerializeField] GameObject _player;
    /// <summary>スポーンしたＸ座標を取得するための変数</summary>
    int _pointX;
    /// <summary>スポーンしたＺ座標を取得するための変数</summary>
    int _pointZ;

    AreaController areaController;

    // Start is called before the first frame update
    void Start()
    {
        _rd = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RizumuController._eMoveFlag == true)
        {
            //タイプによって動きを変える
            if (_eMove == EMove.MoveA)
            {
                MoveA();
            }
        }
    }

    void MoveA()
    {
        if (_change == false)
        {
            if (_count == 0)
            {
                //行きたい方向の情報を確認したいので移動先のスクリプトを取得する
                areaController = MapManager._areas[_pointX + 1, _pointZ].GetComponent<AreaController>();

                //移動先の情報によって行動を決める
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX + 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }
            else
            {
                //行きたい方向の情報を確認したいので移動先のスクリプトを取得する
                areaController = MapManager._areas[_pointX - 1, _pointZ].GetComponent<AreaController>();

                //移動先の情報によって行動を決める
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX - 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX - 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }

            _count++;

            if (_count == 2)
            {
                _count = 0;
                _change = true;
            }
        }
        else
        {
            if (_count == 0)
            {
                //行きたい方向の情報を確認したいので移動先のスクリプトを取得する
                areaController = MapManager._areas[_pointX - 1, _pointZ].GetComponent<AreaController>();

                //移動先の情報によって行動を決める
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX - 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX - 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }
            else
            {
                //行きたい方向の情報を確認したいので移動先のスクリプトを取得する
                areaController = MapManager._areas[_pointX + 1, _pointZ].GetComponent<AreaController>();

                //移動先の情報によって行動を決める
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX + 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }

            _count++;

            if (_count == 2)
            {
                _count = 0;
                _change = false;
            }
        }
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
                    Debug.Log("現在の配列番号" + _pointX + " , " + _pointZ);
                }
            }
        }
    }
}
