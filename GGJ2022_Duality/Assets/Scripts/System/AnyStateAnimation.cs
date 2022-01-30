using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyStateAnimation
{

    public string[] HigherPrio { get; set; }

    public string Name { get; set; }

    public bool Active { get; set; }

    public AnyStateAnimation(string name, params string[] higherPrio)
    {
        this.Name = name;
        this.HigherPrio = higherPrio;
    }
}
