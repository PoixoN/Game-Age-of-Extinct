using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    ObjectPooler.ObjectInfo.ObjectType Type { get; }
}

public interface ICollectable
{
    void Collect(Player player);
    void Artem();
}

public interface IDamageable
{
    void ApplyDamage(int damage = 1);
}
