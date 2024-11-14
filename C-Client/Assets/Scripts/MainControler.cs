using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LectureDefinition;

public class MainControler : MonoBehaviour
{
    [SerializeField] JsonSendToLoad JsonSend;

    // 강의실 정보 UI
    public GameObject LectureRoomPrefab;
    public GameObject LectureRoomInfoPanel;

    // 초기 강의실 정보 입력
    private Dictionary<EBuildingField, List<BaseLectureInfo>> LectureDic;

    // 개발 코드상 필요한 변수
    private EBuildingField _currentSelectBuilding = EBuildingField.Engineering;
    private float _timer = 0;
    private float _resetTime = 3f;

    // 테스트 데이터
    private List<JsonFileDataFrame> _JsonData = new List<JsonFileDataFrame>();
    private List<string> _testData = new List<string>()
    {
        {"0-4147-40-30"},
        {"0-4137-50-20"}
    };

    private void Start()
    {
        // LectureInfo 데이터 화면에 출력
        SetUpLectureDic();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _resetTime)
        {
            _timer = 0;
            UpdateLectureInfo();
            Debug.Log("Panel이 업데이트 됐습니다.");
        }
    }

    private void SetUpLectureDic()
    {
        LectureDic = new Dictionary<EBuildingField, List<BaseLectureInfo>>()
        {
            {EBuildingField.Engineering, new List<BaseLectureInfo>()},
            {EBuildingField.Hyehwagwan, new List<BaseLectureInfo>()},
            {EBuildingField.InfomationCulture, new List<BaseLectureInfo>()}
        };
    }

    private void CreateLectureInfoObject(List<BaseLectureInfo> baseLectureInfos)
    {
        // 기존에 있던 데이터 리셋
        foreach(Transform child in LectureRoomInfoPanel.transform)
        {
            Destroy(child.gameObject);
        }
        // CreateLectureInfoObject의 서브 함수
        List<GameObject> FindInfoObejct(GameObject parentLectureObject)
        {
            List<GameObject> lectureInfoChilds = new List<GameObject>();
            foreach(Transform child in parentLectureObject.transform)
            {
                lectureInfoChilds.Add(child.GetChild(0).gameObject);
            }
            return lectureInfoChilds;
        }

        foreach(BaseLectureInfo baseLectureInfo in baseLectureInfos)
        {
            // LectureDic안에 들어있는 baseLectureInfo 리스트 호출
            GameObject infoSpawned = Instantiate(LectureRoomPrefab, LectureRoomInfoPanel.transform);
            baseLectureInfo.SetInfoObject(FindInfoObejct(infoSpawned));
            infoSpawned.name = baseLectureInfo.LectureFloor + "F-" + baseLectureInfo.LectureNumber;
        }
    }

    private void ConvertToEditor(JsonFileDataFrame jsonData)
    {
        BaseLectureInfo baseLectureInfo = new BaseLectureInfo(this);

        baseLectureInfo.LectureNumber = jsonData.LectureNumber;
        baseLectureInfo.LectureCapacity = jsonData.LectureNumber;
        baseLectureInfo.LectureStudent = jsonData.LectureNumber;
        baseLectureInfo.LectureFloor = baseLectureInfo.LectureNumber / 1000; // 층 구별

        EBuildingField dataBuildingField = (EBuildingField)jsonData.LectureBuilding;
        LectureDic[dataBuildingField].Add(baseLectureInfo.DeepCopy());
    }

    private void UpdateLectureInfo()
    {
        // 기존 데이터 초기화
        LectureDic.Clear();
        SetUpLectureDic();

        // 새로운 데이터 셋 받아오기
        _JsonData = JsonSend.LectureDataLoad();
        foreach (JsonFileDataFrame jsonData in _JsonData)
        {
            ConvertToEditor(jsonData); 
        }

        CreateLectureInfoObject(LectureDic[_currentSelectBuilding]);
    }
}
