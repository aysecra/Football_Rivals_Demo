using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    public class TeamController : Scroller
    {
        protected override void Start()
        {
            amountToPool = (uint) SharedLevelManager.Instance.TeamList.Count;
            base.Start();
        }
    }
}

