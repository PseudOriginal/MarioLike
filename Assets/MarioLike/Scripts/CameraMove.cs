using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform levelEnd;
    public Transform levelStart;

    public Rigidbody2D PlayerBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBody.position.x > levelStart.position.x)
            this.transform.position = new Vector3(PlayerBody.position.x, this.transform.position.y, this.transform.position.z);
    }
}
