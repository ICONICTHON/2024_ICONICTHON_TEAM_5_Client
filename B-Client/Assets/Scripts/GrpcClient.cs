using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using IconicThon.Network;

public class GrpcClient : MonoBehaviour
{
    const string ip = "59.6.87.115" + ":";

    //const string ip = "localhost" + ":";
    const int port = 7777;

    static Channel channel;
    static Attendance.AttendanceClient client;

    string message = "test message iconicthon";


    void Start()
    {
        channel = new Channel(ip + port, ChannelCredentials.Insecure);

        client = new Attendance.AttendanceClient(channel);

        if(client == null)
        {
            Debug.Log("client operation failed");
        }
        else
        {
            Debug.Log("client operation");
        }
    }

    public string GetRefreshPrivateKey(Lecture lectureData, ATTENDANCE_STATUS attendanceStatus)
    {
        if(client == null)
        {
            Debug.Log("client is not operation");
            return "";
        }

        string attendanceStatusStr = attendanceStatus == ATTENDANCE_STATUS.ATTENDANCE ? "SUCCESS" : "LATE";
        PrivateKey privateKeyCallback = client.RefreshPrivateKey(new LectureInfo { LectureCode = lectureData.GetLectureCode(), AttendStatus = attendanceStatusStr });

        Debug.Log("callback key = " + privateKeyCallback.Key);

        return privateKeyCallback.Key;
    }
}
