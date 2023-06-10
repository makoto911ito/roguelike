using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] StairPointCreate _stairPointCreate;
    [SerializeField] RizumuController _rizumuController;
    [SerializeField] MapManager _mapManager;
    [SerializeField] public GameObject _mapController;
    [SerializeField] GameObject _nootCanvas;
    [SerializeField] Image _loadImage;

    AreaController _areaController;

    [SerializeField] GameObject _closeDoor;
    [SerializeField] GameObject _stair;

    //生成するフロアの最大値と最小値
    [SerializeField] int _maxFloor = 4;
    [SerializeField] int _minFloor = 2;

    [SerializeField] int _randomFloorNum = 0;

    /// <summary>テスト用のフラグ</summary>
    [SerializeField] bool _tesutFrag = false;

    /// <summary>階段を使った回数</summary>
    [SerializeField] public int _count = 0;

    /// <summary>リザルト画面で表示するクリアか否かの判定</summary>
    public string _gameResult;

    /// <summary>クリアしたの判定</summary>
    public bool _isVictory = false;

    /// <summary>挑戦している時間</summary>
    public float _playTime = 0;

    /// <summary>ダンジョンに潜っているかどうか</summary>
    public bool _nowPlay = false;

    [SerializeField] EnemyList _enemyList;

    private void Start()
    {
        if (_tesutFrag == false)
        {
            LoadMap(true);
            GoPlay(0);
        }
        else
        {
            _mapManager.BossMapCreate(_mapController);
        }
        _randomFloorNum = Random.Range(_minFloor, _maxFloor + 1);
    }

    /// <summary>
    /// マップの生成を行う
    /// </summary>
    public void GoPlay(int count)
    {
        LoadMap(true);
        _count += count;

        //一番最初のフロアであればそのままマップの生成。そうでなければ今あるマップを消去し再度マップの生成を行う
        if (_count > 0)
        {

            foreach (Transform map in _mapController.transform)
            {
                _areaController = map.GetComponent<AreaController>();
                _areaController.ResetStatus();
                Destroy(map.gameObject);
                var obj = GameObject.Find("stair");
                Destroy(obj);
            }

            _enemyList.EnemyReset();

            if (_count == _randomFloorNum)
            {              
                //ボスの部屋を作る
                StartCoroutine(CreateBoosMap());
            }
            else
            {
                StartCoroutine(CreateMap());
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
        yield return new WaitForSeconds(0.5f);
        _rizumuController.StopSound();
        _mapManager.MapCreate(_mapController);
        _stairPointCreate.PointCreate(_closeDoor);
    }

    IEnumerator CreateBoosMap()
    {
        yield return new WaitForSeconds(0.5f);
        _rizumuController.StopSound();
        _mapManager.BossMapCreate(_mapController);
        _stairPointCreate.PointCreate(_closeDoor);
    }

    /// <summary>
    /// 階段を表示する
    /// </summary>
    public void DetBoosEnemy()
    {
        _stairPointCreate.OpenDoor(_stair);
    }

    public void DetGameBoosEnemy()
    {
        _gameResult = "クリアしました";
        _isVictory = true;
        _nootCanvas.SetActive(false);
        SceneManager.LoadScene("SelectScene", LoadSceneMode.Additive);
    }

    /// <summary>
    /// サウンドを流す
    /// </summary>
    public void GoSound()
    {
        _rizumuController.PlaySound();
    }

    /// <summary>
    /// マップ生成～プレイヤースポーンまでの時間にロード画面表示するための関数
    /// </summary>
    public void LoadMap(bool Loadflag)
    {
        if (Loadflag == true)
        {
            _nowPlay = false;
            _loadImage.enabled = true;
            _nootCanvas.SetActive(false);
        }
        else
        {
            _loadImage.enabled = false;
            _nootCanvas.SetActive(true);
            _nowPlay = true;
        }
    }

    /// <summary>
    /// プレイヤーが倒されたら呼ばれる関数
    /// </summary>
    public void GameOvare(int PlayerPosX, int PlayerPosZ)
    {
        Debug.Log("死亡しました");
        _areaController = MapManager._areas[PlayerPosX, PlayerPosZ].GetComponent<AreaController>();
        _areaController._onPlayer = false;
        _nootCanvas.SetActive(false);
        _gameResult = "倒されました";//表示の仕方を「（敵の名前）に倒されました」にする
        _isVictory = false;
        SceneManager.LoadScene("SelectScene", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if(_nowPlay == true)
        {
            _playTime += Time.deltaTime;
        }
    }
}
