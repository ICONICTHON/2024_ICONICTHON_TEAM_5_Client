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

    void Start()
    {
        CreateRandomDataFrame();
    }

    public static List<JsonFileDataFrame> GetData()
    {
        return TempLectureList;
    }

    private void CreateRandomDataFrame()
    {
        int currentIndex = 0;
        for(int buildingCount = 0; buildingCount < MaxBuilding; ++buildingCount)
        {
            for (int updateCount = 0; updateCount < RandomBatch; ++updateCount)
            {
                int capacity = Random.Range(40, 70); // 강의실의 총량 설정 강의실의 총량은 40 ~ 69까지 랜덤한 값을 가짐
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
        json = JsonUtilityExtention.ToJson(TempLectureList);
        Debug.Log(json);
    }
}
