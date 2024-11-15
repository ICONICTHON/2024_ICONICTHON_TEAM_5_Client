using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

[System.Serializable]
public class JsonFileDataFrame
{
    public int LectureBuilding;
    public int LectureNumber;
    public int LectureStudent;
    public int LectureCapacity;
    public int LectureTemperature;
    public int LectureHumidity;

    public override string ToString() => $"LectureNumber: {LectureNumber}, LectureStudent: {LectureStudent}, LectureCapacity: {LectureCapacity} , LectureTemperature: {LectureTemperature} , LectureHumidity: {LectureHumidity}";
}

public class JsonStudentDataFrame
{
    public int StudentId;
    public int StudentChecking;
}

public class DataSet : MonoBehaviour
{
    public int RandomBatch;
    public int MaxBuilding;
    public string json;

    static List<JsonFileDataFrame> TempLectureList = new List<JsonFileDataFrame>();
    private Dictionary<string, string> _testAttendanceDic = new Dictionary<string, string>();
    private int _checkIndex;

    void Start()
    {
        CreateRandomDataFrame();
        _testAttendanceDic = TempStudentInfo.dicLectureAttendStudentIds["ABC-1234"];
        _checkIndex = _testAttendanceDic.Count;
        TempLectureList[0].LectureStudent = _checkIndex;
    }

    private void Update()
    {
        if(_checkIndex < TempStudentInfo.dicLectureAttendStudentIds["ABC-1234"].Count)
        {
            _checkIndex = TempStudentInfo.dicLectureAttendStudentIds["ABC-1234"].Count;
            TempLectureList[0].LectureStudent = _checkIndex;
        }
    }
    public static List<JsonFileDataFrame> GetData()
    {
        return TempLectureList;
    }

    private void CreateRandomDataFrame()
    {
        int currentIndex = 0;

        TempLectureList.Add(new JsonFileDataFrame());
        TempLectureList[currentIndex].LectureBuilding = 0;
        TempLectureList[currentIndex].LectureCapacity = 6;
        TempLectureList[currentIndex].LectureNumber = 1004;
        TempLectureList[currentIndex].LectureStudent = 0;
        TempLectureList[currentIndex].LectureTemperature = Random.Range(15, 31);
        TempLectureList[currentIndex].LectureHumidity = Random.Range(40, 51);
        currentIndex++;

        for (int buildingCount = 0; buildingCount < MaxBuilding; ++buildingCount)
        {
            for (int updateCount = 0; updateCount < RandomBatch; ++updateCount)
            {
                int capacity = Random.Range(40, 70); // ���ǽ��� �ѷ� ���� ���ǽ��� �ѷ��� 40 ~ 69���� ������ ���� ����
                TempLectureList.Add(new JsonFileDataFrame());
                TempLectureList[currentIndex].LectureBuilding = buildingCount;
                TempLectureList[currentIndex].LectureCapacity = capacity;
                TempLectureList[currentIndex].LectureNumber = Random.Range(1000, 9999);
                TempLectureList[currentIndex].LectureStudent = Random.Range(0, capacity);
                TempLectureList[currentIndex].LectureTemperature = Random.Range(15, 31);
                TempLectureList[currentIndex].LectureHumidity = Random.Range(40, 51);
                currentIndex++;
            }
        }

        for (int tmpCount = 0; tmpCount < 6; ++tmpCount)
        {
            int capacity = Random.Range(40, 70); // ���ǽ��� �ѷ� ���� ���ǽ��� �ѷ��� 40 ~ 69���� ������ ���� ����
            TempLectureList.Add(new JsonFileDataFrame());
            TempLectureList[currentIndex].LectureBuilding = 0;
            TempLectureList[currentIndex].LectureCapacity = capacity;
            TempLectureList[currentIndex].LectureNumber = Random.Range(1000, 2000);
            TempLectureList[currentIndex].LectureStudent = Random.Range(10, capacity);
            TempLectureList[currentIndex].LectureTemperature = Random.Range(15, 31);
            TempLectureList[currentIndex].LectureHumidity = Random.Range(40, 51);
            currentIndex++;
        }

        json = JsonUtilityExtention.ToJson(TempLectureList);
        Debug.Log(json);
    }
}
