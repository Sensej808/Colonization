using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecourceManager : MonoBehaviour
{
    [SerializeField]
    public  InGameStorage storage;
    public static RecourceManager Instance;
    public static event Action OnRecourcesChanged;

    private void Awake()
    {
        OnRecourcesChanged += () => Debug.Log("Invoked");
        Instance = this;
        DontDestroyOnLoad(Instance);
        storage.materials.Metal = 0;
    }

    public  void AddMetal(int count)
    {
        if (count > 0)
        {
            storage.Instance.materials.Metal += count;
            if (OnRecourcesChanged != null) OnRecourcesChanged.Invoke();
        }
    }

    public  void TakeMetal(int count)
    {
        if (count > 0)
        {
            if (storage.Instance.materials.Metal >= count)
                storage.Instance.materials.Metal -= count;
            if (OnRecourcesChanged != null) OnRecourcesChanged.Invoke();

        }
    }

    public void AddCristall(int count)
    {
            if (count > 0)
            {
                storage.Instance.materials.Cristalls += count;
                if (OnRecourcesChanged != null) OnRecourcesChanged.Invoke();


            }

    }

    public void TakeCristall(int count)
    {
        if (count > 0)
        {
            if (storage.Instance.materials.Cristalls >= count)
                storage.Instance.materials.Cristalls -= count;
            if (OnRecourcesChanged != null) OnRecourcesChanged.Invoke();

        }
    }

    public int GetMetal()
    {
        return storage.Instance.materials.Metal;
    }

    public int GetCristall()
    {
        return storage.Instance.materials.Cristalls;
    }
}
