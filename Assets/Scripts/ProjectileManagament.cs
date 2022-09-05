using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileManagament
{
    public ProjectileScriptableObject projectileScriptableObject;
    public int amount;
    private Projectile projectile;
    private GameObject objectProjectile;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public ProjectileManagament(ProjectileScriptableObject projectileScriptableObject, int amount)
    {
        this.projectileScriptableObject = projectileScriptableObject;
        this.amount = amount;
        projectile = new Projectile { projectileScriptableObject = this.projectileScriptableObject, damageAmount = amount };
    }
    public void Launch(Vector2 location, int speed)
    {
     
        objectProjectile = new GameObject(projectile.GetString());
        objectProjectile.transform.position = location;
        rb = objectProjectile.AddComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY ^ RigidbodyConstraints2D.FreezeRotation;
        spriteRenderer = objectProjectile.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = projectileScriptableObject.sprite;

    }


}
