/*
    Gun.cs

    Stores all values and methods related to guns.
    A weapon is created by changing the values and references
    to objects.

    Methods listed are called upon the PlayerController.cs script.
*/

using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour{

    public enum ShootState{
        Ready,
        Shooting,
        Reloading
    }

    [SerializeField] private string gunName;

    [Header("Camera Effects")]
    private CameraController cameraShake;                                    // Reference to CameraController object
    //public CameraControl cameraShake;                                    // Reference to CameraControl object (AS OF NOW FOR DANNY's SCENE ONLY)
    [Range(0f, 1f)] public float shakeMagnitude;                            // Hypotenuse of camera shake
    [Range(0.01f, 0.15f)] public float shakeSharpness;                      // How quickly the camera lerps during the shake

    [Header("Properties")] 
    private PlayerInventory playerInventory;
    [SerializeField] [Range(1f, 2000f)] private float firerate;             // How fast the gun shoots in rounds per minute
    [SerializeField] [Range(0f, 45f)] private float accuracy;               // The spawn angle of the round shot
    [SerializeField] private int roundsPerShot;                             // How many rounds are shot in one fire instance
    [Range(0.5f, 100f)] public float muzzleVelocity;                        // How fast the round travels
    [SerializeField] private float muzzleOffset;                            // Distance of the gun's muzzle from the origin point
    [SerializeField] private bool singleFire;

    [Header("Magazine")]
    [SerializeField] private GameObject round;                              // Bullet type reference
    [SerializeField] private int magSize;                                   // How many rounds are inside the magazine, if any
    public int remainingRounds;                                            // How many rounds are currently in the magazine (must be less/equal to magsize)
    [SerializeField] private float reloadTime;                              // Time it takes to cycle a new magazine
    [SerializeField] private float reloadTimeEmpty;                         // Time it takes to cycle a new magazine plus charging bolt, if applicable

    public ShootState shootState = ShootState.Ready;                       // Current state of the gun

    private float nextShootTime = 0f;                                       // The next time the gun is able to shoot

    // private Renderer renderer;
    // private Color _emissionColorValue;

    //FMOD Parameter setting
    private string eventpath = "event:/ShootingGun";
    private FMOD.Studio.EventInstance e_instance;

    void Start() {
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraController>();

        // renderer = GetComponent<MeshRenderer>();
        // _emissionColorValue = renderer.material.GetColor("_EmissionColor");

        // Fill the magazine
        firerate /= 60;
        remainingRounds = magSize;
        e_instance = FMODUnity.RuntimeManager.CreateInstance(eventpath);
        if (gunName.Equals("Pilate"))
        {
            e_instance.setParameterByName("GunSelection", 2);
        }
        else if(gunName.Equals("Harpy"))
        {
            e_instance.setParameterByName("GunSelection", 1);
        }
        else if(gunName.Equals("Vindicator"))
        {
            e_instance.setParameterByName("GunSelection", 0);
        }
        else if (gunName.Equals("Apostle"))
        {
            e_instance.setParameterByName("GunSelection", 3);
        }
        else
        {
            e_instance.setParameterByName("GunSelection", 0); // default sound for now...
        }

        
        
    }

    // void OnCollisionEnter(Collision collisionInfo)
    // {
        
    //     if(collisionInfo.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Gun Entered Colliding With Player");
    //         //renderer.material.SetColor("_EmissionColor", _emissionColorValue * Mathf.PingPong(Time.time, 1000));
    //         renderer.material.SetColor("_EmissionColor",  _emissionColorValue * Mathf.PingPong(Time.time, 1000));
    //     }
    //     // Debug-draw all contact points and normals
    //     // foreach (ContactPoint contact in collisionInfo.contacts)
    //     // {
    //     //     Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
    //     // }
    // }

    void Update(){
        
        if(!GameValues.instance.isPaused)
        {
            switch(shootState){
                case ShootState.Shooting:
                    // If enough time has passed for the gun to shoot again
                    if(Time.time > nextShootTime)
                        shootState = ShootState.Ready;
                    break;
                case ShootState.Reloading:
                    // If the gun has finished reloading
                    if(Time.time > nextShootTime){
                        // If mag is empty, remainig rounds = mag size
                        // If mag contains rounds, remaining rounds = mag size plus chamber
                        if(remainingRounds == 0)
                            remainingRounds = magSize;
                        else
                            remainingRounds = magSize + 1;
                        shootState = ShootState.Ready;
                    }

                    // Not the best way. Just a fix for now while we get reloading sounds in
                    FMOD.Studio.PLAYBACK_STATE fmodPbState;
                    e_instance.getPlaybackState(out fmodPbState);
                    if (fmodPbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        e_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    }
                    break;
                default:
                    //FMOD.Studio.PLAYBACK_STATE fmodPbState;
                    e_instance.getPlaybackState(out fmodPbState);
                    if (fmodPbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        e_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    }
                    break;
            }
        }

        // float emission = Mathf.PingPong(Time.time, 1000.0f);
        // Color baseColor = Color.yellow;
        // Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

        // renderer.material.SetColor("_EmissionColor",  finalColor);
    }

    public void setInventory(PlayerInventory playerInventory) {
        this.playerInventory = playerInventory;
    }

    public PlayerInventory getInventory() {
        return playerInventory;
    }
    public void Fire(){
        // Check if gun is ready to fire
        if(shootState == ShootState.Ready && remainingRounds > 0 && !GameValues.instance.isPaused){
            // Instantiate each round, given roundsPerShot
            for(int i = 0; i < roundsPerShot; i++){
                GameObject bullet = Instantiate(
                    round,
                    transform.position + transform.forward * muzzleOffset,
                    transform.rotation
                );
                
                bullet.GetComponent<Round>().updateInventory(playerInventory);

                // Apply accuracy value
                bullet.transform.Rotate(new Vector3(
                    0,
                    Random.Range(-1f, 1f) * accuracy,
                    0
                ));

                // Apply muzzleVelocity value
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bullet.transform.forward * muzzleVelocity;

                // Shake the camera when a bullet is fired
                StartCoroutine(cameraShake.Shake(shakeMagnitude, shakeSharpness));
            }
            // Decrease the remaining ammo by 1
            remainingRounds--;

            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            e_instance.getPlaybackState(out fmodPbState);
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                e_instance.start();
            }

            // If mag is empty, reload. If not, change to shooting state and calculate next shooting time
            if (remainingRounds > 0){
                nextShootTime = Time.time + (1f / firerate);
                shootState = ShootState.Shooting;
            } else{
                nextShootTime = Time.time + 0.1f;
            }
        }
    }

    public void Reload(){
        if(remainingRounds != magSize + 1){
            if(remainingRounds == 0) // Tactical Reload
                nextShootTime = Time.time + reloadTimeEmpty;
            else                     // Full Reload
                nextShootTime = Time.time + reloadTime;
            
            shootState = ShootState.Reloading;
        }
    }

    public int CurrentAmmo{
        get{
            return remainingRounds;
        }
    }

    public int MaxAmmo{
        get{
            return magSize;
        }
    }

    public bool IsSemi{
        get{
            return singleFire;
        }
    }

    public string Name{
        get{
            return gunName;
        }
    }

    public float reloadingTime{
        get{
            return reloadTime;
        }
    }

    public void setCamera(CameraController cameraController) {
        
    }
}
