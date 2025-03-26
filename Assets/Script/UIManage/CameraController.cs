using System.Collections;
using UnityEngine;
using Cinemachine;
//ʹ��������CinemachineVirtualCamera���Բ�ʹ��
public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    //�������������ӽǷŴ���С�����ֵ
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Update()
    //��ôд�����￪ʼ����ô"ֱ��"�Ĵ����ˣ�
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
        //transform.forward,transform.right�����Ե�ǰtransform���жϵģ��ʻῼ�ǵ���ת
        transform.position += moveSpeed * moveVector * Time.deltaTime;
        //transform.position += inputMoveDir * movespeed * Time.deltaTime;����д�Ļ�����������ת��wasd�ƶ������ǻᰴ��Ĭ�Ϸ�������������

        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }
        //�����ӽ���ת�е�Q�ĸо���������������������Ȼ�ù����Σ����ǹ���̫�໹�ϲ�����
        float rotationSpeed = 100f;
        transform.eulerAngles += rotationSpeed * rotationVector * Time.deltaTime;
        //����ͷ����CameraController��ת���ƶ�����ֻҪ��CameraController��ת�Ϳ�����
        //�޸�ŷ���ǣ�������Ԫ��?
        //����
        //�·�����������Ŵ���С���߼���ͨ���ı�����VisualCamera-CinemachineVirtualCamera-Body-FOllowOffset��yֵ��Zֵʵ�ֵ�
        //yֵ����ʹCamera���Ϸ��ƶ���Zֵ��������ǰ���ƶ�����ΪFollow��ԭ����Щ�ı䶼��Camera����
        //CameraController������½���
        //����
        //��һ�ַŴ���С�߼��ǷŴ�VitualCamera-CinemachineVirtualCamera-Lens-Vertical FOVֵ����С��֮�Ŵ�
        //��������������y-z��ͬʱ�ı�(���Ƶ����˳Ƶ���������ͷ����)�ڴ���Ϸ�в������

        CinemachineTransposer cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        //Ҫ���޸�Cinemachine���������ֻ��ʹ�����õ�GetCinemachineComponent����
        //Cinemachine���������޸����https://www.cnblogs.com/CCLi/p/14169760.html
        Vector3 followOffset = cinemachineTransposer.m_FollowOffset;
        //������Ҫͨ��body-FollowOffset��ֵ��ʵ������
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0)//�����ȡ���������ֵĹ�����0 1 -1����ֵ
        {
            followOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset.y += zoomAmount;
        }
        followOffset.y = Mathf.Clamp(followOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        //Mathf.Clamp(float value, float min, float max);
        //���У�value��ʾҪ���Ƶ�ֵ��min��ʾ���Ʒ�Χ����Сֵ��max��ʾ���Ʒ�Χ�����ֵ��
        //���valueС��min���򷵻�min�����value����max���򷵻�max�����򷵻�value����
        cinemachineTransposer.m_FollowOffset = followOffset;
    }


}
