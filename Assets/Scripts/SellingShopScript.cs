using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AlexzanderCowell
{
    public class SellingShopScript : MonoBehaviour
    {
        private bool playerInsideOfSellZone;
        private bool choiceInVisitor;
        private int visitorSelector;
        private int currentVisitor;

        private bool canSellCarrots;
        private bool canSellPotatos;
        private int costTotalCarrots, costTotalPotatos;
        
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip sellSFX;
        [SerializeField] private Text buyingText;
        [SerializeField] private GameObject buyingTextGameObject;
        [SerializeField] private GameObject visitor;

        [Header("Visitor Materials")] 
        [SerializeField] private Material visitor1;
        [SerializeField] private Material visitor2;
        [SerializeField] private Material visitor3;
        [SerializeField] private Material visitor4;
        [SerializeField] private Material visitor5;
        [SerializeField] private Material visitor6;
        private bool canSellWood;
        private int costTotalWood;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInsideOfSellZone = true;
                choiceInVisitor = true;
            }
        }

        private void Start()
        {
            visitor.SetActive(false);
            buyingTextGameObject.SetActive(false);
        }

        private void Update()
        {
            visitorSelector = Random.Range(0, 5);

            if (choiceInVisitor)
            {
                currentVisitor = visitorSelector;
                choiceInVisitor = false;
            }

            if (currentVisitor == 0 && playerInsideOfSellZone)
            {
                Customer1();
            }
            else if (currentVisitor == 1 && playerInsideOfSellZone)
            {
                Customer2();
            }
            else if (currentVisitor == 2 && playerInsideOfSellZone)
            {
                Customer3();
            }
            else if (currentVisitor == 3 && playerInsideOfSellZone)
            {
                Customer4();
            }
            else if (currentVisitor == 4 && playerInsideOfSellZone)
            {
                Customer5();
            }
            else if (currentVisitor == 5 && playerInsideOfSellZone)
            {
                Customer6();
            }
            
        }

        private void Customer1()
        {
            visitor.SetActive(true);
            buyingTextGameObject.SetActive(true);
            visitor.GetComponent<Renderer>().material = visitor1;
            
            if (InventoryManager._selected == 6 && playerInsideOfSellZone)
            {
                buyingText.text = ("Hey Man Nice Torch.");
            }
            
            if (InventoryManager._selected == 2 || InventoryManager._selected == 1 && playerInsideOfSellZone)
            {
                buyingText.text = ("Sorry I Do Not Sell You Stuff Sir");
            }

            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("Sorry you do not have any potatoes left.");
            }

            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots == 0) // If the Inventory is selected is equal to 3 & you are inside of the store & you have 0 of the carrots in your invertory you have nothing to sell.
            {
                buyingText.text = ("Sorry you do not have any carrots left.");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("Sorry you do not have any wood left.");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalWood = 3; // How much Carrots cost.
                buyingText.text = ("Well Hello There! Here is How Much I Pay For Your Wood $" + costTotalWood); // You will see the cost of the Wood and Prompted to press E to sell.
                canSellWood= true; // Allowed to sell the Wood.
            }
            else
            {
                canSellWood = false; // If any is null you can not sell Wood.
            }
            
            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalCarrots = 3; // How much Carrots cost.
                buyingText.text = ("Well Hello There! Here is How Much I Pay For Your Carrots $" + costTotalCarrots); // You will see the cost of the Carrots and Prompted to press E to sell.
                canSellCarrots = true; // Allowed to sell the Carrots.
            }
            else
            {
                canSellCarrots = false; // If any is null you can not sell Carrots.
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos >= 1) // If the Inventory is selected is equal to 4 & you are inside of the store & you have more or equal to 1 of the potato's in your invertory you can sell them.
            {
                costTotalPotatos = 2; // How much Potato's cost.
                buyingText.text = ("Well Hello There! Here is How Much I Pay For Your Potato's $" + costTotalPotatos); // You will see the cost of the Potato's and Prompted to press E to sell.
                canSellPotatos = true; // Allowed to sell the Potato's.
            }
            else
            {
                canSellPotatos = false; // If any is null you can not sell Potato's.
            }
            
            if (canSellWood && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalWood; // Will add the cost of the Carrots to the money total.
                SeedStorage.wood -= 1; // Will remove 1 carrot from the seed storage script.
                canSellWood = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            
            if (canSellCarrots && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalCarrots; // Will add the cost of the Carrots to the money total.
                SeedStorage.carrots -= 1; // Will remove 1 carrot from the seed storage script.
                canSellCarrots = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            if (canSellPotatos && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Potatoes are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalPotatos; // Will add the cost of the Potato's to the money total.
                SeedStorage.potatos -= 1; // Will remove 1 potato from the seed storage script.
                canSellPotatos = false; // Turns off Can Sell Potato's so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
        }

        private void Customer2()
        {
            visitor.SetActive(true);
            buyingTextGameObject.SetActive(true);
            visitor.GetComponent<Renderer>().material = visitor2;
            
            if (InventoryManager._selected == 6 && playerInsideOfSellZone)
            {
                buyingText.text = ("Well like Umm That's a Torch... Soo..");
            }
            
            if (InventoryManager._selected == 2 || InventoryManager._selected == 1 && playerInsideOfSellZone)
            {
                buyingText.text = ("Umm So Like I Don't Sell You Stuff..");
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("Umm you do not have any potatoes?.");
            }

            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots == 0) // If the Inventory is selected is equal to 3 & you are inside of the store & you have 0 of the carrots in your invertory you have nothing to sell.
            {
                buyingText.text = ("Umm you do not have any carrots?.");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("Umm you do not have any wood left.");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalWood = 1; // How much Carrots cost.
                buyingText.text = ("Umm yeah so like I will pay this much for your Wood $" + costTotalWood); // You will see the cost of the Wood and Prompted to press E to sell.
                canSellWood= true; // Allowed to sell the Wood.
            }
            else
            {
                canSellWood = false; // If any is null you can not sell Wood.
            }
            
            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalCarrots = 1;
                buyingText.text = ("Umm yeah so like I will pay this much for your carrots $" + costTotalCarrots); // You will see the cost of the Carrots and Prompted to press E to sell.
                canSellCarrots = true; // Allowed to sell the Carrots.
            }
            else
            {
                canSellCarrots = false; // If any is null you can not sell Carrots.
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos >= 1) // If the Inventory is selected is equal to 4 & you are inside of the store & you have more or equal to 1 of the potato's in your invertory you can sell them.
            {
                costTotalPotatos = 1; // How much Potato's cost.
                buyingText.text = ("Umm yeah so like I will pay this much for your potatoes $" + costTotalPotatos); // You will see the cost of the Potato's and Prompted to press E to sell.
                canSellPotatos = true; // Allowed to sell the Potato's.
            }
            else
            {
                canSellPotatos = false; // If any is null you can not sell Potato's.
            }
            
            if (canSellCarrots && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalCarrots; // Will add the cost of the Carrots to the money total.
                SeedStorage.carrots -= 1; // Will remove 1 carrot from the seed storage script.
                canSellCarrots = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            if (canSellPotatos && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Potatoes are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalPotatos; // Will add the cost of the Potato's to the money total.
                SeedStorage.potatos -= 1; // Will remove 1 potato from the seed storage script.
                canSellPotatos = false; // Turns off Can Sell Potato's so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            
            if (canSellWood && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalWood; // Will add the cost of the Carrots to the money total.
                SeedStorage.wood -= 1; // Will remove 1 carrot from the seed storage script.
                canSellWood = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
        }
        private void Customer3()
        {
            visitor.SetActive(true);
            buyingTextGameObject.SetActive(true);
            visitor.GetComponent<Renderer>().material = visitor3;
            
            if (InventoryManager._selected == 6 && playerInsideOfSellZone)
            {
                buyingText.text = ("What Is That? A Torch? Put That Thing Away!");
            }
            
            if (InventoryManager._selected == 2 || InventoryManager._selected == 1 && playerInsideOfSellZone)
            {
                buyingText.text = ("Hey! Do I Look Like a Shop To You? I Do Not Sell You Stuff!");
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("What? You Have No Potatoes Left!.");
            }

            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots == 0) // If the Inventory is selected is equal to 3 & you are inside of the store & you have 0 of the carrots in your invertory you have nothing to sell.
            {
                buyingText.text = ("What? You Have No Carrots Left!.");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("What? You Have No Wood Left!.");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalWood = 4; // How much Carrots cost.
                buyingText.text = ("Hey! U Want To Make Money? I Will Get that Wood For $" + costTotalWood); // You will see the cost of the Wood and Prompted to press E to sell.
                canSellWood= true; // Allowed to sell the Wood.
            }
            else
            {
                canSellWood = false; // If any is null you can not sell Wood.
            }
            
            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalCarrots = 2;
                buyingText.text = ("Hey! U Want To Make Money? I Will Get Them Carrots For $" + costTotalCarrots); // You will see the cost of the Carrots and Prompted to press E to sell.
                canSellCarrots = true; // Allowed to sell the Carrots.
            }
            else
            {
                canSellCarrots = false; // If any is null you can not sell Carrots.
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos >= 1) // If the Inventory is selected is equal to 4 & you are inside of the store & you have more or equal to 1 of the potato's in your invertory you can sell them.
            {
                costTotalPotatos = 3; // How much Potato's cost.
                buyingText.text = ("Hey! U Want To Make Money? I Will Get Them Potatoes For $" + costTotalPotatos); // You will see the cost of the Potato's and Prompted to press E to sell.
                canSellPotatos = true; // Allowed to sell the Potato's.
            }
            else
            {
                canSellPotatos = false; // If any is null you can not sell Potato's.
            }
            
            if (canSellCarrots && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalCarrots; // Will add the cost of the Carrots to the money total.
                SeedStorage.carrots -= 1; // Will remove 1 carrot from the seed storage script.
                canSellCarrots = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            if (canSellPotatos && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Potatoes are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalPotatos; // Will add the cost of the Potato's to the money total.
                SeedStorage.potatos -= 1; // Will remove 1 potato from the seed storage script.
                canSellPotatos = false; // Turns off Can Sell Potato's so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            if (canSellWood && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalWood; // Will add the cost of the Carrots to the money total.
                SeedStorage.wood -= 1; // Will remove 1 carrot from the seed storage script.
                canSellWood = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            
        }
        private void Customer4()
        {
            visitor.SetActive(true);
            buyingTextGameObject.SetActive(true);
            visitor.GetComponent<Renderer>().material = visitor4;
            
            if (InventoryManager._selected == 6 && playerInsideOfSellZone)
            {
                buyingText.text = ("Wow Umm Cool Torch...");
            }
            
            if (InventoryManager._selected == 2 || InventoryManager._selected == 1 && playerInsideOfSellZone)
            {
                buyingText.text = ("Hmm I Do Not Have Anything You Can Buy..");
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("Potatoes are gone you have none anymore...");
            }

            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots == 0) // If the Inventory is selected is equal to 3 & you are inside of the store & you have 0 of the carrots in your invertory you have nothing to sell.
            {
                buyingText.text = ("Carrots are gone you have none anymore...");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("What? You Have No Wood Left!.");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalWood = 2; // How much Carrots cost.
                buyingText.text = ("Hmm I can really use that wood you got there for $" + costTotalWood); // You will see the cost of the Wood and Prompted to press E to sell.
                canSellWood= true; // Allowed to sell the Wood.
            }
            else
            {
                canSellWood = false; // If any is null you can not sell Wood.
            }
            
            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalCarrots = 4;
                buyingText.text = ("Mmm Those Carrots Look Tasty! I Will Get Them For $" + costTotalCarrots); // You will see the cost of the Carrots and Prompted to press E to sell.
                canSellCarrots = true; // Allowed to sell the Carrots.
            }
            else
            {
                canSellCarrots = false; // If any is null you can not sell Carrots.
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos >= 1) // If the Inventory is selected is equal to 4 & you are inside of the store & you have more or equal to 1 of the potato's in your invertory you can sell them.
            {
                costTotalPotatos = 1; // How much Potato's cost.
                buyingText.text = ("Mmm Those Potatoes Look Good! I Will Get Them For $" + costTotalPotatos); // You will see the cost of the Potato's and Prompted to press E to sell.
                canSellPotatos = true; // Allowed to sell the Potato's.
            }
            else
            {
                canSellPotatos = false; // If any is null you can not sell Potato's.
            }
            
            if (canSellCarrots && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalCarrots; // Will add the cost of the Carrots to the money total.
                SeedStorage.carrots -= 1; // Will remove 1 carrot from the seed storage script.
                canSellCarrots = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            if (canSellPotatos && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Potatoes are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalPotatos; // Will add the cost of the Potato's to the money total.
                SeedStorage.potatos -= 1; // Will remove 1 potato from the seed storage script.
                canSellPotatos = false; // Turns off Can Sell Potato's so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            
            if (canSellWood && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalWood; // Will add the cost of the Carrots to the money total.
                SeedStorage.wood -= 1; // Will remove 1 carrot from the seed storage script.
                canSellWood = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
        }
        private void Customer5()
        {
            visitor.SetActive(true);
            buyingTextGameObject.SetActive(true);
            visitor.GetComponent<Renderer>().material = visitor5;
            
            if (InventoryManager._selected == 6 && playerInsideOfSellZone)
            {
                buyingText.text = ("Haha What are you doing with that Torch!..");
            }
            
            if (InventoryManager._selected == 2 || InventoryManager._selected == 1 && playerInsideOfSellZone)
            {
                buyingText.text = ("Haha As If I Would Sell You Anything!");
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("I Bought All Your Potatoes! Haha");
            }

            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots == 0) // If the Inventory is selected is equal to 3 & you are inside of the store & you have 0 of the carrots in your invertory you have nothing to sell.
            {
                buyingText.text = ("I Bought All Your Carrots! Haha");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalWood = 3; // How much Carrots cost.
                buyingText.text = ("Hey Buddy! Oh Look Wood! I Will Pay $" + costTotalWood); // You will see the cost of the Wood and Prompted to press E to sell.
                canSellWood= true; // Allowed to sell the Wood.
            }
            else
            {
                canSellWood = false; // If any is null you can not sell Wood.
            }
            
            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalCarrots = 1;
                buyingText.text = ("Hey Buddy! Oh Look Carrots! I Will Pay For Them For $" + costTotalCarrots); // You will see the cost of the Carrots and Prompted to press E to sell.
                canSellCarrots = true; // Allowed to sell the Carrots.
            }
            else
            {
                canSellCarrots = false; // If any is null you can not sell Carrots.
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos >= 1) // If the Inventory is selected is equal to 4 & you are inside of the store & you have more or equal to 1 of the potato's in your invertory you can sell them.
            {
                costTotalPotatos = 4; // How much Potato's cost.
                buyingText.text = ("Hey Buddy! Oh Look Potatoes! I Will Pay For Them For $" + costTotalPotatos); // You will see the cost of the Potato's and Prompted to press E to sell.
                canSellPotatos = true; // Allowed to sell the Potato's.
            }
            else
            {
                canSellPotatos = false; // If any is null you can not sell Potato's.
            }
            
            if (canSellCarrots && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalCarrots; // Will add the cost of the Carrots to the money total.
                SeedStorage.carrots -= 1; // Will remove 1 carrot from the seed storage script.
                canSellCarrots = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            if (canSellPotatos && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Potatoes are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalPotatos; // Will add the cost of the Potato's to the money total.
                SeedStorage.potatos -= 1; // Will remove 1 potato from the seed storage script.
                canSellPotatos = false; // Turns off Can Sell Potato's so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            
            if (canSellWood && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalWood; // Will add the cost of the Carrots to the money total.
                SeedStorage.wood -= 1; // Will remove 1 carrot from the seed storage script.
                canSellWood = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
        }
        private void Customer6()
        {
            visitor.SetActive(true);
            buyingTextGameObject.SetActive(true);
            visitor.GetComponent<Renderer>().material = visitor6;

            if (InventoryManager._selected == 6 && playerInsideOfSellZone)
            {
                buyingText.text = ("Hmm that's a Torch..");
            }
                
            if (InventoryManager._selected == 2 || InventoryManager._selected == 1 && playerInsideOfSellZone)
            {
                buyingText.text = ("Ahh ?? U Want To Buy Something? I Have Nothing For You!");
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos == 0) // If the Inventory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                buyingText.text = ("Well Well Look Who Has No Potatoes Left He He");
            }

            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots == 0) // If the Inventory is selected is equal to 3 & you are inside of the store & you have 0 of the carrots in your invertory you have nothing to sell.
            {
                buyingText.text = ("Well Well Look Who Has No Carrots Left He He");
            }
            
            if (InventoryManager._selected == 5 && playerInsideOfSellZone && SeedStorage.wood >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalWood = 5; // How much Carrots cost.
                buyingText.text = ("Wow Man! That wood looks Amazing! Give me Some! I Will Pay $" + costTotalWood); // You will see the cost of the Wood and Prompted to press E to sell.
                canSellWood= true; // Allowed to sell the Wood.
            }
            else
            {
                canSellWood = false; // If any is null you can not sell Wood.
            }
            
            if (InventoryManager._selected == 3 && playerInsideOfSellZone && SeedStorage.carrots >= 1) // If the Inventory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costTotalCarrots = 2;
                buyingText.text = ("Well Now.. Them Carrots Look Great! I Want To Pay For Them For $" + costTotalCarrots); // You will see the cost of the Carrots and Prompted to press E to sell.
                canSellCarrots = true; // Allowed to sell the Carrots.
            }
            else
            {
                canSellCarrots = false; // If any is null you can not sell Carrots.
            }
            
            if (InventoryManager._selected == 4 && playerInsideOfSellZone && SeedStorage.potatos >= 1) // If the Inventory is selected is equal to 4 & you are inside of the store & you have more or equal to 1 of the potato's in your invertory you can sell them.
            {
                costTotalPotatos = 1; // How much Potato's cost.
                buyingText.text = ("Well Now.. Them Potatoes Look Great! I Want To Pay For Them For $" + costTotalPotatos); // You will see the cost of the Potato's and Prompted to press E to sell.
                canSellPotatos = true; // Allowed to sell the Potato's.
            }
            else
            {
                canSellPotatos = false; // If any is null you can not sell Potato's.
            }
            
            
            if (canSellCarrots && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalCarrots; // Will add the cost of the Carrots to the money total.
                SeedStorage.carrots -= 1; // Will remove 1 carrot from the seed storage script.
                canSellCarrots = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            if (canSellPotatos && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Potatoes are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalPotatos; // Will add the cost of the Potato's to the money total.
                SeedStorage.potatos -= 1; // Will remove 1 potato from the seed storage script.
                canSellPotatos = false; // Turns off Can Sell Potato's so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
            
            if (canSellWood && Input.GetKeyDown(KeyCode.E) && playerInsideOfSellZone) // If Can sell Carrots are true & you are still inside of the store and you press E button.
            {
                MoneyScript.moneyTotal += costTotalWood; // Will add the cost of the Carrots to the money total.
                SeedStorage.wood -= 1; // Will remove 1 carrot from the seed storage script.
                canSellWood = false; // Turns off Can Sell Carrots so it does not continuously loop.

                // Plays the sellSFX sound effect
                sfxSource.PlayOneShot(sellSFX);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            playerInsideOfSellZone = false;
            visitor.SetActive(false);
            buyingTextGameObject.SetActive(false);
        }
    }
}
