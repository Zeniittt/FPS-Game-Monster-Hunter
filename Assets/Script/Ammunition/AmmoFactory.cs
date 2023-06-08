using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public abstract class AmmoFactory : MonoBehaviour
{
    public abstract GameObject CreateAmmo(Vector3 position);
}
