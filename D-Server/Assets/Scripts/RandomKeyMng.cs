using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class RandomPrivateKeyCreator
{
    static Dictionary<string, string> dicPrivateKey = new Dictionary<string, string>();
    static Dictionary<string, string> dicAttendanceStatus = new Dictionary<string, string>();

    public static string GetRandomKey(string lectureCode, string attendStatus)
    {
        DateTime nowDateTime = DateTime.Now;
        string dateTimeStr = nowDateTime.ToString();

        string ret = dateTimeStr + lectureCode + attendStatus;

        SHA1 sha = SHA1.Create();
        byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(ret));
        StringBuilder returnValue = new StringBuilder();

        for (int i = 0; i < hashData.Length; ++i)
        {
            returnValue.Append(hashData[i].ToString());
        }

        string key = returnValue.ToString();

        DebugTool.myLog += "created key = " + key + "\n";
        Debug.Log("created key = " + key);

        if (dicPrivateKey.TryAdd(lectureCode, key) == false)
        {
            dicPrivateKey[lectureCode] = key;
        }

        if (dicAttendanceStatus.TryAdd(lectureCode, attendStatus) == false)
        {
            dicAttendanceStatus[lectureCode] = attendStatus;
        }

        return key;
    }

    public static bool CheckLectureKey(string lectureCode, string getKey)
    {
        string haveKey = "";
        bool isGet = dicPrivateKey.TryGetValue(lectureCode, out haveKey);

        if (isGet == false)
            return false;

        if (getKey == haveKey)
        {
            return true;
        }

        return false;
    }

    public static string GetLecturesAttendanceStatus(string lectureCode)
    {
        string lecturesTimeStatus = ""; // 현재 시간에 대한 강의의 출석 상태
        bool isGet = dicAttendanceStatus.TryGetValue(lectureCode, out lecturesTimeStatus);

        if (isGet)
        {
            return lecturesTimeStatus;
        }
        else
        {
            return "ERROR";
        }
    }
}