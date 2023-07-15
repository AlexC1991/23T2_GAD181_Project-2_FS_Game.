using UnityEngine;

namespace AlexzanderCowell
{
    public class PlantingScript : MonoBehaviour
    {
        [SerializeField] private InventoryManager invManager;

        private string selectableDirtTag = "Dirt",
            selectableGrassTag = "Grass",
            selectableWitherdTag = "Withered";
            /*selectableStage1Tag = "Stage1",
            selectableStage2Tag = "Stage2",
            selectableStage3Tag = "Stage3";*/

        private RaycastHit _hitIt;
        private Renderer _selectedRenderer;
        private Transform _selection;
        [SerializeField] private Material highLightedM;
        [SerializeField] private Material defaultMat;
        private Transform spawnHere;
        [SerializeField] private GameObject s1Potato;
        [SerializeField] private GameObject s1Carrots;
        [SerializeField] private GameObject dirtPatch;

        private void FixedUpdate()
        {
            if (_selection != null)
            {
                _selectedRenderer = _selection.GetComponent<Renderer>();
                _selectedRenderer.material = defaultMat;
                _selection = null;
            }

            if (Camera.main != null)
            {
                var rayH = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayH, out _hitIt))
                {
                    var selectionHit = _hitIt.transform;
                    
                    if (selectionHit.CompareTag(selectableDirtTag) && selectionHit != null)
                    {
                        _selectedRenderer = selectionHit.GetComponent<Renderer>();

                            if (_selectedRenderer != null && EquipmentScript.holdingEquipment == false)
                            {
                                _selectedRenderer.material = highLightedM;

                                if (SeedStorage.potatoSeed > 0 && (invManager._selected == 2))
                                {
                                    if (Input.GetKeyDown(KeyCode.Mouse0))
                                    {
                                        Destroy(_hitIt.transform.gameObject);
                                        Instantiate(s1Potato, _hitIt.transform.position, Quaternion.identity);
                                        SeedStorage.potatoSeed -= 1;
                                    }
                                }

                                if (SeedStorage.carrotSeed > 0 && (invManager._selected == 1))
                                {
                                    if (Input.GetKeyDown(KeyCode.Mouse0))
                                    {
                                        Destroy(_hitIt.transform.gameObject);
                                        Instantiate(s1Carrots, _hitIt.transform.position, Quaternion.identity);
                                        SeedStorage.carrotSeed -= 1;
                                    }
                                }

                                _selection = selectionHit;
                            }
                    }
                    
                    if (EquipmentScript.holdingEquipment && EquipmentScript.heldEquipmentName == "Shovel") 
                    {
                        if ((selectionHit.CompareTag(selectableWitherdTag) || selectionHit.CompareTag(selectableGrassTag)) && selectionHit != null)
                        {
                            _selectedRenderer = selectionHit.GetComponent<Renderer>();
                            
                            if (_selectedRenderer != null)
                            {
                                _selectedRenderer.material = highLightedM;
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    Vector3 xyz = new Vector3(-90, 0, 0);
                                    Quaternion newRotation = Quaternion.Euler(xyz);
                                    Instantiate(dirtPatch, _hitIt.transform.position, newRotation);
                                    Destroy(_hitIt.transform.gameObject);
                                }
                                _selection = selectionHit;
                            }
                        }
                    }
                }
            }
        }
    }
}
                        
                        
                    
