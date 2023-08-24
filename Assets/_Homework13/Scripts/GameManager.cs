using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int _memories = 0;

    public static int Memories
    {
        get { return _memories; }
        set
        {
            _memories = value;
            Debug.Log(_memories);
        }
    }
    
    //TODO: при смерти игрока счётчик воспоминий не обновляется до значения до загрузки уровня

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
