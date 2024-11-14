using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IconicThon.Network;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] GrpcClient grpcClient;
    [SerializeField] QRCodeReadController qrCodeReadController;

    [SerializeField] LectureSlot lectureSlotPrefab;
    [SerializeField] Transform lectureSlotParent;

    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject classroomPanel;

    [SerializeField] Text readDataText;

    [SerializeField] GameObject attendanceResultPopup;
    [SerializeField] GameObject attendanceStatusSuccessObject;
    [SerializeField] GameObject attendanceStatusFailObject;

    [SerializeField] GameObject qrCodeOnBtnTab;
    [SerializeField] GameObject attendanceNotInitTab;
    [SerializeField] GameObject attendanceSuccessTab;
    [SerializeField] GameObject attendanceLateTab;
    [SerializeField] GameObject absentTab;

    [SerializeField] Text lectureNameText;
    [SerializeField] Text lectureName_2Text;
    [SerializeField] Text lectureName_3Text;
    [SerializeField] Text representiveNameText;
    [SerializeField] Text studentIdText;

    string nowLectureCode;

    Dictionary<string, string> dicLectureName = new Dictionary<string, string>();

    private void Start()
    {
        Screen.fullScreen = false;

        List<string> lectureCodeList = new List<string>();

        lectureCodeList.Add("ABC-1234");
        lectureCodeList.Add("ABC-5678");
        lectureCodeList.Add("ABC-9012");

        dicLectureName.Add("ABC-1234", "인공지능과 미래사회");
        dicLectureName.Add("ABC-5678", "심화 프로그래밍");
        dicLectureName.Add("ABC-9012", "프로그래밍언어론");

        CreateLectureSlot(lectureCodeList);
        
    }

    public void SetQRData(string qrData)
    {
        readDataText.text = qrData;
    }

    public void ShowAttendancePopup(string attendance)
    {
        if(attendance == "SUCCESS")
        {
            attendanceStatusSuccessObject.SetActive(true);
            attendanceStatusFailObject.SetActive(false);
        }
        else
        {
            attendanceStatusSuccessObject.SetActive(false);
            attendanceStatusFailObject.SetActive(true);
        }
        attendanceResultPopup.SetActive(true);
    }

    public void OnClickAttendanceResultOkBtn()
    {
        attendanceResultPopup.SetActive(false);
        RefreshClassroomPanel(nowLectureCode);
    }

    public void CreateLectureSlot(List<string> lectureNameList)
    {
        for(int i = 0; i < lectureNameList.Count; ++i)
        {
            LectureSlot slot = Instantiate(lectureSlotPrefab);
            slot.Init(lectureNameList[i], dicLectureName[lectureNameList[i]], OnClickLecture);
            slot.transform.parent = lectureSlotParent;
        }
    }

    void OnClickLecture(string lectureCode)
    {
        qrCodeReadController.SetLectureCode(lectureCode);
        RefreshClassroomPanel(lectureCode);
        nowLectureCode = lectureCode;
    }

    void RefreshClassroomPanel(string lectureCode)
    {
        qrCodeOnBtnTab.SetActive(false);
        attendanceNotInitTab.SetActive(false);
        attendanceSuccessTab.SetActive(false);
        attendanceLateTab.SetActive(false);
        absentTab.SetActive(false);

        StudentAttendanceInfo attendanceInfo = new StudentAttendanceInfo();
        try
        {
            attendanceInfo =
                grpcClient.RefreshLectureInfo("2019112549", lectureCode);

            DebugTool.myLog += "\n" + attendanceInfo.AttendanceStatus;

            if (attendanceInfo == null)
                return;
        }
        catch(Exception e)
        {
            Debug.Log(e);
            DebugTool.myLog += "\n" + e;
            mainPanel.SetActive(true);
        }

        mainPanel.SetActive(false);
        classroomPanel.SetActive(true);

        studentIdText.text = attendanceInfo.Id;
        lectureNameText.text = attendanceInfo.LectureName;
        lectureName_2Text.text = attendanceInfo.LectureName;
        lectureName_3Text.text = attendanceInfo.LectureName;
        representiveNameText.text = "담당 교수 : 이채린";

        Debug.Log(attendanceInfo.AttendanceStatus);

        if (attendanceInfo.AttendanceStatus == "READY")
        {
            qrCodeOnBtnTab.SetActive(true);
        }
        else if (attendanceInfo.AttendanceStatus == "NOT_INIT")
        {
            attendanceNotInitTab.SetActive(true);
        }
        else if (attendanceInfo.AttendanceStatus == "SUCCESS")
        {
            attendanceSuccessTab.SetActive(true);
        }
        else if (attendanceInfo.AttendanceStatus == "LATE")
        {
            attendanceLateTab.SetActive(true);
        }
        else if (attendanceInfo.AttendanceStatus == "ABSENT")
        {
            absentTab.SetActive(true);
        }
    }
}
