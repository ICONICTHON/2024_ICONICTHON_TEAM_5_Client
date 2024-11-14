using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ATTENDANCE_STATUS
{
    ATTENDANCE,
    LATE
}

public class ClassroomController : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] GrpcClient grpcClient;
    [SerializeField] QRCodeController qrCodeController;
    string classroomCode; // 강의실 식별번호

    List<Lecture> lectureList = new List<Lecture>(); // 강의 리스트

    Lecture focusLecture;

    DateTime nowDateTime;
    float elapsedTime;
    const float lectureRefreshTime = 60.0f;
    const float qrCodeRefreshTime = 10.0f;

    string prevAttendanceCode;
    string attendanceCode;

    private void Start()
    {
        focusLecture = null;

        // 임시 강의, 대관 생성

        {
            LectureTime tempLectureTime = new LectureTime(12, 0, 90);
            List<LectureTime> lectureTimes = new List<LectureTime>();
            lectureTimes.Add(tempLectureTime);
            Lecture tempLecture = new Lecture("ABC-1234", lectureTimes, "인공지능과 미래사회", "장현진", 30);

            lectureList.Add(tempLecture);
        }
        {
            LectureTime tempLectureTime = new LectureTime(14, 30, 120);
            List<LectureTime> lectureTimes = new List<LectureTime>();
            lectureTimes.Add(tempLectureTime);
            Lecture tempLecture = new Lecture("ABC-5678", lectureTimes, "심화 프로그래밍", "장현진", 25);

            lectureList.Add(tempLecture);
        }
        {
            LectureTime tempLectureTime = new LectureTime(17, 0, 180);
            List<LectureTime> lectureTimes = new List<LectureTime>();
            lectureTimes.Add(tempLectureTime);
            Lecture tempLecture = new Lecture("ABC-9012", lectureTimes, "프로그래밍언어론", "장현진", 50);

            lectureList.Add(tempLecture);
        }

        uiController.CreateLectureSlots(lectureList);
    }

    private void Update()
    {
        nowDateTime = DateTime.Now;
        nowDateTime = new DateTime(2024, 11, 15, 10, 55,0); //
        Debug.Log(nowDateTime);
        qrCodeController.SetNowDateTimeText(nowDateTime);

        if (focusLecture == null)
        {
            // 포커싱 할 강의 찾기


            SetEmptyRoom();

            for (int i = 0; i < lectureList.Count; ++i)
            {
                Debug.Log(lectureList[i].IsFocusLecture(nowDateTime));
                if (lectureList[i].IsFocusLecture(nowDateTime))
                {
                    focusLecture = lectureList[i];
                    CreateRandomAttendanceCode(ATTENDANCE_STATUS.ATTENDANCE);
                    elapsedTime = 0;
                    qrCodeController.SetLectureData(focusLecture);
                    Debug.Log("Focus lecture");
                    uiController.ShowLectureInfoPanel(focusLecture);
                    return;
                }
            }
        }
        else
        {
            // 현재 강의 관련 데이터 갱신

            elapsedTime += Time.smoothDeltaTime;
            uiController.SetLessQRCodeTimeText((int)(qrCodeRefreshTime - elapsedTime));

            if (elapsedTime >= qrCodeRefreshTime)
            {
                elapsedTime -= qrCodeRefreshTime;

                if(focusLecture.IsFocusLecture(nowDateTime) == false)
                {
                    focusLecture = null;
                    return;
                }

                ATTENDANCE_STATUS status = ATTENDANCE_STATUS.LATE;

                if(focusLecture.CheckAttendanceTime(nowDateTime))
                {
                    status = ATTENDANCE_STATUS.ATTENDANCE;
                }

                CreateRandomAttendanceCode(status);
            }


        }
    }

    void CreateRandomAttendanceCode(ATTENDANCE_STATUS status)
    {
        uiController.SetAttendanceStatus(status);
        prevAttendanceCode = attendanceCode;

        string randomCode = grpcClient.GetRefreshPrivateKey(focusLecture, status);

        attendanceCode = randomCode;

        Debug.Log("get code : " + attendanceCode);

        SetQrCodeImage(attendanceCode);
    }

    void SetQrCodeImage(string data = "")
    {
        if (data == "")
        {

            return;
        }

        qrCodeController.SetQRCodeImage(data);
    }

    void SetEmptyRoom()
    {
        qrCodeController.SetLectureData(null);
    }

    public bool IsFocusLecture(Lecture lecture)
    {
        return focusLecture == lecture;
    }
}

