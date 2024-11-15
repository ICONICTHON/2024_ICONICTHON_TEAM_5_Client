using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using IconicThon.Network;
using UnityEngine.UI;

public class GrpcClient : MonoBehaviour
{
    const string ip = "59.6.87.115" + ":";
    //const string ip = "localhost" + ":";
    const int port = 7777;

    static Channel channel;
    static Attendance.AttendanceClient client;

    [SerializeField] GameObject loadingCanvas;


    void Awake()
    {
        channel = new Channel(ip + port, ChannelCredentials.Insecure);

        client = new Attendance.AttendanceClient(channel);

        if (client == null)
        {
            Debug.Log("client operation failed");
        }
        else
        {
            Debug.Log("client operation");
        }
    }

    public string SendAttendanceInfo(string studentId, string privateKey, string lectureCode)
    {
        if (client == null)
        {
            Debug.Log("client is not operation");
            return "";
        }

        loadingCanvas.SetActive(true);

        ResponseAttendanceInfo attendanceInfo = client.SendAttendanceInfo(
            new RequestAttendanceInfo { Id = studentId, PrivateKey = privateKey, LectureCode = lectureCode });
        
        loadingCanvas.SetActive(false);

        return attendanceInfo.AttendanceStatus;
    }

    public StudentAttendanceInfo RefreshLectureInfo(string studentId, string lectureCode)
    {
        if (client == null)
        {
            Debug.Log("client is not operation");
            return new StudentAttendanceInfo();
        }

        loadingCanvas.SetActive(true);

        StudentAttendanceInfo attendanceInfo = client.RequestLectureInfo(
            new StudentInfo { Id = studentId, LectureCode = lectureCode });

        loadingCanvas.SetActive(false);

        return attendanceInfo;
    }

    //public string GetRefreshPrivateKey(Lecture lectureData, ATTENDANCE_STATUS attendanceStatus)
    //{
    //    if (client == null)
    //    {
    //        Debug.Log("client is not operation");
    //        return "";
    //    }

    //    string attendanceStatusStr = attendanceStatus == ATTENDANCE_STATUS.ATTENDANCE ? "A" : "L";
    //    PrivateKey privateKeyCallback = client.RefreshPrivateKey(new LectureInfo { LectureCode = lectureData.GetLectureCode(), AttendStatus = attendanceStatusStr });

    //    Debug.Log("callback key = " + privateKeyCallback.Key);

    //    return privateKeyCallback.Key;
    //}
}
