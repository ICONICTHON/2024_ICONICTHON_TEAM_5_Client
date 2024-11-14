using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LectureDefinition;

public class MainControler : MonoBehaviour
{
    [SerializeField] JsonSendToLoad JsonSend;

    // ���ǽ� ���� UI
    public GameObject LectureRoomPrefab;
    public GameObject LectureRoomInfoPanel;

    // �ʱ� ���ǽ� ���� �Է�
    private Dictionary<EBuildingField, List<BaseLectureInfo>> LectureDic;

    // ���� �ڵ�� �ʿ��� ����
    private EBuildingField _currentSelectBuilding = EBuildingField.Engineering;
    private float _timer = 0;
    private float _resetTime = 3f;

    // �׽�Ʈ ������
    private List<JsonFileDataFrame> _JsonData = new List<JsonFileDataFrame>();
    private List<string> _testData = new List<string>()
    {
        {"0-4147-40-30"},
        {"0-4137-50-20"}
    };

    private void Start()
    {
        // LectureInfo ������ ȭ�鿡 ���
        SetUpLectureDic();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _resetTime)
        {
            _timer = 0;
            UpdateLectureInfo();
            Debug.Log("Panel�� ������Ʈ �ƽ��ϴ�.");
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
        // ������ �ִ� ������ ����
        foreach(Transform child in LectureRoomInfoPanel.transform)
        {
            Destroy(child.gameObject);
        }
        // CreateLectureInfoObject�� ���� �Լ�
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
            // LectureDic�ȿ� ����ִ� baseLectureInfo ����Ʈ ȣ��
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
        baseLectureInfo.LectureFloor = baseLectureInfo.LectureNumber / 1000; // �� ����

        EBuildingField dataBuildingField = (EBuildingField)jsonData.LectureBuilding;
        LectureDic[dataBuildingField].Add(baseLectureInfo.DeepCopy());
    }

    private void UpdateLectureInfo()
    {
        // ���� ������ �ʱ�ȭ
        LectureDic.Clear();
        SetUpLectureDic();

        // ���ο� ������ �� �޾ƿ���
        _JsonData = JsonSend.LectureDataLoad();
        foreach (JsonFileDataFrame jsonData in _JsonData)
        {
            ConvertToEditor(jsonData); 
        }

        CreateLectureInfoObject(LectureDic[_currentSelectBuilding]);
    }
}
