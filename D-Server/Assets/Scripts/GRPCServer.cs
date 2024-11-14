using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Grpc.Core;
using IconicThon.Network;
using System;
using System.Security.Cryptography;
using System.Text;

public class GRPCServer : MonoBehaviour
{
    Server server;
    //public string ip = "59.6.87.115";
    const string ip = "localhost";
    const int port = 7777;

    private void Awake()
    {
        server = new Server
        {
            Services = { Attendance.BindService(new AttendanceGRPC()) },
            Ports = { new ServerPort(ip, port, ServerCredentials.Insecure) }
        };
        server.Start();
        Debug.Log("Server Operation");

        TempStudentInfo.Init();
    }


}

public class AttendanceGRPC : Attendance.AttendanceBase
{
    public override Task<PrivateKey> RefreshPrivateKey(LectureInfo lectureInfo, ServerCallContext context)
    {
        string privateKey = RandomPrivateKeyCreator.GetRandomKey(lectureInfo.LectureCode, lectureInfo.AttendStatus);

        // Debug.Log("created key = " + privateKey);


        return Task.FromResult(new PrivateKey { Key = privateKey });
    }

    public override Task<JsonResponse> RequestLectureList(JsonRequest lectureInfo, ServerCallContext context)
    {
        Debug.Log("LOG1");
        string json = JsonUtilityExtention.ToJson(DataSet.GetData());
        Debug.Log("created key = " + json);


        return Task.FromResult(new JsonResponse { Json = json });
    }

    public override Task<ResponseAttendanceInfo> SendAttendanceInfo(RequestAttendanceInfo attendanceInfo, ServerCallContext context)
    {
        string studentId = attendanceInfo.Id;
        string lectureCode = attendanceInfo.LectureCode;
        string privateKey = attendanceInfo.PrivateKey;
        Debug.Log(studentId);
        Debug.Log(lectureCode);
        Debug.Log(privateKey);

        string attendanceStatus = "ERROR";

        if (TempStudentInfo.ExistStudentAttendInfo(lectureCode, studentId) == false)
        {
            attendanceStatus = TempStudentInfo.GetLecturesAttendanceInfo(lectureCode, privateKey);
        }

        if (attendanceStatus != "ERROR")
        {
            TempStudentInfo.UpdateStudentAttendInfo(lectureCode, studentId, attendanceStatus);
        }

        string lectureName = "";

        if (lectureCode == "ABC-1234")
        {
            lectureName = "인공지능과 미래사회";
        }
        else if (lectureCode == "ABC-5678")
        {
            lectureName = "심화 프로그래밍";
        }
        else if (lectureCode == "ABC-9012")
        {
            lectureName = "프로그래밍언어론";
        }

        return Task.FromResult(new ResponseAttendanceInfo { AttendanceStatus = attendanceStatus, LectureName = lectureName }); // TempName
    }

    public override Task<StudentAttendanceInfo> RequestLectureInfo(StudentInfo studentInfo, ServerCallContext context)
    {
        string lectureCode = studentInfo.LectureCode;
        string studentId = studentInfo.Id;

        string attendStatus = "READY";

        Debug.Log(lectureCode);
        Debug.Log(studentId);

        Debug.Log("Log1");
        if (TempStudentInfo.ExistStudentAttendInfo(lectureCode, studentId))
        {
            Debug.Log("Log2");
            string status = TempStudentInfo.GetStudentAttendStatus(lectureCode, studentId);

            attendStatus = status;
        }
        else
        {

        }
        Debug.Log(attendStatus);

        string lectureName = "";

        if (lectureCode == "ABC-1234")
        {
            lectureName = "인공지능과 미래사회";
        }
        else if (lectureCode == "ABC-5678")
        {
            lectureName = "심화 프로그래밍";
        }
        else if (lectureCode == "ABC-9012")
        {
            lectureName = "프로그래밍언어론";
        }

        return Task.FromResult(new StudentAttendanceInfo { AttendanceStatus = attendStatus, Id = studentId, LectureName = lectureName });
    }
}