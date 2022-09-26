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
    [SerializeField] int _count;
    [SerializeField] bool _change;
    [SerializeField] EMove _eMove = EMove.MoveA;
    [SerializeField] GameObject _player;

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
                this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
                //Debug.Log("エネミーがｘ＋１動いた");
                RizumuController._eMoveFlag = false;
                RizumuController._moveFlag = false;
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
                //Debug.Log("エネミーがｘ-１動いた");
                RizumuController._eMoveFlag = false;
                RizumuController._moveFlag = false;
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
                this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
                //Debug.Log("エネミーがｘ-１動いた");
                RizumuController._eMoveFlag = false;
                RizumuController._moveFlag = false;

            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
                //Debug.Log("エネミーがｘ＋１動いた");
                RizumuController._eMoveFlag = false;
                RizumuController._moveFlag = false;
            }

            _count++;

            if (_count == 2)
            {
                _count = 0;
                _change = false;
            }
        }
    }
}
