using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadMeSpace
{

    ///How to add a new weapon:
    ///1) create a new DamageTypeSword script (same format as NormalSword.cs)
    ///2) create a new DamageTypeDamage script (same as NormalDamage.cs)
    ///3) On th PickUpObject script do the following:
    /// Weapon_base weaponBase = Player.GetComponentInChildren<Weapon_base>();
    /// weaponBase.SetDamageType(new DamageTypeDamage());


}
