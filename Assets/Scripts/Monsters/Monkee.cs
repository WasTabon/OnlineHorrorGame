using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkee : Monster
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("123");
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log("123");

    }

    protected override void Update()
    {
        base.Update();
        Debug.Log("123");

    }
}
