using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBQ
{
    public interface Weapon
    {
        int Damage { get; set; }

        void ShootAnimation();
    }

}

