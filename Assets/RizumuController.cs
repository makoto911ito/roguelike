using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RizumuController : MonoBehaviour
{
    float _time;
    public float _keisoku = 1;
    static public bool _moveFlag = false;
    static public bool _eMoveFlag = false;
    public Text _text;
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
            if(_time > _keisoku + (_pTaim % 2))
            {
                    _eMoveFlag = true;
            }

            if (_time > _keisoku + _pTaim) //入力のラグを考慮して遅らせている
            {
                _time = 0;
            }
        }
        else
        {
            _moveFlag = false;
            _eMoveFlag = false;
        }
    }
}
