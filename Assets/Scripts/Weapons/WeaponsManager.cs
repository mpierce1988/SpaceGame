using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Weapons
{
	public class WeaponsManager : MonoBehaviour
    {
        [SerializeField] List<BaseWeapon> weapons = new List<BaseWeapon>();

        // Start is called before the first frame update
        void Start()
        {
            // start all weapons in list
            foreach (var weapon in weapons)
            {
				weapon.BeginFiring();
			}
        }        
    }
}
