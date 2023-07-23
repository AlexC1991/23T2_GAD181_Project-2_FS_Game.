using UnityEngine;

namespace AlexzanderCowell
{
    public class TreeScript : MonoBehaviour
    {
        [SerializeField] private GameObject stumpTree;
        private bool canChopDown;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canChopDown = true;
            }
        }

        private void Update()
        {
            if (PlantingScript.choppingFinished && canChopDown) // If you can collect is true & you press C.
            {
                Vector3 xyz = new Vector3(-90, 0, 0); // Makes a xyz Vector 3 variable to use below.
                Quaternion newRotation = Quaternion.Euler(xyz); // using the xyz it is used in a Quaternion variable called newRotation.
                SeedStorage.wood += 1; // When collecting it will give you 1 carrot for your total carrot storage.
                Instantiate(stumpTree, transform.position, newRotation); // Spawns in dirt prefab with the current transform.position with a rotation of the newRotation variable from above.
                Destroy(gameObject); //Destroys Tree Prefab in scene.
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canChopDown = false;
            }
        }
        
    }
}
