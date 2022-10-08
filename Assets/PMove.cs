using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    static public bool _buttonDown = false;
    int _count = 0;
    /// <summary>現在地のｘ軸</summary>
    int _pointX = 0;
    /// <summary>現在地のｚ軸</summary>
    int _pointZ = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Pmove();


    }

    void Pmove()
    {
        var velox = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");


        if (Input.GetButtonDown("Horizontal"))
        {
            //if(_buttonDown == false)//入力キーを押されたときに敵も一緒に動いてほしいので
            //{
            //    RizumuController._eMoveFlag = true;
            //}

            if (RizumuController._moveFlag == true && _buttonDown == false)
            {   
                if (velox > 0)
                {
                    //this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, MapManager._areas[_pointX + 1, _pointZ].transform.position.y, MapManager._areas[_pointX + 1, _pointZ].transform.position.z);
                }

                this.transform.position = new Vector3(this.transform.position.x + velox, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                Debug.Log("MISS");
            }
            _buttonDown = true;
        }

        if (Input.GetButtonDown("Vertical"))
        {
            //if(_buttonDown == false)
            //{
            //    RizumuController._eMoveFlag = true;
            //}

            if (RizumuController._moveFlag == true && _buttonDown == false)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + vertical);
            }
            else
            {
                Debug.Log("MISS");
            }
            _buttonDown = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        for(int i = 0; i < MapManager._x; i++)
        {
            for (int j = 0; j < MapManager._z; i++)
            {
                if(MapManager._areas[i,j] == collision.gameObject)
                {
                    //現在のプレイヤーの位置を調べる
                    _pointX = i;
                    _pointZ = j;
                }
            }
        }
    }
}
