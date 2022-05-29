/*
    Target.cs

    Allows for rounds fired from a gun to damage the gameObject.
*/

using UnityEngine;

public class Target : MonoBehaviour{

    public float health;

    public void Hit(float damage){
        health -= damage;
        if(health <= 0) KillObject();
        Debug.Log("Hit");
    }

    public void KillObject()
    {
        Destroy(this.gameObject);
    }
}
