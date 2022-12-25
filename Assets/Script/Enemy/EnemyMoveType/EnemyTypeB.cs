using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeB : MonoBehaviour
{
    /// <summary>間隔のカウント</summary>
    [SerializeField] int _count;
    /// <summary>方向の切り替え</summary>
    [SerializeField] bool _change;
    /// <summary>スポーンしたＸ座標を取得するための変数</summary>
    public int _pointX;
    /// <summary>スポーンしたＺ座標を取得するための変数</summary>
    public int _pointZ;

    int _playePointX;
    int _playePointZ;

    [SerializeField] EnemyMove _eMove;

    /// <summary>ステージの情報を管理しているスクリプトを参照するための変数</summary>
    AreaController areaController;

    public void MoveB()
    {
        int pointx = _pointX - 2;
        int pointz = _pointZ - 2;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (pointx + i == _pointX && pointz + j == _pointZ)
                {
                    return;
                }
                else
                {
                    areaController = MapManager._areas[pointx + i, pointz + j].GetComponent<AreaController>();
                    if (areaController._onPlayer == true)
                    {
                        _playePointX = pointx;
                        _playePointZ = pointz;
                    }
                }
            }
        }

        
        int pointXdistance;
        int pointZdistance;

        //falseだと-1・trueだと+1
        bool x;
        bool y;

        //どっちの軸のほうがプレイヤーに近いか判定するために差を求めている
        if (_playePointX < 0 && _pointX < 0 || _playePointX > 0 && _pointX > 0)
        {
            if(_playePointX < _pointX)
            {
                x = false;
            }
            else
            {
                x = true;
            }
            pointXdistance = _playePointZ - _pointX;
            if (_playePointZ < 0 && _pointZ < 0 || _playePointZ > 0 && _pointZ > 0)
            {
                pointZdistance = _playePointZ - _pointZ;

            }
            else
            {
                if (_playePointZ > 0)
                {
                    pointZdistance = _playePointZ - (_pointZ *= -1);
                }
                else
                {
                    pointZdistance = (_playePointZ *= -1) - _pointZ;
                }
            }
        }
        else
        {
            if(_playePointX > 0)
            {
                pointXdistance = _playePointX - (_pointX *= -1);
                if (_playePointZ < 0 && _pointZ < 0 || _playePointZ > 0 && _pointZ > 0)
                {
                    pointZdistance = _playePointZ - _pointZ;
                }
                else
                {
                    if (_playePointZ > 0)
                    {
                        pointZdistance = _playePointZ - (_pointZ *= -1);
                    }
                    else
                    {
                        pointZdistance = (_playePointZ *= -1) - _pointZ;
                    }
                }
            }
            else
            {
                pointXdistance = (_playePointX *= -1) - _pointX;
                if (_playePointZ < 0 && _pointZ < 0 || _playePointZ > 0 && _pointZ > 0)
                {
                    pointZdistance = _playePointZ - _pointZ;
                }
                else
                {
                    if (_playePointZ > 0)
                    {
                        pointZdistance = _playePointZ - (_pointZ *= -1);
                    }
                    else
                    {
                        pointZdistance = (_playePointZ *= -1) - _pointZ;
                    }
                }
            }
        }

        if (pointXdistance < 0)
        {
            pointXdistance *= -1;
        }

        if (pointZdistance < 0)
        {
            pointZdistance *= -1;
        }

        if (pointXdistance < pointZdistance)
        {
            //if()
            //{

            //}
            areaController = MapManager._areas[_pointX,_pointZ].GetComponent<AreaController>();
            //移動先の情報によって行動を決める
            if (areaController._onWall == true)
            {
                //反対に移動するかもしれない
            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //攻撃をする
                _eMove.Attack();
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
        else
        {
            areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
            //移動先の情報によって行動を決める
            if (areaController._onWall == true)
            {
                //反対に移動するかもしれない
            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //攻撃をする
                _eMove.Attack();
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
    }
}
