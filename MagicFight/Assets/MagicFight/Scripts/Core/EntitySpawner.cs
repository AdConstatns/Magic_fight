namespace AmazingTeam.MagicFight
{
    using UnityEngine;

    /// <summary>
    /// Spawns entities at an interval (or just once).
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class EntitySpawner : MonoBehaviour
    {
        /// <summary>
        /// The type of object to be spawned -> must correspond to a list in the spawning manager
        /// </summary>
        public EntityType entityType;

        /// <summary>
        /// The frequency by which objects are spawned
        /// </summary>
        public float frequency = 3f;

        /// <summary>
        /// Time delay before the first object is spawned
        /// </summary>
        public float delay = 4f;

        /// <summary>
        /// Instruct the spawner to only spawn one
        /// </summary>
        public bool spawnOnce;

        [SerializeField]
        private float _lastSpawnTime;
        [SerializeField]
        private float _CurrentTime;

        private bool spawnFirst = true;

        private void Start()
        {
#if UNITY_EDITOR
            Debug.Log("<color=yellow><b>Initializing: Spawner </b></color>");
#endif
            _lastSpawnTime = delay - frequency;
        }

        private void Update()
        {
            _CurrentTime = Time.time;
            if (Time.time > _lastSpawnTime + frequency)
            {
                Spawn();
                if (spawnOnce)
                {
                    this.enabled = false;
                }
            }
        }

        private void Spawn()
        {
            _lastSpawnTime = Time.time;

            if (spawnFirst) {
                spawnFirst = false;
               

                EntityManager.instance.Spawn(entityType, transform.position, transform.rotation);
            }
               

            // If Empty Span the object.
            if (locationIsObstructed(transform.position) && !spawnFirst)
                EntityManager.instance.Spawn(entityType, transform.position, transform.rotation);          

#if UNITY_EDITOR
            Debug.Log($"<color=green><b>Spawnning: { entityType +" "+ Time.time } </b></color>");
#endif
        }

        // Check whether any box collider/ Health is Present at that location
        bool locationIsObstructed(Vector3 location) {
            // return Physics.CheckSphere(location, 2.0f);
            LayerMask mask = LayerMask.GetMask("Powerup");
            Collider[] colliders = Physics.OverlapSphere(location, 2.0f, mask);


            if (colliders.Length > 0) {
                foreach (var col in colliders) {
                    return !col.gameObject.CompareTag("Powerup");
                }
            }     
            return true;
        }
    }
}
