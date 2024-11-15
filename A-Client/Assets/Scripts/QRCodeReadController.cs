using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using ZXing;

public class QRCodeReadController : MonoBehaviour
{
    [SerializeField] GrpcClient grpcClient;
    [SerializeField] UIController uiController;

    [SerializeField] GameObject cameraUiObject;
    [SerializeField] RawImage cameraTextureRawImage;

    Vector2 requestedRatio;

    WebCamTexture webCamTexture;
    Rect screenRect;

    string qrCodeReadStr;
    string lectureCode;

    int Width;
    int Height;

    private void Start()
    {

        Width = Screen.width;
        Height = Screen.height;

        requestedRatio = new Vector2(Screen.width, Screen.height);
        qrCodeReadStr = "";

        if (webCamTexture) //�̹� WebCamTexture�� �����ϴ� ��쿡��
        {
            Destroy(webCamTexture); //�����Ѵ� (�޸� ����)
            webCamTexture = null;
        }

        WebCamDevice[] webCamDevices = WebCamTexture.devices; //���� ������ �� �ִ� ī�޶� ������ ��� �����´�
        if (webCamDevices.Length == 0) return; //����, ī�޶� ���ٸ� ����

        int backCamIndex = -1; //�Ĺ� ī�޶� �����ϱ� ���� ����
        for (int i = 0, l = webCamDevices.Length; i < l; ++i) //ī�޶� Ž���ϸ鼭
        {
            if (!webCamDevices[i].isFrontFacing) //�Ĺ� ī�޶� �߰��ϸ�
            {
                backCamIndex = i; //�ε��� ����
                break; //�ݺ��� ����������
            }
        }

        if (backCamIndex != -1) //�Ĺ� ī�޶� �߰�������
        {
            int requestedWidth = Width; //�����ϰ��� �ϴ� ���� �ȼ� ���� ���� (���� ȭ���� ���� �ȼ��� �⺻������ ����)
            int requestedHeight = Height; //�����ϰ��� �ϴ� ���� �ȼ� ���� ���� (���� ȭ���� ���� �ȼ��� �⺻������ ����)
            for (int i = 0, l = webCamDevices[backCamIndex].availableResolutions.Length; i < l; ++i) //���� ���õ� �Ĺ� ī�޶� Ȱ���� �� �ִ� �ػ󵵸� Ž���ϸ鼭
            {
                Resolution resolution = webCamDevices[backCamIndex].availableResolutions[i];
                if (GetAspectRatio((int)requestedRatio.x, (int)requestedRatio.y).Equals(GetAspectRatio(resolution.width, resolution.height))) //�����ϰ��� �ϴ� ������ ��ġ�ϴ� �ػ󵵸� �߰��ϸ�
                {
                    requestedWidth = resolution.width; //�����ϰ��� �ϴ� ���� �ȼ��� ����
                    requestedHeight = resolution.height; //�����ϰ��� �ϴ� ���� �ȼ��� ����
                    break; //�ݺ��� ����������
                }
            }

            webCamTexture = new WebCamTexture(webCamDevices[backCamIndex].name, requestedWidth, requestedHeight, 60); //ī�޶� �̸����� WebCamTexture ����
            webCamTexture.filterMode = FilterMode.Trilinear;
            
        }

        if(webCamTexture == null)
        {
            screenRect = new Rect(0, 0, Screen.width, Screen.height);

            webCamTexture = new WebCamTexture();

            webCamTexture.requestedHeight = Height;

            webCamTexture.requestedWidth = Width;
        }

        webCamTexture.requestedFPS = 60;

        cameraTextureRawImage.texture = webCamTexture;
        //cameraTextureRawImage.material.mainTexture = webCamTexture;
    }

    public void SetLectureCode(string code)
    {
        lectureCode = code;
    }

    private string GetAspectRatio(int width, int height, bool allowPortrait = false) //������ ��ȯ�ϴ� �Լ�
    {
        if (!allowPortrait && width < height) Swap(ref width, ref height); //���ΰ� ������ �ʴµ�, (���� < ����)�̸� ������ ��ȯ
        float r = (float)width / height; //���� ����
        return r.ToString("F2"); //�Ҽ��� ��°���� �߶� ���ڿ��� ��ȯ
    }
    private void Swap<T>(ref T a, ref T b) //�� �������� ��ȯ�ϴ� �Լ�
    {
        T tmp = a;
        a = b;
        b = tmp;
    }

    private void Update()
    {

        if (webCamTexture == null || webCamTexture.isPlaying == false)
            return;

        UpdateWebCamRawImage();

        QRCodeDataUpdate();
    }

    private void UpdateWebCamRawImage() //RawImage�� �����ϴ� �Լ�
    {
        if (!webCamTexture) return; //WebCamTexture�� �������� ������ ����

        int videoRotAngle = webCamTexture.videoRotationAngle;
        cameraTextureRawImage.transform.localEulerAngles = new Vector3(0, 0, -videoRotAngle); //ī�޶� ȸ�� ������ �ݿ�

        int width, height;
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) //���� ȭ���̸�
        {
            width = Width; //���θ� �����ϰ�
            height = Width * webCamTexture.width / webCamTexture.height; //WebCamTexture�� ������ ���� ���θ� ����

            // Debug.Log("portrait : height - " + height); 
        }
        else //���� ȭ���̸�
        {
            //Debug.Log("Landspace");
            height = Height; //���θ� �����ϰ�
            width = Height * webCamTexture.width / webCamTexture.height; //WebCamTexture�� ������ ���� ���θ� ����
        }

        if (Mathf.Abs(videoRotAngle) % 180 != 0f) Swap(ref width, ref height); //WebCamTexture ��ü�� ȸ���Ǿ��ִ� ��� ����/���� ���� ��ȯ
        cameraTextureRawImage.rectTransform.sizeDelta = new Vector2(width, height); //RawImage�� size�� ����
    }

    void QRCodeDataUpdate()
    {
        IBarcodeReader barcodeReader = new BarcodeReader();

        var result = barcodeReader.Decode(webCamTexture.GetPixels32(), webCamTexture.width, webCamTexture.height);

        if (result == null)
            return;

        Debug.Log("Read Data : " + result.Text);
        qrCodeReadStr = result.Text;
        uiController.SetQRData(qrCodeReadStr);

        string attendance = grpcClient.SendAttendanceInfo("201911254911", qrCodeReadStr, lectureCode);
        if(attendance != "")
        {
            webCamTexture.Stop();
            cameraUiObject.SetActive(false);
            uiController.ShowAttendancePopup(attendance);
        }

    }

    public void OnClickOpenQRCamera()
    {
        if (webCamTexture == null)
            return;


        cameraUiObject.SetActive(true);
        webCamTexture.Play();
    }
}
