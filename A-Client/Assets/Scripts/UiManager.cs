using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class UiManager : MonoBehaviour
{
    public Vector2 TouchPosition = new Vector2(0, 0);    
    private TouchUtil.ETouchState _touchState = TouchUtil.ETouchState.None;


    // Update is called once per frame
    void Update()
    {
        TouchUtil.TouchSetUp(ref _touchState, ref TouchPosition);
    }
}
