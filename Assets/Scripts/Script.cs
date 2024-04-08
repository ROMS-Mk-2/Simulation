using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;


public class Script : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    
    public GameObject customerBurger;
    public GameObject customerPizza;
    public GameObject customerHotDog;

<<<<<<< Updated upstream
    public GameObject[] customerList = new GameObject[3];

=======
    public GameObject[] customerList = new GameObject[6];
    public List<GameObject> currentCustomers = new List<GameObject>();
    
    
>>>>>>> Stashed changes
    private string currentCustomer;
    
    private GameObject currentCustomerObject;

    private List<string> foodList = new List<string>();
    
    private List<int> cart = new List<int>();
    
    private int score = 0;

    private int random = 0;

    [DllImport("__Internal")]
    private static extern void SendData(int data);

    public void ReceiveData(string data)
    {
        //Debug.Log("Received data from React: " + data);
        // Use the received data as needed

        for (int i = cart.Count - 1; i >= 0; i--)
        {
            foreach(GameObject o in currentCustomers)
            {
                Customer customerScript = o.GetComponent<Customer>();
                print("Food Bool: " + foodList[cart[i]].Equals(customerScript.Food) + " Check Bool: " + (customerScript.Check == false));
                if (foodList[cart[i]].Equals(customerScript.Food) && customerScript.Check == false)
                {
                    score++;
                    //cart.Remove(i);
                    customerScript.Check = true;
                }
            }
        }


        text.text = score.ToString();
        StartCoroutine(NewCustomer());
    }

    public void SendDataToReact(string data)
    {
        random = Random.Range(0, 1000);
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            SendData (random);
        #endif
    }

    void Start()
    {
        customerList[0] = customerBurger;
        customerList[1] = customerPizza;
        customerList[2] = customerHotDog;
        //text.text = "This text will be replaced in React!";
        random = Random.Range(0, 2);
<<<<<<< Updated upstream
        currentCustomerObject = Instantiate(customerList[random], this.transform);
        currentCustomerObject.transform.Find("bubble").gameObject.SetActive(false);
        currentCustomer = random.ToString();
=======
        // currentCustomerObject = Instantiate(customerList[random], this.transform);
        // currentCustomerObject.transform.Find("bubble").gameObject.SetActive(false);
        // currentCustomer = random.ToString();
        foodList.Add("Hamburger");
        foodList.Add("Pizza");
        foodList.Add("HotDog");
        foodList.Add("Chicken");
        foodList.Add("Fish");
        foodList.Add("Fry");
        
        StartCoroutine(NewCustomer());
>>>>>>> Stashed changes
    }

    private string GetCurrentCustomerWant()
    {
        return currentCustomer;
    }

    private IEnumerator NewCustomer()
    {
<<<<<<< Updated upstream
        Customer currentCustomerScript = currentCustomerObject.GetComponent<Customer>();
        StartCoroutine(currentCustomerScript.WalkAwayAnim());
        yield return new WaitForSeconds(1f);
        Destroy(currentCustomerObject);
        
        random = Random.Range(0, 3);
        currentCustomerObject = Instantiate(customerList[random], this.transform);
        currentCustomer = random.ToString();
=======
        foreach (Button b in FindObjectsOfType<Button>())
        {
            b.enabled = false;
        }
        foreach(GameObject o in currentCustomers)
        {
            Customer currentCustomerScript = o.GetComponent<Customer>();
            StartCoroutine(currentCustomerScript.WalkAwayAnim());
            
            yield return new WaitForSeconds(1f);
            //Destroy(o);
        }
        currentCustomers.Clear();
        cart.Clear();
        
        
        
        for (int i = 0; i < customerNum; i++)
        {        
            //spawn customernumber amount of customers with random food option
            
            //this dictates the food number
            int newRandom = Random.Range(0, 6);

            currentCustomerObject = Instantiate(customerList[newRandom], new Vector3(transform.position.x + (i*2), transform.position.y, transform.position.z), transform.rotation);
            currentCustomer = newRandom.ToString();
            
            //update customer food var
            Customer customerScript = currentCustomerObject.GetComponent<Customer>();
            customerScript.Food = foodList[newRandom];
            
            //rename customer to fit number
            currentCustomerObject.name = "Customer " + (i+1);
            currentCustomers.Add(currentCustomerObject);
        }
        
        foreach (Button b in FindObjectsOfType<Button>())
        {
            b.enabled = true;
        }
>>>>>>> Stashed changes
    }

    public void PrintList()
    {
        foreach(GameObject o in currentCustomers)
        {
            Customer customerScript = o.GetComponent<Customer>();
            print("Customer: " + o.name + " Food: " + customerScript.Food);
        }
    }

    public void addItemToCart(int item)
    {
        cart.Add(item);
    }
}
