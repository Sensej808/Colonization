using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public bool isCompleted;
    public bool isFailed;
    public string CompletedText;
    public string FailedText;
    public Action FuncCompleted;
    public Action FuncFailed;
    public string name;
    public Mission(bool isCompleted, bool isFailed, string completedText, string failedText, Action funcCompleted, Action funcFailed, string name)
    {
        this.isCompleted = isCompleted;
        this.isFailed = isFailed;
        CompletedText = completedText;
        FailedText = failedText;
        FuncCompleted = funcCompleted;
        FuncFailed = funcFailed;
        this.name = name;
    }
}
