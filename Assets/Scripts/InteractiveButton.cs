using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractiveButton : MonoBehaviour
{
    [Header("References")]
    public GameObject infoCanvas;          // UI���
    public ParticleSystem particles;       // ����ϵͳ
    public AudioSource buttonSound;        // ��Ч

    [Header("Button Animation")]
    public float pressDepth = 0.5f;       // ���µ����
    public float pressSpeed = 10f;         // �����ٶ�

    private Vector3 originalPosition;      // ��ť��ʼλ��
    private bool isPressed = false;        // �Ƿ񱻰���
    private bool uiVisible = false;        // UI�Ƿ�ɼ�

    void Start()
    {
        // ���水ť��ʼλ��
        originalPosition = transform.localPosition;

        // ��ȡXR Simple Interactable���
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

        if (interactable != null)
        {
            // ����������ͣ�ڰ�ť��ʱ����
            interactable.hoverEntered.AddListener(OnHoverEnter);
            // ��������ѡ�񣨰��£���ťʱ����
            interactable.selectEntered.AddListener(OnPress);
        }
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        // ��ѡ����������ͣʱ�ķ���
        Debug.Log("Button hovered!");
    }

    void OnPress(SelectEnterEventArgs args)
    {
        // ��ť������
        if (!isPressed)
        {
            isPressed = true;
            PressButton();
        }
    }

    void PressButton()
    {
        Debug.Log("Button pressed!");

        // �л�UI��ʾ
        uiVisible = !uiVisible;
        if (infoCanvas != null)
        {
            infoCanvas.SetActive(uiVisible);
        }

        // ��������Ч��
        if (particles != null)
        {
            particles.Play();
        }

        // ������Ч
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        // ���ð���״̬���ӳ٣�
        Invoke("ResetButton", 0.3f);
    }

    void ResetButton()
    {
        isPressed = false;
    }

    void Update()
    {
        // ��ť���¶���
        if (isPressed)
        {
            // �����ƶ�
            Vector3 targetPos = originalPosition - new Vector3(0, pressDepth, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * pressSpeed);
        }
        else
        {
            // �ָ�ԭλ
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * pressSpeed);
        }
    }
}