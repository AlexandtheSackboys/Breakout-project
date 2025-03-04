using UnityEngine;
using System.Collections;
using FMODUnity;
public class BlockWall : MonoBehaviour
{
    [SerializeField] private float _hitPoints; // determines the number of hits it takes to destroy a block
    
    private ScoreSystem _scoreSystem; 
    [SerializeField] private GameObject _debris;

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
        Instantiate(_debris);


    }

    private void isPowerBlock() 
    
    {
        if (_hasPowerUp)
        {
            
            blockGone();
            _pillSpawner.Spawn();
            return;

        }
        _debris.transform.position = gameObject.transform.position;
        blockGone();
    }



}
