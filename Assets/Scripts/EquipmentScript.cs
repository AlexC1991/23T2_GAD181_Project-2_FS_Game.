using UnityEngine;

namespace AlexzanderCowell
{
    public class EquipmentScript : MonoBehaviour
    {
        public static bool _holdingEquipment; // Says if Holding Equipment or Not.
        private RaycastHit _hit; // The End of the ray cast.
        [SerializeField] private GameObject equipment; // the equipment parent that holds equipment.
        private Transform _currentSelection; // This is the transform variable the end of the ray cast gives info to.
        private int _eButtonInt; // Button E Pressed Counter;
        [HideInInspector] public static GameObject _currentEquipment; // What current equipment is currently selected and changes to what is selected.
        private Renderer _selectedRenderer; // Grabs the renderer of the game object it is looking at.
        [SerializeField] private Material highLightedM; // Highlighted Material used.
        [SerializeField] private Material defaultMat; // Original Material which is StoreStuff in this script.
        private Transform _selection; // Same as the current Selection but is used for the renderer.
        private bool _canCollect; // Allows the character to use the equipment or not.
        private GameObject shedVarient; // Gets the shedVarient and Turns it off & on.


        private void Start()
        {
            _eButtonInt = 0; // Button E Counter starts at 0;
            _currentEquipment = equipment; // currentEquipment is equipment game object as default.
            _holdingEquipment = true; // holding equipment is set to true for it to work currently.
            shedVarient = GameObject.Find("Shed"); // Default the shed variant variable is set to just shed.
        }

        private void FixedUpdate()
        {
            if (Camera.main != null) // If the main camera is not equal to Null
            {
                var rayH = Camera.main.ScreenPointToRay(Input.mousePosition); // Uses the camera to point the ray using the mouse position in the game.
                if (Physics.Raycast(rayH, out _hit, 4f)) // Uses the camera ray being sent out and finding a hit at 4.00 at a max distance.
                {
                    _currentSelection = _hit.transform; // current selection transform will equal to the hit transform
                    Debug.Log(_currentSelection.transform.name); // Finds the name in a debug log.
                }
            }

            if (Input.GetKeyDown(KeyCode.E)) // If E gets pressed down.
            {
                _eButtonInt += 1; // E Button Int will go up by 1 every E Button Pressed down.
            }

            if (_eButtonInt >= 0 && _eButtonInt < 2) // If the E Button is 0 or 1.
            {
                _currentEquipment.SetActive(true); // Current Equipment will be set active.
                _canCollect = true; // Able to collect the game object.
                if (_selection != null) // If the selection is not equal to nothing.
                
                {
                    _selectedRenderer = _selection.GetComponent<Renderer>(); // Selection Renderer will equal to the Renderer Component of the current selected.
                    _selectedRenderer.material = defaultMat; // Selected Renderer material will be default Material 
                    _selection = null; // selection is equal to nothing.
                }
            }
            
            if (_currentSelection != null) // If the current Selection is not equal to nothing.
            {
                _selectedRenderer = _currentSelection.GetComponent<Renderer>(); // If the current Selection is not equal to nothing.

                if (_currentSelection.transform.name == "ShedShovel") // If the currently selected name is called Shovel.
                {
                    _selectedRenderer.material = highLightedM; // Change the material to the highlighted Material.
                    
                    if (Input.GetKeyDown(KeyCode.E) && _canCollect) // If you meet all requirements above and you press E while able to collect is true.
                    {
                        _holdingEquipment = true; // holding equipment is true.
                        _currentEquipment = equipment.transform.GetChild(0).gameObject; // current equipment will change to the first child of the equipment which is the Shovel.
                        _currentEquipment.SetActive(true); // The new current equipment which is the Shovel will be set to true.
                        shedVarient.SetActive(true); // Turns on the shed Variant.
                        shedVarient = GameObject.Find("ShedShovel"); // Sets shed Variant as shed shovel.
                        if (shedVarient != null)
                        {
                            shedVarient.SetActive(false); // Turns off the shed Variant.
                        }
                    }
                }

                _selection = _currentSelection; // Selected will not be null and will equal the currentSelected so will turn it back into the default material.
            }
            
            if (_currentSelection != null) // If the current Selection is not equal to nothing.
            {
                _selectedRenderer = _currentSelection.GetComponent<Renderer>(); // If the current Selection is not equal to nothing.
                
                if (_currentSelection.transform.name == "ShedHammer") // If the currently selected name is called Hammer.
                {
                    _selectedRenderer.material = highLightedM; // Change the material to the highlighted Material.
                    
                    if (Input.GetKeyDown(KeyCode.E) && _canCollect) // If you meet all requirements above and you press E while able to collect is true.
                    { 
                        _holdingEquipment = true;  // holding equipment is true.
                        _currentEquipment = equipment.transform.GetChild(1).gameObject; // current equipment will change to the second child of the equipment which is the Hammer.
                        _currentEquipment.SetActive(true); // The new current equipment which is the Hammer will be set to true.
                        shedVarient.SetActive(true); // Turns on the shed Variant.
                        shedVarient = GameObject.Find("ShedHammer"); // Sets shed Variant as shed Hammer.
                        if (shedVarient != null)
                        {
                            shedVarient.SetActive(false); // Turns off the shed Variant.
                        }
                    }
                }
                
                _selection = _currentSelection; // Selected will not be null and will equal the currentSelected so will turn it back into the default material.
            }

        }
        private void Update()
        {
            if (_eButtonInt >= 3) // If E Button Int Goes higher than 2 and will equal 3 or more it will reset back to 0.
            {
                _eButtonInt = 0; // E button press will equal 0.
            }
            
            if (_eButtonInt >= 2) // If the E Button int is 2 or more.
            {
                _canCollect = false; // You can not collect the equipment.
                _holdingEquipment = false; // Will not be holding any equipment.
            }
            if (!_holdingEquipment) // If the Holding Equipment is false.
            {
                if (shedVarient != null)
                {
                    shedVarient.SetActive(true); // Turns on the shed Variant.
                }
                _selection = _currentSelection; // Selected will not be null and will equal the currentSelected so will turn it back into the default material.
                _currentEquipment.SetActive(false); // The set active will be false so it will not show anymore.
                _currentEquipment = equipment; // current Equipment will reset and equal the equipment game object again.
            }
        }
    }
}

