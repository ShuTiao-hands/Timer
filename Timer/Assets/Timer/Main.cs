using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        TimerMgr.Instance.StartChangeTime(3f, Test, true);        
    }

    private void Test()
    {
        Debug.Log("������֮������ʲô");
    }
}
