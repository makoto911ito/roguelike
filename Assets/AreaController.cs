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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
        else if (collision.gameObject.CompareTag("Wall"))
        {
            _onWall = true;
        }
    }
}
