using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMove : MonoBehaviour
{
    /// <summary>はじく位置までの距離</summary>
    float _kyori;
    /// <summary>はじく位置までにかける時間</summary>
    float _time;

    /// <summary>スポーン場所</summary>
    GameObject _spon;
    /// <summary>判定をとる場所</summary>
    GameObject _point;

    RectTransform _sponRect;
    RectTransform _pointRect;
    RectTransform _myRect;

    /// <summary>
    /// １ビートの秒数を取得するための関数
    /// </summary>
    /// <param name="beat">１ビートの秒数</param>
    public void Setup(float beat)
    {
        _time = beat;
    }

    // Start is called before the first frame update
    void Start()
    {
        _point = GameObject.Find("NotesJudgePoint");

        _pointRect = _point.GetComponent<RectTransform>();

        _myRect = GetComponent<RectTransform>();

        //Debug.Log(_pointRect.anchoredPosition.x);

        _kyori = Mathf.Abs(_myRect.anchoredPosition.x - _pointRect.anchoredPosition.x);
        //Debug.Log(_kyori);
    }

    // Update is called once per frame
    void Update()
    {
        _myRect.anchoredPosition = new Vector2(_myRect.anchoredPosition.x + _kyori * _time * Time.deltaTime * 1f, 0);

        if (_myRect.anchoredPosition.x >= _pointRect.anchoredPosition.x)
        {
            Destroy(gameObject);
        }
    }
}
