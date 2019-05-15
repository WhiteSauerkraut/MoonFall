using UnityEngine;
using System.Collections;
public class MoveCar : MonoBehaviour
{
    // 获取四个车轮
    public GameObject BRWheel;     	
    public GameObject BLWheel;
    public GameObject FRWheel;
    public GameObject FLWheel;

    // 车轮渲染对象
    public GameObject BRwheelMesh;
    public GameObject BLwheelMesh;
    public GameObject FRwheelMesh;
    public GameObject FLwheelMesh;

    // 车轮碰撞器
    private WheelCollider BRwheelCollider;
    private WheelCollider BLwheelCollider;
    private WheelCollider FRwheelCollider;
    private WheelCollider FLwheelCollider;

    // 获取刚体对象
    private Rigidbody car_rigidbody;

    void Start()
    {
        BRwheelCollider = BRWheel.GetComponent<WheelCollider>();
        BLwheelCollider = BLWheel.GetComponent<WheelCollider>();
        FRwheelCollider = FRWheel.GetComponent<WheelCollider>();
        FLwheelCollider = FLWheel.GetComponent<WheelCollider>();

        // 设置重心
        car_rigidbody = GetComponent<Rigidbody>();
        car_rigidbody.centerOfMass = Vector3.zero;
    }

    void Uptate()
    {
        UpdateMeshPositions();
    }

    void FixedUpdate()
    {
        // 空格刹车
        if (Input.GetKey(KeyCode.Space))
        {
            BRwheelCollider.motorTorque = 0;
            BLwheelCollider.motorTorque = 0;
            car_rigidbody.drag = 1;
        }
        else
        {
            float steer = Input.GetAxis("Horizontal");
            float accelerate = Input.GetAxis("Vertical");

            if(steer != 0 || accelerate != 0)
            {
                // 限制最大车速
                car_rigidbody.drag = car_rigidbody.velocity.magnitude / 250;

                // 前轮转向
                FRwheelCollider.steerAngle = steer * 30;
                FLwheelCollider.steerAngle = steer * 30;

                // 后轮驱动
                BRwheelCollider.motorTorque = accelerate * 1000;
                BLwheelCollider.motorTorque = accelerate * 1000;
            }
            else
            {
                car_rigidbody.drag = 0.3f;
            }
        }
    }

    void UpdateMeshPositions()
    {
        Vector3 pos;
        Quaternion quat;

        BRwheelCollider.GetWorldPose(out pos, out quat);
        BRwheelMesh.transform.position = pos;
        BRwheelMesh.transform.rotation = quat;

        BLwheelCollider.GetWorldPose(out pos, out quat);
        BLwheelMesh.transform.position = pos;
        BLwheelMesh.transform.rotation = quat;

        FRwheelCollider.GetWorldPose(out pos, out quat);
        FRwheelMesh.transform.position = pos;
        FRwheelMesh.transform.rotation = quat;

        FLwheelCollider.GetWorldPose(out pos, out quat);
        FLwheelMesh.transform.position = pos;
        FLwheelMesh.transform.rotation = quat;

    }
}
