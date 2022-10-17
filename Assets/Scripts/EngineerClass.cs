using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerClass : BaseUnitClass
{
    public Build Create;
    public BuildMenu Menu;
    public new void Start()
    {
        base.Start();
        Create = gameObject.AddComponent<Build>();
        Menu = gameObject.AddComponent<BuildMenu>();
    }
}
