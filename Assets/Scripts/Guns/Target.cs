/*
    Target.cs

    Allows for rounds fired from a gun to damage the gameObject.
*/

using UnityEngine;

public class Target : MonoBehaviour{

    public float health;

    void Update(){
        if(health <= 0)
            Destroy(this.gameObject);
    }

    public void Hit(float damage){
        health -= damage;
        Debug.Log("Hit");
    }
}
