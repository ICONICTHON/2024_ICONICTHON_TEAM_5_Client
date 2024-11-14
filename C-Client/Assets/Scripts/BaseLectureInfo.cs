using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BaseLectureInfo
{
    protected readonly MainControler ParentMainController;

    #region Private_MainValues
    private int _lectureFloor;
    private int _lectureNumber;
    private int _lectureStudent;
    private int _lectureCapacity;
    private int _lectureTemperature;
    private int _lectureHumidity;
    private bool _canLectureControl;
    private bool _isLectureAir;
    private Dictionary<int, bool> _studentAttendanceDic = new Dictionary<int, bool>();

    private TMP_Text _lectureNumberText;
    private TMP_Text _lectureStudentText;
    private TMP_Text _lectureHumeText;
    private TMP_Text _lectureTempText;
    private Image _lectureAirConditionerImage;
    private Image _lectureControlImage;
    private Sprite[] _lectureAirConditionerImages;
    private Sprite[] _lectureControlImages;
    #endregion

    #region Public_MainValues
    public TMP_Text LectureNumberText
    {
        get
        {
            return _lectureNumberText;
        }

        set
        {
            _lectureNumberText = value;
            _lectureNumberText.text = LectureNumber.ToString();
        }
    }

    public TMP_Text LectureStudentText
    {
        get
        {
            return _lectureStudentText;
        }

        set
        {
            _lectureStudentText = value;
            _lectureStudentText.text = "" + LectureStudent;
        }
    }

    public TMP_Text LectureHumeText
    {
        get
        {
            return _lectureHumeText;
        }

        set
        {
            _lectureHumeText = value;
            _lectureHumeText.text = "" + LectureHumidity;
        }
    }

    public TMP_Text LectureTempText
    {
        get
        {
            return _lectureTempText;
        }

        set
        {
            _lectureTempText = value;
            _lectureTempText.text = "" + LectureTemperature;
        }
    }

    public Image LectureAirConditionerImage
    {
        get
        {
            return _lectureAirConditionerImage;
        }

        set
        {
            _lectureAirConditionerImage = value;
            if (CanLectureControl && Random.Range(0,2) == 1)
            {
                _lectureAirConditionerImage.sprite = _lectureAirConditionerImages[1];
            }
            else
            {
                _lectureAirConditionerImage.sprite = _lectureAirConditionerImages[0];
            }
        }
    }

    public Image LectureControlImage
    {
        get
        {
            return _lectureControlImage;
        }

        set
        {
            _lectureControlImage = value;
            
            if ((float)LectureStudent / (float)LectureCapacity > 0.5f)
            {
                CanLectureControl = false;
                _lectureControlImage.sprite = _lectureControlImages[1];
            }
            else
            {
                CanLectureControl = true;
                _lectureControlImage.sprite = _lectureControlImages[0];
            }
        }
    }

    public int LectureFloor
    {
        get
        {
            return _lectureFloor;
        }

        set
        {
            _lectureFloor = value;
        }
    }

    public int LectureNumber
    {
        get
        {
            return _lectureNumber;
        }
        set
        {
            _lectureNumber = value;
        }
    }

    public int LectureStudent
    {
        get
        {
            return _lectureStudent;
        }

        set
        {
            _lectureStudent = value;
        }
    }

    public int LectureCapacity
    {
        get
        {
            return _lectureCapacity;
        }

        set
        {
            _lectureCapacity = value;
        }
    }

    public int LectureHumidity
    {
        get
        {
            return _lectureHumidity;
        }

        set
        {
            _lectureHumidity = value;
        }
    }

    public int LectureTemperature
    {
        get
        {
            return _lectureTemperature;
        }

        set
        {
            _lectureTemperature = value;
        }
    }

    public bool CanLectureControl
    {
        get
        {
            return _canLectureControl;
        }

        set
        {
            _canLectureControl = value;
        }
    }

    public bool IsLectureAir
    {
        get
        {
            return _isLectureAir;
        }

        set
        {
            int flag = Random.Range(0, 2);
            if (flag == 1 && CanLectureControl) _isLectureAir = true;
            else _isLectureAir = false;
        }
    }

    public Dictionary<int, bool> StudentAttendanceDic
    {
        get
        {
            return _studentAttendanceDic;
        }

        set
        {
            _studentAttendanceDic = value;
        }
    }
    #endregion

    public BaseLectureInfo(MainControler mainControler)
    {
        ParentMainController = mainControler;
    }


    public void SetInfoObject(List<GameObject> infoObjects, Sprite[] images1, Sprite[] images2)
    {
        _lectureControlImages = images1;
        _lectureAirConditionerImages = images2;
        LectureNumberText = infoObjects[0].GetComponent<TMP_Text>();
        LectureControlImage = infoObjects[5].GetComponent<Image>();
        LectureAirConditionerImage = infoObjects[1].GetComponent<Image>();
        LectureStudentText = infoObjects[2].GetComponent<TMP_Text>();
        LectureHumeText = infoObjects[3].GetComponent<TMP_Text>();
        LectureTempText = infoObjects[4].GetComponent<TMP_Text>();
    }

    public BaseLectureInfo DeepCopy()
    {
        BaseLectureInfo baseLectureInfo = new BaseLectureInfo(ParentMainController);
        baseLectureInfo.LectureCapacity = LectureCapacity;
        baseLectureInfo.LectureFloor = LectureFloor;
        baseLectureInfo.LectureStudent = LectureStudent;
        baseLectureInfo.LectureNumber = LectureNumber;
        baseLectureInfo.LectureTemperature = LectureTemperature;
        baseLectureInfo.LectureHumidity = LectureHumidity;
        
        return baseLectureInfo;
    }
}