public class Lecture
{
    string lectureCode; // 학수번호
    string lectureName; // 강의명 (대관명)
    string representativeName; // 강사명 (대관자명)
    int attendeesCnt; // 수강인원
    List<LectureTime> lectureTimeList;
    bool isRental;

    public Lecture(string lectCode, List<LectureTime> lectureTimes, string lectName, string representName, int attendCnt, bool rental = false)
    {
        lectureCode = lectCode;
        lectureTimeList = lectureTimes;
        lectureName = lectName;
        representativeName = representName;
        attendeesCnt = attendCnt;

        isRental = rental;

        if(isRental == true)
        {
            attendeesCnt = 0;
        }
    }

    public bool IsFocusLecture(DateTime nowDateTime)
    {
        for (int i = 0; i < lectureTimeList.Count; ++i)
        {
            if (nowDateTime.AddMinutes(15) >= lectureTimeList[i].GetStartTime() && 
                nowDateTime <= lectureTimeList[i].GetLateRecognitionTime())
            {
                return true;
            }
        }

        return false;


    }

    public bool CheckAttendanceTime(DateTime nowDateTime)
    {
        for(int i = 0;i<lectureTimeList.Count; ++i)
        {
            if (nowDateTime < lectureTimeList[i].GetStartTime())
            {
                return true;
            }
        }

        return false;
    }

    public string GetLectureCode()
    {
        return lectureCode;
    }

    public string GetLectureName()
    {
        return lectureName;
    }

    public string GetRepresentativeName()
    {
        return representativeName;
    }

    public DateTime GetStartTime()
    {
        if (lectureTimeList.Count <= 0)
            return DateTime.Now;
        return lectureTimeList[0].GetStartTime();
    }

    public DateTime GetEndTime()
    {
        if (lectureTimeList.Count <= 0)
            return DateTime.Now;
        return lectureTimeList[0].GetEndTime();
    }

    public int GetMaxStudentCnt()
    {
        return attendeesCnt;
    }

    public string GetStartHour()
    {
        if (lectureTimeList.Count <= 0)
            return "00";

        if (lectureTimeList[0].GetStartTime().Hour < 10)
            return "0" + lectureTimeList[0].GetStartTime().Hour.ToString();
        else
            return lectureTimeList[0].GetStartTime().Hour.ToString();
    }

    public string GetStartMinute()
    {
        if (lectureTimeList.Count <= 0)
            return "00";

        if (lectureTimeList[0].GetStartTime().Minute < 10)
            return "0" + lectureTimeList[0].GetStartTime().Minute.ToString();
        else
            return lectureTimeList[0].GetStartTime().Minute.ToString();
    }

    public string GetEndHour()
    {
        if (lectureTimeList.Count <= 0)
            return "00";

        if (lectureTimeList[0].GetEndTime().Hour < 10)
            return "0" + lectureTimeList[0].GetEndTime().Hour.ToString();
        else
            return lectureTimeList[0].GetEndTime().Hour.ToString();
    }

    public string GetEndMinute()
    {
        if (lectureTimeList.Count <= 0)
            return "00";

        if (lectureTimeList[0].GetEndTime().Minute < 10)
            return "0" + lectureTimeList[0].GetEndTime().Minute.ToString();
        else
            return lectureTimeList[0].GetEndTime().Minute.ToString();
    }

    public bool IsRental()
    {
        return isRental;
    }
}

public class LectureTime
{
    DateTime startTime;
    DateTime endTime;
    int lateRecognitionTime;

    public LectureTime(int sHour, int sMinute, int durationMinute)
    {
        DateTime nowDateTime = DateTime.Now;

        startTime = new DateTime(nowDateTime.Year, nowDateTime.Month, nowDateTime.Day, sHour, sMinute, 0);

        endTime = startTime.AddMinutes(durationMinute);

        lateRecognitionTime = (int)(durationMinute * 0.33f);
    }

    public DateTime GetStartTime()
    {
        return startTime;
    }

    public DateTime GetEndTime()
    {
        return endTime;
    }

    public DateTime GetLateRecognitionTime()
    {
        Debug.Log(startTime.AddMinutes(lateRecognitionTime));
        return startTime.AddMinutes(lateRecognitionTime);
    }
}