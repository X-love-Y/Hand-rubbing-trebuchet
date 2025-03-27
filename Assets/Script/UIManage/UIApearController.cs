using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIAppearController : MonoBehaviour
{
    //����ģʽ�Ұ���
    public static UIAppearController instance;
    //�����˱��ű����ʵ��instance
    public RectTransform canvasRect; // ����Canvas��RectTransform
    public RectTransform hoverImage; // ������Ҫ��ʾ��UIͼ��

    [Header("Text��Ϣ")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI introText;
    public List<TextMeshProUGUI> textMeshPros = new List<TextMeshProUGUI>();
    [Header("ѡ��")]
    public UIAppearSet slectedUI;

    [SerializeField] private LayerMask appearLayer;
    [SerializeField] private float UIappearTime = 1f;//��ͣ��ʾʱ��
    [SerializeField] private Image UIimage;
    [SerializeField] private Camera mainCamera;
    private float UIappearCounter = 0f;//��ʱ��
    public bool isChecked = false;

    [SerializeField] private float Xoffset;
    [SerializeField] private float Yoffset;
    public float fadeDuration = 1f; // �������ʱ��

    private void Awake()
    {
        instance = this;
        //ʹinstanceʵ��ʵ����Ϊ���ű�
    }
    private void Start()
    {
        textMeshPros.Add(nameText);
        textMeshPros.Add(introText);

        SetAlpha(0f);
    }
    private void Update()
    {
        transform.position = GetPosition();

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, appearLayer))
        {
            UIappearCounter += Time.deltaTime;
            if (UIappearCounter >= UIappearTime)
            {
                SelectedUIObj(hit.collider.GetComponent<UIAppearSet>());
                if (!isChecked)//����Ǳ�֤�������ֻ����һ�ε�
                    UIAppearLogic();
                isChecked = true;
            }
        }
        else
        {
            DeselectUIObj();
            UIappearCounter = 0;
            if (isChecked)
                StartCoroutine(FadeImage(1f, 0f));
            isChecked = false;
        }
    }

    public static Vector3 GetPosition()
    //static���������ڲ������ͷ���ʹ�����������������ʵ���������ڽű���ʵ����Ҳ����.�����������ڲ�����
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //�ҵ�tagΪmaincamera��camera���
        //ScreenPointToRay() ��Unity��Camera���һ�����������ڽ���Ļ�ϵ�һ����ת��Ϊһ�����ߡ��������ߵ���������������Ļ�϶�Ӧ�ĵ㣬
        //�����Ǵ����������ָ���Ǹ��㡣���ڽ����������м��ʱ�ǳ����ã��ر������û��������꽻����صĳ����С�
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.appearLayer);//���ûʵ���ô���ֻ������raycastHit������ʹ���������������ò�֮�����ײ
        //Raycast�����ڶ���ֵʹout��ʽ���ʴ�ֻ��Ҳдout��out��ʹֵ��ʧ�������¸�ֵ��Ҳ���ǻ�ı�
        //RaycastHit ���� Unity �е�һ���ṹ�����ڴ洢����Ͷ������Ľ��������Ͷ����һ�ֳ��õļ�����
        //���ڼ�ⳡ���е���ײ����ȡ��ײ�㡢��ȡ��ײ�������Ϣ�ȡ�RaycastHit �ṩ�˹��������볡���ж���Ľ�����Ϣ��������ײ�㡢��ײ���ߡ���ײ�����
        return raycastHit.point;
    }

    private void SelectedUIObj(UIAppearSet uIAppear)
    {
        if (slectedUI != null)
        {

        }
        slectedUI = uIAppear;

    }

    private void DeselectUIObj()
    {
        if (slectedUI != null)
        {
            slectedUI = null;
        }
    }

    private void UIAppearLogic()
    {
        // �������Ļ����ת��ΪCanvas��������
        Vector2 mouseScreenPos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIAppearController.instance.canvasRect, mouseScreenPos, null, out Vector2 localPoint);
        //RectTransformUtility�ṩ��һϵ�����ڴ��� RectTransform �ľ�̬���� https://www.jianshu.com/p/185cbe4ee981
        //RectTransformUtility.ScreenPointToLocalPointInRectangle �� Unity ���������ڰ���Ļ�ռ�ĵ�ת��Ϊ RectTransform �ֲ��ռ�ĵ��ʵ�÷���
        //rect��Ŀ�� RectTransform��Ҳ����Ҫ����Ļ��ת������ֲ��ռ�ľ��α任�����screenPoint����Ļ�ռ�ĵ㣬ͨ�������λ�û��ߴ���λ�á�
        //cam������ת����������� UI ����Ļ�ռ� - ����ģʽ���ɴ��� null��localPoint���������������ת����ľֲ��ռ�㡣
        Vector2 offset = new Vector2(Xoffset, Yoffset);
        Vector2 targetPos = localPoint + offset;

        Vector2 clampedPosition = ClampPositionToScreen(targetPos);
        UIAppearController.instance.hoverImage.anchoredPosition = clampedPosition;
        //RectTransform.anchoredPosition �� Unity ���������ڴ��� UI ���ֵ���Ҫ���ԣ����� RectTransform ���������ء�
        //RectTransform �� Unity �����ڿ��� UI Ԫ�ز��ֺ�λ�õ�������� anchoredPosition ������ָ�� UI Ԫ���������ê�㣨Anchors����λ��
        UIAppearController.instance.hoverImage.gameObject.SetActive(true);
        UIAppearController.instance.nameText.text = slectedUI.objName;
        UIAppearController.instance.introText.text = slectedUI.introduceInf;
        StartCoroutine(FadeImage(0f, 1f));
    }

    private Vector2 ClampPositionToScreen(Vector2 targetPosition)
        //aiд�ķ�ui������Ļ�ķ��� ��������ʵ��ʵ��Ҳ���Ǻܺ� ��������
    {
        RectTransform canvasRect = UIAppearController.instance.canvasRect;
        RectTransform imageRect = UIAppearController.instance.hoverImage;

        // ��ȡCanvas�ĳߴ������
        Vector2 canvasSize = canvasRect.rect.size;
        Vector3 canvasScale = canvasRect.localScale;

        // ����UIͼ���ʵ�ʳߴ磨�������ţ�
        float imageWidth = imageRect.rect.width * canvasScale.x;
        float imageHeight = imageRect.rect.height * canvasScale.y;

        // ����Canvas�ı߽緶Χ�����ڱ�������ϵ��
        float canvasMinX = -canvasSize.x / 2 + imageWidth / 2;
        float canvasMaxX = canvasSize.x / 2 - imageWidth / 2;
        float canvasMinY = -canvasSize.y / 2 + imageHeight / 2;
        float canvasMaxY = canvasSize.y / 2 - imageHeight / 2;

        // ����λ����Canvas�߽���
        float clampedX = Mathf.Clamp(targetPosition.x, canvasMinX, canvasMaxX);
        float clampedY = Mathf.Clamp(targetPosition.y, canvasMinY, canvasMaxY);

        return new Vector2(clampedX, clampedY);
    }

    public void SetAlpha(float alpha)
    {
        Color newColor = UIimage.color;
        newColor.a = alpha; // ��͸���� Ϊ0ʱ����ʧ��
        UIimage.color = newColor;

        foreach (TextMeshProUGUI text in UIAppearController.instance.textMeshPros)
        //��������textMeshPros���趨͸����
        {
            Color textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;
        }
    }

    IEnumerator FadeImage(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        Color initialColor = UIimage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            //���Բ�ֵ����ʵ��ƽ������
            SetAlpha(newAlpha);
            yield return null;
        }

        // ȷ������ֵ׼ȷ
        SetAlpha(targetAlpha);
    }



}
