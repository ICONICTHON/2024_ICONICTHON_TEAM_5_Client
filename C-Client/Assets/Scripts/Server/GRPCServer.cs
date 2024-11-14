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

    private void Start()
    {
        server = new Server
        {
            Services = { Attendance.BindService(new AttendanceGRPC()) },
            Ports = { new ServerPort(ip, port, ServerCredentials.Insecure) }
        };
        server.Start();
        Debug.Log("Server Operation");
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
        string json = JsonUtilityExtention.ToJson(new List < JsonFileDataFrame >());
        // Debug.Log("created key = " + privateKey);


        return Task.FromResult(new JsonResponse { Json = json });
    }
}

public class RandomPrivateKeyCreator
{
    public static string GetRandomKey(string lectureCode, string attendStatus)
    {
        DateTime nowDateTime = DateTime.Now;
        string dateTimeStr = nowDateTime.ToString();

        string ret = dateTimeStr + lectureCode + attendStatus;

        SHA1 sha = SHA1.Create();
        byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(ret));
        StringBuilder returnValue = new StringBuilder();

        for (int i = 0;i<hashData.Length;++i)
        {
            returnValue.Append(hashData[i].ToString());
        }

        DebugTool.myLog += "created key = " + returnValue.ToString() + "\n";
        Debug.Log("created key = " + returnValue.ToString());

        return returnValue.ToString();
    }
}