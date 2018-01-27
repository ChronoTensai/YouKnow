using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Friend : Character
{
    bool isTarget = false;
    bool visited = false;
    bool hasMessage = false;
    
    public override void GetTheMessage()
    {
        if(!visited)
        {
            visited = true;
            hasMessage = true;
            base.GetTheMessage();
            StartShoot();
        }
    }
    
    private void StartShoot()
    {
        //ShowUIShoot
    }
    
    void Update()
    {
        if(hasMessage)
        {
            ShootLogic();
        }
    }
    
    private void ShootLogic()
    {
        
    }
    
    private void Shoot()
    {
    
    }
}

