using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIAppearController : MonoBehaviour
{
    //单例模式我爱你
    public static UIAppearController instance;
    //声明了本脚本类的实例instance
    public RectTransform canvasRect; // 拖入Canvas的RectTransform
    public RectTransform hoverImage; // 拖入需要显示的UI图像

    [Header("Text信息")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI introText;
    public List<TextMeshProUGUI> textMeshPros = new List<TextMeshProUGUI>();
    [Header("选中")]
    public UIAppearSet slectedUI;

    [SerializeField] private LayerMask appearLayer;
    [SerializeField] private float UIappearTime = 1f;//悬停显示时间
    [SerializeField] private Image UIimage;
    [SerializeField] private Camera mainCamera;
    private float UIappearCounter = 0f;//计时器
    public bool isChecked = false;

    [SerializeField] private float Xoffset;
    [SerializeField] private float Yoffset;
    public float fadeDuration = 1f; // 渐变持续时间

    private void Awake()
    {
        instance = this;
        //使instance实例实例化为本脚本
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
                if (!isChecked)//这个是保证这个函数只触发一次的
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
    //static声明函数内部变量和方法使属于这个类而不是类的实例，允许不在脚本中实例化也能用.符号来访问内部变量
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //找到tag为maincamera的camera组件
        //ScreenPointToRay() 是Unity中Camera类的一个方法，用于将屏幕上的一个点转换为一条射线。这条射线的起点是摄像机在屏幕上对应的点，
        //方向是从摄像机出发指向那个点。这在进行射线命中检测时非常有用，特别是与用户界面和鼠标交互相关的场景中。
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.appearLayer);//这个没实际用处，只是声明raycastHit，本身使检测鼠标射线与设置层之间的碰撞
        //Raycast函数第二个值使out形式，故此只能也写out，out会使值丢失，并重新赋值，也就是会改变
        //RaycastHit 类是 Unity 中的一个结构，用于存储射线投射操作的结果。射线投射是一种常用的技术，
        //用于检测场景中的碰撞、获取碰撞点、获取碰撞对象的信息等。RaycastHit 提供了关于射线与场景中对象的交互信息，包括碰撞点、碰撞法线、碰撞对象等
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
        // 将鼠标屏幕坐标转换为Canvas本地坐标
        Vector2 mouseScreenPos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIAppearController.instance.canvasRect, mouseScreenPos, null, out Vector2 localPoint);
        //RectTransformUtility提供了一系列用于处理 RectTransform 的静态方法 https://www.jianshu.com/p/185cbe4ee981
        //RectTransformUtility.ScreenPointToLocalPointInRectangle 是 Unity 引擎里用于把屏幕空间的点转换为 RectTransform 局部空间的点的实用方法
        //rect：目标 RectTransform，也就是要把屏幕点转换到其局部空间的矩形变换组件。screenPoint：屏幕空间的点，通常是鼠标位置或者触摸位置。
        //cam：用于转换的相机。若 UI 是屏幕空间 - 覆盖模式，可传入 null。localPoint：输出参数，代表转换后的局部空间点。
        Vector2 offset = new Vector2(Xoffset, Yoffset);
        Vector2 targetPos = localPoint + offset;

        Vector2 clampedPosition = ClampPositionToScreen(targetPos);
        UIAppearController.instance.hoverImage.anchoredPosition = clampedPosition;
        //RectTransform.anchoredPosition 是 Unity 引擎中用于处理 UI 布局的重要属性，它和 RectTransform 组件紧密相关。
        //RectTransform 是 Unity 里用于控制 UI 元素布局和位置的组件，而 anchoredPosition 则用于指定 UI 元素相对于其锚点（Anchors）的位置
        UIAppearController.instance.hoverImage.gameObject.SetActive(true);
        UIAppearController.instance.nameText.text = slectedUI.objName;
        UIAppearController.instance.introText.text = slectedUI.introduceInf;
        StartCoroutine(FadeImage(0f, 1f));
    }

    private Vector2 ClampPositionToScreen(Vector2 targetPosition)
        //ai写的防ui超出屏幕的方法 看不懂，实际实现也不是很好 但是能用
    {
        RectTransform canvasRect = UIAppearController.instance.canvasRect;
        RectTransform imageRect = UIAppearController.instance.hoverImage;

        // 获取Canvas的尺寸和缩放
        Vector2 canvasSize = canvasRect.rect.size;
        Vector3 canvasScale = canvasRect.localScale;

        // 计算UI图像的实际尺寸（考虑缩放）
        float imageWidth = imageRect.rect.width * canvasScale.x;
        float imageHeight = imageRect.rect.height * canvasScale.y;

        // 计算Canvas的边界范围（基于本地坐标系）
        float canvasMinX = -canvasSize.x / 2 + imageWidth / 2;
        float canvasMaxX = canvasSize.x / 2 - imageWidth / 2;
        float canvasMinY = -canvasSize.y / 2 + imageHeight / 2;
        float canvasMaxY = canvasSize.y / 2 - imageHeight / 2;

        // 限制位置在Canvas边界内
        float clampedX = Mathf.Clamp(targetPosition.x, canvasMinX, canvasMaxX);
        float clampedY = Mathf.Clamp(targetPosition.y, canvasMinY, canvasMaxY);

        return new Vector2(clampedX, clampedY);
    }

    public void SetAlpha(float alpha)
    {
        Color newColor = UIimage.color;
        newColor.a = alpha; // 改透明度 为0时就消失了
        UIimage.color = newColor;

        foreach (TextMeshProUGUI text in UIAppearController.instance.textMeshPros)
        //遍历两个textMeshPros并设定透明度
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
            //线性插值函数实现平滑渐变
            SetAlpha(newAlpha);
            yield return null;
        }

        // 确保最终值准确
        SetAlpha(targetAlpha);
    }



}
