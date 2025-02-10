using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    public float timeToDestroy;
    /*
	* Destroys gameobject after its created on scene.
	* This is used for particles.
	*/
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

}
