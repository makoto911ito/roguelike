using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] StairPointCreate _stairPointCreate;
    [SerializeField] RizumuController _rizumuController;
    [SerializeField] MapManager _mapManager;
    [SerializeField] public GameObject _mapController;
  
    AreaController _areaController;

    [SerializeField] GameObject _closeDoor;
    [SerializeField] GameObject _stair;

    [SerializeField] int _maxFloor = 4;
    [SerializeField] int _minFloor = 2;

    int _randomFloorNum = 0;

    [SerializeField] bool _tesutFrag = false;

    /// <summary>階段を使った回数</summary>
    [SerializeField] public int _count = 0;

    private void Start()
    {
        if(_tesutFrag == false)
        {
            GoPlay(1);
        }
        else
        {
            _mapManager.BossMapCreate(_mapController);
        }
        GoSound();
        _randomFloorNum = Random.Range(_minFloor, _maxFloor + 1);
    }

    /// <summary>
    /// マップの生成を行う
    /// </summary>
    public void GoPlay(int count)
    {
        _count += count;

        //一番最初のフロアであればそのままマップの生成。そうでなければ今あるマップを消去し再度マップの生成を行う
        if (_count > 0)
        {

            if(_count == _randomFloorNum)
            {
                //ボスの部屋を作る
                _mapManager.BossMapCreate(_mapController);
            }
            else
            {
                foreach (Transform map in _mapController.transform)
                {
                    _areaController = map.GetComponent<AreaController>();
                    _areaController.ResetStatus();
                    Destroy(map.gameObject);
                    var obj = GameObject.Find("stair");
                    Destroy(obj);
                }

                StartCoroutine("CreateMap");
            }
        }
        else
        {
            _mapManager.MapCreate(_mapController);
            _stairPointCreate.PointCreate(_closeDoor);
        }
    }

    /// <summary>
    /// マップの生成と閉じている階段の設置
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateMap()
    {
        yield return new WaitForSeconds(1);
        _mapManager.MapCreate(_mapController);
        _stairPointCreate.PointCreate(_closeDoor);
    }

    /// <summary>
    /// 階段を表示する
    /// </summary>
    public void DetBoosEnemy()
    {
        _stairPointCreate.OpenDoor(_stair);
    }

    /// <summary>
    /// サウンドを流す
    /// </summary>
    void GoSound()
    {
        _rizumuController.PlaySound();
    }

    //private void Update()
    //{
    //    if(Input.GetButtonDown("Vertical"))
    //    {
    //        DetBoosEnemy();
    //    }

    //    if (Input.GetButtonDown("Horizontal"))
    //    {
    //        _count++;
    //        GoPlay();
    //    }
    //}
}
