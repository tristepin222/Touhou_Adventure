using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile 
{
    // Start is called before the first frame update
    public enum ProjectileType
    {
        Amulet,
        Arrow,
        ArrowHead,
        Bacteria,
        Ball,
        Ball2,
        Bubble,
        Bullet,
        Butterfly,
        Coin,
        Fireball,
        FireBall2,
        Heart,
        Jellybean,
        Knife,
        Kunai,
        Mentos,
        Note,
        Orb,
        Pellet,
        Popcorn,
        Raindrop,
        Rest,
        Rice,
        Rose,
        Shard,
        Star2,
        Star,
        WildCard,
        ccatch,
        Hoe,
        Pickaxe,
    }

    // Update is called once per frame
    public ProjectileScriptableObject projectileScriptableObject;
    public int damageAmount;


   
    public string GetString()
    {
        switch (projectileScriptableObject.projectileType)
        {
            default:
            case ProjectileType.Amulet: return "Amulet";
            case ProjectileType.Arrow: return "Arrow";
            case ProjectileType.ArrowHead: return "ArrowHead";
            case ProjectileType.Bacteria: return "Bacteria";
            case ProjectileType.Ball: return "Ball";
            case ProjectileType.Ball2: return "Ball(Outlined)";
            case ProjectileType.Bubble: return "Bubble";
            case ProjectileType.Bullet: return "Bullet";
            case ProjectileType.Butterfly: return "Butterfly";
            case ProjectileType.Coin: return "Coin";
            case ProjectileType.Fireball: return "Fireball";
            case ProjectileType.FireBall2: return "FireBall(glowy)";
            case ProjectileType.Heart: return "Heart";
            case ProjectileType.Jellybean: return "Jellybean";
            case ProjectileType.Knife: return "Knife";
            case ProjectileType.Kunai: return "Kunai";
            case ProjectileType.Mentos: return "Mentos";
            case ProjectileType.Note: return "Note";
            case ProjectileType.Orb: return "Orb";
            case ProjectileType.Pellet: return "Pellet";
            case ProjectileType.Popcorn: return "Popcorn";
            case ProjectileType.Raindrop: return "Raindrop";
            case ProjectileType.Rest: return "Rest";
            case ProjectileType.Rice: return "Rice";
            case ProjectileType.Rose: return "Rose";
            case ProjectileType.Shard: return "Shard";
            case ProjectileType.Star2: return "Star(Big)";
            case ProjectileType.Star: return "Star(Small)";
            case ProjectileType.ccatch: return "Catch";
        }

    }

    
}
