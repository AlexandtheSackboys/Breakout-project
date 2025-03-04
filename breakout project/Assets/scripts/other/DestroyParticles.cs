using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;
    /*
	* Destroys gameobject after its created on scene.
	* This is used for particles.
	*/
    void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }

}
