using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̏�ɏ���Ă���I�u�W�F�N�g�͉������肷�邽�߂̃X�N���v�g
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
    /// �����K�i�̃C���[�W��\��������
    /// </summary>
    /// <param name="closeStair"></param>
    public void Stair(GameObject closeStair)
    {
        var door = Instantiate(closeStair, new Vector3(transform.position.x, 0.51f, transform.position.z), Quaternion.identity);
        door.name = "door";
    }

    /// <summary>
    /// �{�X���|���ꂽ�Ƃ��ɊJ�����K�i�̃C���[�W��\��������
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
