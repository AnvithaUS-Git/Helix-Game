using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJGameEventHandler
{
    private static HJGameEventHandler mInstance;

    public static HJGameEventHandler Instance()
    {
        return mInstance = mInstance == null ? new HJGameEventHandler() : mInstance;
    }

    public delegate void OnScoreChangedDel(int score);

    public event OnScoreChangedDel OnScoreChangedEvent;

    public void TriggerOnScoreChangedEvent(int score)
    {
        OnScoreChangedEvent?.Invoke(score);
    }
}
