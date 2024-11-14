using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] ClassroomController classroomController;

    [SerializeField] GameObject lectureListPanel;
    [SerializeField] GameObject lectureInfoPanel;

    [SerializeField] LectureSlot lectureSlotPrefab;
    [SerializeField] GameObject lectureSlotLinePrefab;
    [SerializeField] Transform lectureListParent;


    [SerializeField] Text lectureNameText;
    [SerializeField] Text representativeNameText;
    [SerializeField] Text timeInfoText;
    [SerializeField] Text maxStudentCntText;
    [SerializeField] Text attendanceStatusText;

    [SerializeField] Text nowDateTimeText;
    [SerializeField] Image imageQrCode;
    [SerializeField] Text lessQrCodeTimeText;

    [SerializeField] GameObject rentalInfo;
    [SerializeField] GameObject lectureInfo;
    [SerializeField] GameObject emptyInfo;

    private void Awake()
    {
        lectureListPanel.SetActive(true);
        lectureInfoPanel.SetActive(false);
    }

    public void ShowLectureInfoPanel(Lecture focusLecture)
    {
        lectureListPanel.SetActive(false);
        lectureInfoPanel.SetActive(true);

        lectureNameText.text = focusLecture.GetLectureName();
        representativeNameText.text = focusLecture.GetRepresentativeName();

        DateTime startTime = focusLecture.GetStartTime();
        DateTime endTime = focusLecture.GetEndTime();
        timeInfoText.text = focusLecture.GetStartHour() + ":" + focusLecture.GetStartMinute() + " - " + focusLecture.GetEndHour() + ":" + focusLecture.GetEndMinute();
        maxStudentCntText.text = focusLecture.GetMaxStudentCnt().ToString() + "명";
        
    }

    public void SetNowClassroomStatus(CLASSROOM_STATUS status)
    {
        if (status == CLASSROOM_STATUS.EMPTY)
        {
            emptyInfo.SetActive(true);
            lectureInfo.SetActive(false);
            rentalInfo.SetActive(false);
        }
        else if (status == CLASSROOM_STATUS.LECTURE)
        {
            emptyInfo.SetActive(false);
            lectureInfo.SetActive(true);
            rentalInfo.SetActive(false);
        }
        else if (status == CLASSROOM_STATUS.RENTAL)
        {
            emptyInfo.SetActive(false);
            lectureInfo.SetActive(false);
            rentalInfo.SetActive(true);
        }
    }

    public void SetQRCodeImage(Sprite qrSprite)
    {
        imageQrCode.sprite = qrSprite;
    }

    public void SetNowDateTime(TimeSpan ts)
    {
        nowDateTimeText.text = ts.ToString(@"hh\:mm\:ss");
    }

    public void SetLessQRCodeTimeText(int lessTime)
    {
        lessQrCodeTimeText.text = lessTime.ToString();
    }

    public void SetAttendanceStatus(ATTENDANCE_STATUS status)
    {
        string str = ATTENDANCE_STATUS.ATTENDANCE == status ? "출석" : "지각";
        attendanceStatusText.text = str;
    }

    public void CreateLectureSlots(List<Lecture> lectureList)
    {
        if (lectureList.Count <= 0)
            return;

        for(int i = 0;i<lectureList.Count;++i)
        {
            bool isFirst = i == 0;

            LectureSlot slot = Instantiate<LectureSlot>(lectureSlotPrefab);
            slot.Init(lectureList[i], isFirst);
            slot.transform.SetParent(lectureListParent);

            if(isFirst)
            {
                GameObject line = Instantiate(lectureSlotLinePrefab);
                line.transform.SetParent(lectureListParent);
            }
        }
    }
}
