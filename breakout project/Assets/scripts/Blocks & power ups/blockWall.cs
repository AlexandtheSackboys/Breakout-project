using UnityEngine;
using System.Collections;
using FMODUnity;
public class BlockWall : MonoBehaviour
{
    [SerializeField] private float _hitPoints;
    private ScoreSystem _scoreSystem; // reference game manager
    public GameObject Debris;

    [SerializeField] private PowerUp _pillSpawner;


    [SerializeField]private StudioEventEmitter _breakBlock;
    [SerializeField] private bool _hasPowerUp = false;

    private void Start()
    {
        _scoreSystem = FindAnyObjectByType<ScoreSystem>(); // Find the GameManager in the scene
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            _hitPoints--;
            _breakBlock.Play();

            if (_hitPoints == 0) {

                isPowerBlock();

            }
        }

    }

    private void blockGone()
    {
        if (_scoreSystem != null)
        {
            _scoreSystem.BlockDestroy(); // Notify GameManager that an enemy is killed

        }
        Destroy(gameObject); // game object is no longer in scene
        Instantiate(Debris);


    }

    private void isPowerBlock() 
    
    {
        if (_hasPowerUp)
        {
            blockGone();
            _pillSpawner.Spawn();
            return;
        }
        Debris.transform.position = gameObject.transform.position;
        blockGone();
    }



}
