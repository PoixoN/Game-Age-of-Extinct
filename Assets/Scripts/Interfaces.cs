using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    ObjectPooler.ObjectInfo.ObjectType Type { get; }
}

interface ICollectable
{
    void Colect(Player player);
}
