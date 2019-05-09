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

    // 用于设置刹车力矩的大小
    public float stop_speed = 100;

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

        // 限制最大车速
        car_rigidbody.drag = car_rigidbody.velocity.magnitude / 250;

        // 空格刹车
        if (Input.GetKey(KeyCode.Space))
        {
            car_rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            car_rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            car_rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }

    void FixedUpdate()
    {
        // 前轮转向
        float steer = Input.GetAxis("Horizontal");
        FRwheelCollider.steerAngle = steer * 30;
        FLwheelCollider.steerAngle = steer * 30;

        // 后轮驱动
        float accelerate = Input.GetAxis("Vertical"); ;
        BRwheelCollider.motorTorque = accelerate * 1000;
        BLwheelCollider.motorTorque = accelerate * 1000;
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
