using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClassesTool
{
    public abstract class Weapon
    {
        #region Private Fields

        private int damage;
        private WeaponName name;
        private WeaponFireMode fireMode;

        #endregion

        #region Constructor

        public Weapon(WeaponName name, int damage, WeaponFireMode fireMode)
        {
            this.Damage = damage;
            this.Name = name;
            this.FireMode = fireMode;
        }

        #endregion

        #region Getters And Setters

        public WeaponFireMode FireMode { get => fireMode; set => fireMode = value; }
        public int Damage { get => damage; set => damage = value; }
        public WeaponName Name { get => name; set => name = value; }

        #endregion
    }

}

