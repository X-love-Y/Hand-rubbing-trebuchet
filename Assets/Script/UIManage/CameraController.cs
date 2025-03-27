using System.Collections;
using UnityEngine;
using Cinemachine;
//使其能引入CinemachineVirtualCamera属性并使用
public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    //这两个常量是视角放大缩小的最大值
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Update()
    //怎么写到这里开始用这么"直观"的代码了？
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        float moveSpeed = 10f;
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        //transform.forward,transform.right都是以当前transform来判断的，故会考虑到旋转
        transform.position += moveSpeed * moveVector * Time.deltaTime;
        //transform.position += inputMoveDir * movespeed * Time.deltaTime;这样写的话如果摄像机旋转，wasd移动方向还是会按照默认方向来，不可行

        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }
        //这里视角旋转有点Q的感觉，调不过来，这个组件虽然用过两次，但是功能太多还认不过来
        float rotationSpeed = 100f;
        transform.eulerAngles += rotationSpeed * rotationVector * Time.deltaTime;
        //摄像头跟随CameraController旋转和移动，故只要让CameraController旋转就可以了
        //修改欧拉角，不用四元数?
        //！！
        //下方进行摄像机放大缩小的逻辑是通过改变加入的VisualCamera-CinemachineVirtualCamera-Body-FOllowOffset的y值和Z值实现的
        //y值增大使Camera往上放移动，Z值增大向正前方移动，因为Follow的原因，这些改变都在Camera看向
        //CameraController的情况下进行
        //！！
        //另一种放大缩小逻辑是放大VitualCamera-CinemachineVirtualCamera-Lens-Vertical FOV值来缩小反之放大
        //但是这种缩放是y-z轴同时改变(类似第三人称单机的摄像头缩放)在此游戏中不够灵活

        CinemachineTransposer cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        //要想修改Cinemachine组件的属性只能使用内置的GetCinemachineComponent方法
        //Cinemachine组件代码简单修改详见https://www.cnblogs.com/CCLi/p/14169760.html
        Vector3 followOffset = cinemachineTransposer.m_FollowOffset;
        //这里是要通过body-FollowOffset的值来实现缩放
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0)//这个获取的是鼠标滚轮的滚动，0 1 -1三个值
        {
            followOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset.y += zoomAmount;
        }
        followOffset.y = Mathf.Clamp(followOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        //Mathf.Clamp(float value, float min, float max);
        //其中，value表示要限制的值，min表示限制范围的最小值，max表示限制范围的最大值。
        //如果value小于min，则返回min；如果value大于max，则返回max；否则返回value本身
        cinemachineTransposer.m_FollowOffset = followOffset;
    }


}
