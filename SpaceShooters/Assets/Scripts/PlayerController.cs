using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//template <class T>
//T max(T x, T y)
//{
//    if (x > y) return x;
//    else return y;
//}
[System.Serializable]                   // Boundary의 직렬화
public class Boundary
{
    public float Hmin;
    public float Hmax;
    public float Vmin;
    public float Vmax;
}

public class PlayerController : MonoBehaviour {

    float moveHorizontal;
    float moveVertical;
    float Speed = 7.0f;     // 이동 스피드
    float tilt = 4.0f;      // 플레이어 기체 기울기
    public Rigidbody rd;
    public Vector3 movement;
    public Boundary boundary;
    
    void start()
    {
        rd = this.GetComponent<Rigidbody>();
        //boundary = new Boundary();
    }

    void FixedUpdate()
    {        
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rd.velocity =  movement * Speed;
        rd.position = new Vector3(
        Mathf.Clamp(rd.position.x, boundary.Hmin, boundary.Hmax),           // Hmin _ Hmax 와 Vmin _ Vmax 이 바뀌었다... 
        0.0f,
        Mathf.Clamp(rd.position.z, boundary.Vmin, boundary.Vmax)
        );

        rd.rotation = Quaternion.Euler(0.0f, 0.0f, rd.velocity.x * -tilt);      // 좌우 이동간 기체 기울기 z축 조정.
        
    //Debug.Log(10);

    }

}
