using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject selectedItem;
        [SerializeField] private Text selectedInInvertory;
        [SerializeField] private Text selectedAmount;
        [SerializeField] private Sprite carrotSeeds, potatoSeeds, carrotIcon, potatoIcon, woodIcon, torchIcon;
        [HideInInspector] public static int _selected;


        private void Update()
        {
            if (Input.mouseScrollDelta.y >= 0 && Time.timeScale == 1)
            {
                _selected += 1;
            }

            if (Input.mouseScrollDelta.y <= 0 && Time.timeScale == 1)
            {
                _selected -= 1;
            }

            if (_selected > 6)
            {
                _selected = 1;
            }

            if (_selected < 1)
            {
                _selected = 6;
            }


            if (_selected == 1)
            {
                selectedItem.GetComponent<Image>().sprite = carrotSeeds;
                selectedInInvertory.text = ("Carrot Seeds");
                selectedAmount.text = (SeedStorage.carrotSeed).ToString();
                selectedInInvertory.GetComponent<Text>().color = Color.red; 
            }
            
            if (_selected == 2)
            {
                selectedItem.GetComponent<Image>().sprite = potatoSeeds;
                selectedInInvertory.text = ("Potato Seeds");
                selectedAmount.text = (SeedStorage.potatoSeed).ToString();
                selectedInInvertory.GetComponent<Text>().color = Color.yellow; 
            }
            
            if (_selected == 3)
            {
                selectedItem.GetComponent<Image>().sprite = carrotIcon;
                selectedInInvertory.text = ("Carrots");
                selectedAmount.text = (SeedStorage.carrots).ToString();
                selectedInInvertory.GetComponent<Text>().color = Color.red; 
            }
            if (_selected == 4)
            {
                selectedItem.GetComponent<Image>().sprite = potatoIcon;
                selectedInInvertory.text = ("Potatoes");
                selectedAmount.text = (SeedStorage.potatos).ToString();
                selectedInInvertory.GetComponent<Text>().color = Color.yellow; 
            }
            if (_selected == 5)
            {
                selectedItem.GetComponent<Image>().sprite = woodIcon;
                selectedInInvertory.text = ("Logs of Wood");
                selectedAmount.text = (SeedStorage.wood).ToString();
                selectedInInvertory.GetComponent<Text>().color = Color.green; 
            }
            if (_selected == 6)
            {
                selectedItem.GetComponent<Image>().sprite = torchIcon;
                selectedInInvertory.text = ("Torch");
                selectedAmount.text = (TorchScript.batteryBank).ToString();
                selectedInInvertory.GetComponent<Text>().color = Color.gray; 
            }
        }
    }
}
