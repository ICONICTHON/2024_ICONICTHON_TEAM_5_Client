using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

[System.Serializable]
public class JsonFileDataFrame
{
    public int _lectureBuilding;
    public int _lectureNumber;
    public int _lectureStudent;
    public int _lectureCapacity;

    public override string ToString() => $"_lectureNumber: {_lectureNumber}, _lectureStudent: {_lectureStudent}, _lectureCapacity: {_lectureCapacity}";
}

public class JsonSendToLoad : MonoBehaviour
{
    public static List<JsonFileDataFrame> LectureData;

    public static string JSON_PATH = "/Scripts/Test.json";
    public static string LIST_JSON_PATH = "/Scripts/ListTest.json";

    [ContextMenu("To Json File")]
    public static void TestSaveList()
    {
        JsonUtilityExtention.FileSaveList(LectureData, LIST_JSON_PATH);
    }

    [ContextMenu("From Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public static List<JsonFileDataFrame> LectureDataLoad()
    {
        List<JsonFileDataFrame> datas = GrpcClient.GetLectureDataJson();
        string s = "Data List: \n";
        foreach (var data in datas)
        {
            s += data + "\n";
        }
        Debug.Log(s);
        return datas;
    }
}
