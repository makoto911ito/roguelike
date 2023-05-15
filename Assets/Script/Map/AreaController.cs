using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自分の上に乗っているオブジェクトは何か判定するためのスクリプト
/// </summary>
public class AreaController : MonoBehaviour
{
    public bool _onPlayer = false;
    public bool _onEnemy = false;
    public bool _onWall = false;
    public bool _stair = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _onPlayer = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            _onEnemy = true;
        }
    }

    public void ResetStatus()
    {
        _onPlayer = false;
        _onEnemy = false;
        _onWall = false;
        _stair = false;
    }


    /// <summary>
    /// 閉じた階段のイメージを表示させる
    /// </summary>
    /// <param name="closeStair"></param>
    public void Stair(GameObject closeStair)
    {
        var door = Instantiate(closeStair, new Vector3(transform.position.x, 0.51f, transform.position.z), Quaternion.identity);
        door.name = "door";
    }

    /// <summary>
    /// ボスが倒されたときに開いた階段のイメージを表示させる
    /// </summary>
    /// <param name="openStair"></param>
    public void OpenStair(GameObject openStair)
    {
        var objImage = GameObject.Find("door");
        Destroy(objImage);
        var stairImage  = Instantiate(openStair, new Vector3(transform.position.x, 0.51f, transform.position.z), Quaternion.identity);
        stairImage.name = "stair";
        _stair = true;
    }

}
