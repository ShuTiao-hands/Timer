using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimerMgr : MonoBehaviour
{
    //��ʵ��һ������
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
    //����һ��list���浥����ʱ��
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
            //�Ӻ���ǰ�������Բ��ÿ������ö�ʧ����
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
    //��Ϊ��˽�еı��������Լ���һ���������Ը��ⲿʹ��
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