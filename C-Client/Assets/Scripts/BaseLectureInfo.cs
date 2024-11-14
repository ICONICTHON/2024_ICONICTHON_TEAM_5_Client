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
    private bool _canLectureControl;
    private Dictionary<int, bool> _studentAttendanceDic = new Dictionary<int, bool>();

    private TMP_Text _lectureNumberText;
    private TMP_Text _lectureStudentText;
    private Image _lectureAirConditionerImage;
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
            _lectureNumberText.text = "강의실" + LectureNumber;
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
            _lectureStudentText.text = "학생수 : " + LectureStudent;
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
            if (CanLectureControl)
            {
                _lectureAirConditionerImage.color = new Color(0, 0, 1);
            }
            else
            {
                _lectureAirConditionerImage.color = new Color(1, 0, 0);
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
            return _lectureStudent;
        }

        set
        {
            _lectureCapacity = value;
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


    public void SetInfoObject(List<GameObject> infoObjects)
    {
        LectureNumberText = infoObjects[0].GetComponent<TMP_Text>();
        LectureAirConditionerImage = infoObjects[1].GetComponent<Image>();
        LectureStudentText = infoObjects[2].GetComponent<TMP_Text>();
    }

    public BaseLectureInfo DeepCopy()
    {
        BaseLectureInfo baseLectureInfo = new BaseLectureInfo(ParentMainController);
        baseLectureInfo.LectureCapacity = LectureCapacity;
        baseLectureInfo.LectureFloor = LectureFloor;
        baseLectureInfo.LectureStudent = LectureStudent;
        baseLectureInfo.LectureNumber = LectureNumber;
        
        return baseLectureInfo;
    }
}
