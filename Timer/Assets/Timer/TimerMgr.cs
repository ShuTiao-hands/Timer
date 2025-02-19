using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimerMgr : MonoBehaviour
{
    //先实现一个单例
    private static TimerMgr instance;
    public static TimerMgr Instance
    {
        get 
        { 
            return instance; 
        }
    }
    private void Awake()
    {
        instance = this;
    }
    //利用一个list保存单个计时器
    private List<Timer> timers = new List<Timer>();
    public void StartChangeTime(float maxTime, Action onComplete, bool isLoop = false)
    {
        var timer = new Timer(maxTime, onComplete, isLoop = false);
        timers.Add(timer);
    }
    public void Update() 
    {
        if (timers.Count > 0) 
        {
            //从后往前遍历可以不用考虑引用丢失问题
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                timers[i].ChangeTime(Time.deltaTime);
                if (timers[i].InFinish)
                {
                    timers.RemoveAt(i);
                }
            }
        }
    }
}


public class Timer
{
    private float maxTime;
    private Action onComplete;
    private float currentTime = 0f;
    private bool isLoop;
    private bool isFinish;

    public Timer(float maxTime, Action onComplete, bool isLoop = false)
    {
        this.maxTime = maxTime;
        this.onComplete = onComplete;
        this.isLoop = isLoop;
    }
    //因为是私有的变量，所以加了一个公共属性给外部使用
    public bool InFinish
    {
        get
        {
            return isFinish;
        }
    }
    public void ChangeTime(float time)
    {
        currentTime += time;
        if (currentTime >= maxTime)
        {
            onComplete?.Invoke();
            if (!isLoop)
            {
                currentTime = 0;
            }
            else
            {
                isFinish = true;
            }
        }
    }
}