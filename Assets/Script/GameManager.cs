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

    /// <summary>�K�i���g������</summary>
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
    /// �}�b�v�̐������s��
    /// </summary>
    public void GoPlay(int count)
    {
        _count += count;

        //��ԍŏ��̃t���A�ł���΂��̂܂܃}�b�v�̐����B�����łȂ���΍�����}�b�v���������ēx�}�b�v�̐������s��
        if (_count > 0)
        {

            if(_count == _randomFloorNum)
            {
                //�{�X�̕��������
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
    /// �}�b�v�̐����ƕ��Ă���K�i�̐ݒu
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateMap()
    {
        yield return new WaitForSeconds(1);
        _mapManager.MapCreate(_mapController);
        _stairPointCreate.PointCreate(_closeDoor);
    }

    /// <summary>
    /// �K�i��\������
    /// </summary>
    public void DetBoosEnemy()
    {
        _stairPointCreate.OpenDoor(_stair);
    }

    /// <summary>
    /// �T�E���h�𗬂�
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
