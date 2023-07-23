using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class PlantingScript : MonoBehaviour
    {
        [SerializeField] private InventoryManager invManager;

        private readonly string _selectableDirtTag = "Dirt";

        private readonly string _selectableGrassTag = "Grass";

        private readonly string _selectableWitheredTag = "Withered";

        private readonly string _selectableFenceTag = "Fence";

        private readonly string _selectableTreeTag = "Tree";

        private RaycastHit _hitIt;
        private Renderer _selectedRenderer;
        private Transform _selection;
        [SerializeField] private Material highLightedM;
        [SerializeField] private Material defaultGrassMat;
        [SerializeField] private Material defaultTreeMat;
        private Transform _spawnHere;
        [SerializeField] private GameObject s1Potato;
        [SerializeField] private GameObject s1Carrots;
        [SerializeField] private GameObject dirtPatch;

        [FormerlySerializedAs("characterSFXSource")] [Header("SFX Based Settings")] [SerializeField]
        private AudioSource characterSfxSource;

        [SerializeField] private AudioClip plantingSfx;
        [SerializeField] private AudioClip hammerSfx;

        [Header("Equipment Based Settings")] [SerializeField]
        private GameObject fenceObject;

        [SerializeField] private GameObject axeObject;
        private bool _canChopTree;
        private float _choppingTreeTime;
        private float _choppingTreeTimeOriginal;
        [SerializeField] private GameObject _ChoppingTimer;
        public static bool choppingFinished;
        private float choppingTreeProgressFinished;
        private float choppingTreeProgressFinishedOriginal;

        // Declaration of the InGameTutorial object
        [Header("Script References")] [SerializeField]
        private GameObject gameTut;

        private void Start()
        {
            choppingTreeProgressFinished = 1;
            choppingTreeProgressFinishedOriginal = choppingTreeProgressFinished;
            choppingFinished = false;
            _ChoppingTimer.SetActive(false);
            axeObject.GetComponent<Animator>().enabled = false;
            _choppingTreeTime = 5;
            _choppingTreeTimeOriginal = _choppingTreeTime;
        }

        private void FixedUpdate()
        {

            if (_choppingTreeTime == _choppingTreeTimeOriginal && choppingFinished)
            {
                choppingTreeProgressFinished -= 0.95f * Time.deltaTime;
                Debug.Log(choppingTreeProgressFinished);
                if (choppingTreeProgressFinished < 0.2f)
                {
                    choppingTreeProgressFinished = 0;
                    choppingFinished = false;
                }

            }

            if (_choppingTreeTime == _choppingTreeTimeOriginal && !choppingFinished)
            {
                Debug.Log(choppingTreeProgressFinished);
                choppingTreeProgressFinished = choppingTreeProgressFinishedOriginal;
            }

            if (_selection != null)
            {
                _selectedRenderer = _selection.GetComponent<Renderer>();
                if (_selectedRenderer.CompareTag(_selectableDirtTag) || _selectedRenderer.CompareTag(_selectableDirtTag) || _selectedRenderer.CompareTag(_selectableWitheredTag))
                {
                    _selectedRenderer.material = defaultGrassMat;
                }
                else if (_selectedRenderer.CompareTag(_selectableTreeTag))
                {
                    _selectedRenderer.material = defaultTreeMat;
                }
                _selection = null;
            }

            if (Camera.main != null)
            {
                var rayH = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayH, out _hitIt, 6f))
                {
                    var selectionHit = _hitIt.transform;

                    if (selectionHit.CompareTag(_selectableDirtTag) && selectionHit != null)
                    {
                        _selectedRenderer = selectionHit.GetComponent<Renderer>();

                        if (_selectedRenderer != null && EquipmentScript.heldEquipmentName == "GardenSpade" &&
                            (SeedStorage.potatoSeed > 0 && (InventoryManager._selected == 2) ||
                             (SeedStorage.carrotSeed > 0 && (InventoryManager._selected == 1))))
                        {
                            _selectedRenderer.material = highLightedM;

                            if (SeedStorage.potatoSeed > 0 && (InventoryManager._selected == 2))
                            {
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    Destroy(_hitIt.transform.gameObject);
                                    Instantiate(s1Potato, _hitIt.transform.position, Quaternion.identity);
                                    characterSfxSource.PlayOneShot(plantingSfx);
                                    SeedStorage.potatoSeed -= 1;

                                    // Checks if first time then if it is progresses tutorial stage
                                    if (InGameTutorial.firstPlant == true)
                                    {
                                        InGameTutorial.tutorialAudioSource.Stop();
                                        MainMenu.tutorialStage++;
                                        InGameTutorial.firstPlant = false;

                                        gameTut.GetComponent<InGameTutorial>().RunTutorial();
                                    }
                                }
                            }

                            if (SeedStorage.carrotSeed > 0 && (InventoryManager._selected == 1))
                            {
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    Destroy(_hitIt.transform.gameObject);
                                    Instantiate(s1Carrots, _hitIt.transform.position, Quaternion.identity);
                                    characterSfxSource.PlayOneShot(plantingSfx);
                                    SeedStorage.carrotSeed -= 1;

                                    // Checks if first time then if it is progresses tutorial stage
                                    if (InGameTutorial.firstPlant == true)
                                    {
                                        InGameTutorial.tutorialAudioSource.Stop();
                                        MainMenu.tutorialStage++;
                                        InGameTutorial.firstPlant = false;

                                        gameTut.GetComponent<InGameTutorial>().RunTutorial();
                                    }
                                }
                            }

                            _selection = selectionHit;
                        }
                    }

                    if (CharacterMovementScript.holdingEquipment && EquipmentScript.heldEquipmentName == "Shovel")
                    {
                        if ((selectionHit.CompareTag(_selectableWitheredTag) ||
                             selectionHit.CompareTag(_selectableGrassTag)) && selectionHit != null)
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
                                    characterSfxSource.PlayOneShot(plantingSfx);
                                    Destroy(_hitIt.transform.gameObject);

                                    // Checks if first time then if it is progresses tutorial stage
                                    if (InGameTutorial.lastShovel == true)
                                    {
                                        InGameTutorial.tutorialAudioSource.Stop();
                                        MainMenu.tutorialStage++;
                                        InGameTutorial.lastShovel = false;

                                        gameTut.GetComponent<InGameTutorial>().RunTutorial();
                                    }
                                }

                                _selection = selectionHit;
                            }
                        }
                    }

                    if (CharacterMovementScript.holdingEquipment && EquipmentScript.heldEquipmentName == "Hammer")
                    {
                        if ((selectionHit.CompareTag(_selectableGrassTag) ||
                             selectionHit.CompareTag(_selectableDirtTag)) && selectionHit != null)
                        {
                            _selectedRenderer = selectionHit.GetComponent<Renderer>();

                            if (_selectedRenderer != null)
                            {
                                _selectedRenderer.material = highLightedM;
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    Vector3 xyz = new Vector3(0, 0, 0);
                                    var position = _hitIt.transform.position;
                                    Vector3 fenceSpawnPos = position;
                                    Quaternion newRotation = this.gameObject.transform.rotation;

                                    Instantiate(fenceObject, fenceSpawnPos, newRotation);
                                    characterSfxSource.PlayOneShot(hammerSfx);

                                    Debug.Log(
                                        "the fence is built at " + fenceSpawnPos + " with rotation " + newRotation);
                                    Debug.Log("Player loking at " + this.gameObject.transform.rotation);
                                }

                                _selection = selectionHit;
                            }
                        }

                        if ((selectionHit.CompareTag(_selectableGrassTag) ||
                             selectionHit.CompareTag(_selectableFenceTag)) && selectionHit != null)
                        {
                            _selectedRenderer = selectionHit.GetComponent<Renderer>();

                            if (_selectedRenderer != null)
                            {
                                _selectedRenderer.material = highLightedM;
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    Vector3 xyz = new Vector3(0, 0, 0);
                                    var position = _hitIt.transform.position;
                                    Vector3 fenceSpawnPos = position;
                                    Quaternion newRotation = this.gameObject.transform.rotation;

                                    if (selectionHit.transform.gameObject.tag == "Enemy")
                                    {
                                        Destroy(selectionHit.transform.gameObject);

                                        characterSfxSource.PlayOneShot(hammerSfx);

                                    }

                                    _selection = selectionHit;
                                }


                            }
                        }

                    }

                    if (CharacterMovementScript.holdingEquipment &&
                        EquipmentScript.heldEquipmentName == "Axe")
                    {
                        if (selectionHit.CompareTag(_selectableTreeTag) && selectionHit != null)
                        {
                            _selectedRenderer = selectionHit.GetComponent<Renderer>();

                            if (_selectedRenderer != null)
                            {
                                _selectedRenderer.material = highLightedM;
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    _canChopTree = true;
                                }

                                if (_canChopTree)
                                {
                                    choppingFinished = false;
                                    _ChoppingTimer.SetActive(true);
                                    axeObject.GetComponent<Animator>().enabled = true;
                                    _choppingTreeTime -= 1 * Time.deltaTime;
                                    AppearingSlider();
                                    _ChoppingTimer.GetComponent<Slider>().value = SliderCountDown();
                                }

                                if (_choppingTreeTime < 0.2f)
                                {
                                    choppingFinished = true;
                                    _ChoppingTimer.SetActive(false);
                                    DisappearingSlider();
                                    _canChopTree = false;
                                    axeObject.GetComponent<Animator>().enabled = false;
                                    _choppingTreeTime = _choppingTreeTimeOriginal;
                                }

                                _selection = selectionHit;
                            }
                        }

                    }
                }
            }
        }

        private float SliderCountDown()
        {

            return
                (_choppingTreeTime /
                 _choppingTreeTimeOriginal); // Gives the variables of the slider of the current and max boot timer of the slider.
        }

        private void AppearingSlider()
        {
            _ChoppingTimer.GetComponent<CanvasGroup>().alpha = 1; // Makes the slider appear in the UI.
        }

        private void DisappearingSlider()
        {
            _ChoppingTimer.GetComponent<CanvasGroup>().alpha = 0; // Makes the slider appear in the UI.
        }
    }
}
                        
                        
                    
