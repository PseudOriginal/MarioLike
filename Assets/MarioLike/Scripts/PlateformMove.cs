using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformMove : MonoBehaviour
{

    public float Speed;
    public Transform StartPoint;
    public Transform EndPoint;
    private float _alpha = 0;
    private int _switch = 1;
    public Rigidbody2D Body;
    private Transform _currentDestination ;

    // Start is called before the first frame update
    void Start()
    {
        _currentDestination = EndPoint;
    }
    

    void FixedUpdate()
    {
        //var cur = Vector2.Lerp(StartPoint.position, EndPoint.position, _alpha);
        //var pos = this.transform.position;
        //this.transform.position = new Vector3(cur.x , cur.y, pos.z);
        
        _alpha = _alpha + _switch*(Time.fixedDeltaTime * Speed);

        if (_alpha >= 1) 
        {
            _switch = -_switch;
            _currentDestination = StartPoint;
        }
        if(_alpha <= 0)
        {
            _switch = -_switch;
            _currentDestination = EndPoint;
        }

        Body.velocity = (_currentDestination.position - transform.position) * Speed;
    }

}
