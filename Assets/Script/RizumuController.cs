using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RizumuController : MonoBehaviour
{
    [SerializeField] AudioClip _audio;
    AudioSource _audioSource = default;

    //[SerializeField] Text _testText;

    float _bpm = 0;

    int _count = 1;
    /// <summary>与えられた音源の周波数</summary>
    int _frequency = 0;

    /// <summary></summary>
    float _time = 0;
    static public float _beat = 0;
    float _keikajikan = 0;
    float _judgeTime = 0;

    /// <summary>１ビートの秒数</summary>
    float _beatTime = 0;

    static public float _moveTime = 0;

    static float _mae = 0;
    static float _usiro = 0;

    GameObject _player;
    PlayerMove _playerMove;

    [SerializeField] Image _sponImage;
    [SerializeField] NotesMove _notesImage;
    [SerializeField] Image _notesPoint;

    [SerializeField] EnemyList _enemyList;

    public void Judgetiming(float audioTime, float moveNumber, string direction)
    {
        Debug.Log(audioTime + " 渡された値" + " " + _mae + "入力可能範囲（早め）");

        if (audioTime >= _mae)
        {
            Debug.Log("うごけた");
            _playerMove.GoMove(moveNumber, direction);
        }
        else if (audioTime >= _usiro || audioTime < _mae)
        {
            Debug.Log("ミス");
        }
    }

    public void PlaySound()
    {
        _audioSource = GetComponent<AudioSource>();

        //与えられた音源の周波数を確認
        _frequency = _audio.frequency;

        _bpm = BpmChecker.AnalyzeBpm(_audio, _frequency);
        if (_bpm < 0)//帰ってきたBPMの値が０よりも小さい値であれば音源が入っていないことになるそれをエラーとしてデバックする
        {
            Debug.LogError("AudioClip is null.");
            return;
        }

        Debug.Log(_bpm);

        _beatTime = 60 / _bpm;

        //判定のズレの秒数を取得
        _judgeTime = _frequency * Time.fixedDeltaTime;

        //曲を流す
        _audioSource.clip = _audio;
        _audioSource.Play();
    }

    public void NowPlayerSpawn()
    {
        _player = GameObject.Find("Player");
        _playerMove = _player.GetComponent<PlayerMove>();
    }

    //Debug.Log(_audioSource.timeSamples + "　タイム");
    //    float bite = 60f / (float)_bpm;
    //Debug.Log(bite + "ビート");
    //    float ans = _frequency * bite;
    //Debug.Log(ans + " サンプル/ビート");

    private void Update()
    {
        _time = _audioSource.time;
        _moveTime = _audioSource.timeSamples;//曲の経過時間を取得
        _beat = Beat(_count);//判定の間隔を取得
        _mae = _beat - _judgeTime;//取得した判定の前ズレ
        _usiro = _beat + _judgeTime;//取得した間隔の後ズレ

        _keikajikan = _moveTime - _beat;
        if (_moveTime >= _beat)
        {
            //Debug.Log(_time);
            //_testText.text = "はい".ToString();
        }

        if (_keikajikan > _judgeTime && _moveTime > _usiro)
        {

            _enemyList.GoEnemyMove();
            //_testText.text = "".ToString();
            _count++;
            var image = Instantiate(_notesImage, _sponImage.rectTransform);
            //image.transform.parent = _notesPoint.transform;
            image.transform.SetParent(_notesPoint.transform);
            image.Setup(_beatTime);
        }
    }

    /// <summary>
    /// 判定の間隔を求める
    /// </summary>
    /// <param name="count">回数</param>
    /// <returns></returns>
    float Beat(float count)
    {
        return _frequency * (60 * count / _bpm);
    }
}
