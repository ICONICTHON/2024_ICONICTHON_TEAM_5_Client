using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LectureSlot : MonoBehaviour
{
    [SerializeField] Text lectureNameText;
    [SerializeField] Text lectureNameText_1;

    Action<string> clickCallback;

    string lectureCode;

    public void Init(string code, string lectureName, Action<string> callback)
    {
        lectureCode = code;

        lectureNameText.text = lectureName;
        lectureNameText_1.text = lectureName;

        clickCallback = callback;
    }

    public void OnClickLecture()
    {
        clickCallback?.Invoke(lectureCode);
    }
}
