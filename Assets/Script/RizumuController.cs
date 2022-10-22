using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RizumuController : MonoBehaviour
{
    /// <summary>タイミングの時間を取得する変数</summary>
    static public float _time;
    /// <summary>動けるタイミングを決める変数</summary>
    public float _keisoku = 1;

    /// <summary>プレイヤーが動けるかどうか判定するフラグ</summary>
    static public bool _moveFlag = false;

    /// <summary>敵が動けるかどうか判定をするフラグ</summary>
    static public bool _eMoveFlag = false;

    public Text _text;

    /// <summary>入力を受け受けている時間</summary>
    [SerializeField] float _pTaim = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _time.ToString();

        _time += Time.deltaTime;
        if (_time > _keisoku)
        {
            //Debug.Log("動いた");
            _moveFlag = true;

            if (_time > _keisoku + _pTaim) //入力のラグを考慮して遅らせている
            {
                _eMoveFlag = true;
                _time = 0;
                PMove._buttonDown = false;
            }
        }
        else
        {
            _moveFlag = false;
        }
    }
}
