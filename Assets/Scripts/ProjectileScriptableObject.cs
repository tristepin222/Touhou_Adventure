using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ProjectileScriptableObject")]
public class ProjectileScriptableObject : ScriptableObject
{

    public Projectile.ProjectileType projectileType;
    public Sprite sprite;
    public int damage = 1;
    public int speed = 10;
}
