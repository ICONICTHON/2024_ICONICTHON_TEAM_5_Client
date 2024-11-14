using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LectureSlot : MonoBehaviour
{
    [SerializeField] Text lectureNameText;
    [SerializeField] GameObject focusObject;
    [SerializeField] GameObject notYetObject;
    [SerializeField] Text startTimeText;

    public void Init(Lecture lecture, bool isFocus)
    {
        lectureNameText.text = lecture.GetLectureName();
        if(isFocus)
        {
            focusObject.SetActive(true);
            notYetObject.SetActive(false);
        }
        else
        {
            focusObject.SetActive(false);
            notYetObject.SetActive(true);
            startTimeText.text = lecture.GetStartHour() + ":" + lecture.GetStartMinute();
        }
    }
}
