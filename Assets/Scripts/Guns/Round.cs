/*
    Round.cs

    The object spawned when as gun is fired.
    References the source gun to access ballistics info.
*/

using UnityEngine;

public class Round : MonoBehaviour
{
    public float damage;
    public float despawnTime = 1.5f;
    private float counter = 0f;
    public bool overPenetrate = false;

    void Update(){
        counter += Time.deltaTime;
        if(counter > despawnTime)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Target"){
            other.gameObject.GetComponent<Target>().Hit(damage);

            // Destroy this round if not overpenetratable 
            if(!overPenetrate)
                Destroy(this.gameObject);
        }
    }
}
