using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
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

        if(Input.GetButtonDown("Horizontal"))
        {
            
            if (RizumuController._moveFlag == true)
            {
                this.transform.position = new Vector3(this.transform.position.x + velox , this.transform.position.y, this.transform.position.z );
            }
            else
            {
                Debug.Log("MISS");
            }
        }

        if(Input.GetButtonDown("Vertical"))
        {

            if (RizumuController._moveFlag == true)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + vertical);
            }
            else
            {
                Debug.Log("MISS");
            }
        }
    }
}
