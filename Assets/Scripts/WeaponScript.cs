using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    public bool enemyOnRange;
    public Collider target;
    public int weaponDmg;
    public float weaponSpeed;
    public string enemyTag;
    public float weaponRange;

    // Use this for initialization
    void Start()
    {
        if (weaponDmg == 0)
            weaponDmg = 5;
        if (weaponSpeed == 0f)
            weaponSpeed = 1f;
        if (weaponRange == 0f)
            weaponSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //something entered in weapon range

        if (other.tag == enemyTag) {   
            enemyOnRange = true;
            target = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //something exited from weapon range
        if (other.tag == enemyTag)
        {
            enemyOnRange = false;
            target = null;
        }
    }
}
