using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using System;

public enum CLASSROOM_STATUS
{
    EMPTY,
    LECTURE,
    RENTAL
}

public class QRCodeController : MonoBehaviour
{

    [SerializeField] ClassroomController classroomController;
    [SerializeField] UIController uiController;

    Texture2D curQRCodeImage;
    static int qrCodeWidth;
    static int qrCodeHeight;

    Lecture focusLecture;
    float elapsedTime = 0.0f;

    private void Awake()
    {
        qrCodeWidth = 256;
        qrCodeHeight = 256;

        SetClassRoomStatus(CLASSROOM_STATUS.EMPTY);
    }

    private void Update()
    {

    }

    public void SetLectureData(Lecture lecture)
    {
        if(lecture == null)
        {
            focusLecture = null;
            SetClassRoomStatus(CLASSROOM_STATUS.EMPTY);
            return;
        }
        else
        {
            focusLecture = lecture;

            if(lecture.IsRental())
            {
                SetClassRoomStatus(CLASSROOM_STATUS.RENTAL);
            }
            else
            {
                SetClassRoomStatus(CLASSROOM_STATUS.LECTURE);
            }
        }
    }

    public void SetQRCodeImage(string data)
    {
        curQRCodeImage = null;
        CreateQRTexture(data);

        if (curQRCodeImage == null)
            return;

        Sprite qrSprite = Sprite.Create(curQRCodeImage, new Rect(0, 0, qrCodeWidth, qrCodeHeight), new Vector2(0.5f, 0.5f));
        uiController.SetQRCodeImage(qrSprite);
    }

    void SetClassRoomStatus(CLASSROOM_STATUS status)
    {
        //Debug.Log(status);
        uiController.SetNowClassroomStatus(status);
    }

    void CreateQRTexture(string data)
    {
        Texture2D encoded = new Texture2D(qrCodeWidth, qrCodeHeight);
        Color32[] color32 = Encode(data);
        encoded.SetPixels32(color32);
        encoded.Apply();

        curQRCodeImage = encoded;

        SaveQrImage();
    }

    Color32[] Encode(string data)
    {
        // check code

        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,

            Options = new QrCodeEncodingOptions
            {
                Height = qrCodeHeight,
                Width = qrCodeWidth
            }
        };

        return writer.Write(data);
    }

    void SaveQrImage()
    {
        try
        {
            byte[] bytes = curQRCodeImage.EncodeToPNG();
            File.WriteAllBytes("C:/works/qr.png", bytes);
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    Texture2D GetCurrentQRcodeImageNullable()
    {
        if(curQRCodeImage != null)
        {
            return curQRCodeImage;
        }
        else
        {
            return null;
        }
    }

    public void SetNowDateTimeText(DateTime nowDateTime)
    {
        TimeSpan ts = new TimeSpan(nowDateTime.Hour, nowDateTime.Minute, nowDateTime.Second); ;
        uiController.SetNowDateTime(ts);
    }
}
