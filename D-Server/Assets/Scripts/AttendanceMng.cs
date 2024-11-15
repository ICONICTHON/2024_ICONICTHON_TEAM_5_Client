using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStudentInfo
{
    public static Dictionary<string, Dictionary<string, string>> dicLectureAttendStudentIds;

    public static void Init()
    {
        dicLectureAttendStudentIds = new Dictionary<string, Dictionary<string, string>>();

        dicLectureAttendStudentIds.Add("ABC-1234", new Dictionary<string, string>());
        dicLectureAttendStudentIds.Add("ABC-5678", new Dictionary<string, string>());
        dicLectureAttendStudentIds.Add("ABC-9012", new Dictionary<string, string>());
    }

    public static bool ExistStudentAttendInfo(string lectureCode, string studentId)
    {


        string outStr = "";
        bool check = dicLectureAttendStudentIds[lectureCode].TryGetValue(studentId, out outStr);
        Debug.Log(check);
        if (check)
        {
            return true;
        }

        return false;
    }

    public static string GetLecturesAttendanceInfo(string lectureCode, string getKey)
    {
        string ret = "ERROR";
        bool compareKey = RandomPrivateKeyCreator.CheckLectureKey(lectureCode, getKey);

        if (compareKey)
        {
            return RandomPrivateKeyCreator.GetLecturesAttendanceStatus(lectureCode);
        }


        return ret;
    }

    public static void UpdateStudentAttendInfo(string lectureCode, string studentId, string attendanceStatus)
    {
        bool tryResult = dicLectureAttendStudentIds[lectureCode].TryAdd(studentId, attendanceStatus);
        Debug.Log(tryResult);
        if (tryResult == false)
        {
            dicLectureAttendStudentIds[lectureCode][studentId] = attendanceStatus;
        }
        Debug.Log(dicLectureAttendStudentIds[lectureCode][studentId]);
    }

    public static string GetStudentAttendStatus(string lectureCode, string studentId)
    {
        string ret = "";
        ret = dicLectureAttendStudentIds[lectureCode][studentId];

        return ret;
    }
}