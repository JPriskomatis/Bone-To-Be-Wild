using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damageables
{

    public interface ISwordDamageable
    {
        void SwordDamageable(int damage);

    }
    //TODO:
    //When I implement health system, make this itnerface deal damage
    //directly to the health of the enemy;

}