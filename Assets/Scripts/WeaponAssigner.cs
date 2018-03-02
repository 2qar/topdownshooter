using UnityEngine;
using System.Collections;

public class WeaponAssigner : MonoBehaviour
{
    private int weaponNum;
    public int WeaponNum
    {
        get { return weaponNum; }
        set { CreateWeapon(value); weaponNum = value; }
    }

    private void CreateWeapon(int num)
    {
        Weapon weapon = new Weapon(weaponNum);
    }
}