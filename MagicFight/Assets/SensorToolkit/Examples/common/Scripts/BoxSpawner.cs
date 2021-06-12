using UnityEngine;
using System.Collections;

namespace SensorToolkit.Example
{
    public class BoxSpawner : MonoBehaviour
    {
        public Teams Team;
        public GameObject ToSpawn;
        public int Number;
        public float SpawnInterval;
        public int StartSpawnAmount;

        public float SizeX = 10f;
        public float SizeY = 10f;
        public float SizeZ = 10f;

        public float ClearRadius = 1f;
        public LayerMask ObstructingLayers;

        float spawnCountdown;
        GameObject[] spawned;

        void Awake()
        {
            spawned = new GameObject[Number];
        }

        void Start()
        {
            for (int i = 0; i < StartSpawnAmount; i++)
            {
                spawn();
            }
        }

        void OnEnable()
        {
            StartCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine()
        {
            spawnCountdown = SpawnInterval;
            while (true)
            {
                spawnCountdown -= Time.deltaTime;
                if (spawnCountdown <= 0f)
                {
                    spawn();
                }
                yield return null;
            }
        }

        int nextAvailableSlot
        {
            get
            {
                for (int i = 0; i < spawned.Length; i++)
                {
                    if (spawned[i] == null) return i;
                }
                return -1;
            }
        }

        void spawn()
        {
            spawnCountdown = SpawnInterval;
            var nextSlot = nextAvailableSlot;
            if (nextSlot == -1) return; // No spawn slots available

            int nTrys = 0;
            Vector3 pos;
            do
            {
                nTrys++;
                if (nTrys > 10)
                {
                    Debug.LogWarning("Failed to find spawn location after 10 tries, aborting.", gameObject);
                    return;
                }
                pos = chooseLocation();
            } while (locationIsObstructed(pos));

            var newInst = Instantiate(ToSpawn, pos, transform.rotation) as GameObject;
            newInst.transform.SetParent(transform.parent);

            // Assigning the Color of the Team Member relative to Base 
            if (Team != Teams.None)
            {
                var instTeam = newInst.GetComponent<TeamMember>();
                if (instTeam != null)
                {
                    instTeam.StartTeam = Team;
                }
            }
            spawned[nextSlot] = newInst;
        }

        Vector3 chooseLocation()
        {
            var dims = new Vector3(SizeX / 2f, SizeY / 2f, SizeZ / 2f);
            var randVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            var pos = Vector3.Scale(dims, randVector) + transform.position;
            return pos;
        }

        bool locationIsObstructed(Vector3 location)
        {
            return Physics.CheckSphere(location, ClearRadius, ObstructingLayers);
        }

        protected static readonly Color YellowColor = Color.yellow;
        protected static readonly Color MagentaColor = Color.magenta;
        protected static readonly Color NoneColor = Color.white;
        protected static readonly Color RedColor = Color.red;
        protected static readonly Color GreenColor = Color.green;
        protected static readonly Color CyanColor = Color.cyan;
        public void OnDrawGizmosSelected()
        {
            if (!isActiveAndEnabled) return;

            switch (Team) {
                case Teams.Yellow:
                    Gizmos.color = YellowColor;
                    break;
                case Teams.Magenta:
                    Gizmos.color = MagentaColor;
                    break;
                case Teams.Green:
                    Gizmos.color = GreenColor;
                    break;
                case Teams.Red:
                    Gizmos.color = RedColor;
                    break;
                case Teams.Cyan:
                    Gizmos.color = CyanColor;
                    break;
                case Teams.None:
                    Gizmos.color = Color.white;
                    break;
                default:
                    break;
            }
           
            Gizmos.DrawCube(transform.position, new Vector3(SizeX, SizeY, SizeZ));

            Gizmos.color = RedColor;
            Gizmos.DrawSphere(transform.position + Vector3.up * (SizeY / 2f + ClearRadius), ClearRadius/2);
        }
    }
}