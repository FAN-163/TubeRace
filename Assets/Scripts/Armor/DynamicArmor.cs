using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicArmor : MonoBehaviour
{
    [SerializeField] private int m_HitPoins;

    public void applyDamage(int damage)
    {
        if(m_HitPoins > damage)
        {
            m_HitPoins -= damage;
        }
        else
        {
            Destruction();
        }
    }

    public void Destruction()
    {
        Debug.Log("Деталь отлетает, партикл эффект");
    }

}
