using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class InvertoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject selectedItem;
        [SerializeField] private Text selectedInInvertory;
        [SerializeField] private Text selectedAmount;
        [SerializeField] private Sprite carrotSeeds, potatoSeeds, carrotIcon, potatoIcon;
        [HideInInspector] public int _selected;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale == 1)
            {
                _selected -= 1;
            }

            if (Input.GetKeyDown(KeyCode.E) && Time.timeScale == 1)
            {
                _selected += 1;
            }

            if (_selected > 4)
            {
                _selected = 1;
            }

            if (_selected < 1)
            {
                _selected = 4;
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
        }
    }
}
