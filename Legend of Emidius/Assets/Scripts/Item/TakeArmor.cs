using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeArmor : MonoBehaviour
{
    [Header("Armor Player")]
    public GameObject helmet;
    public GameObject body;
    public GameObject legs;
    public GameObject gloves;
    public GameObject boots;



    [Header("Cloth Player")]
    public GameObject beard;
    public GameObject beard2;
    public GameObject hair;
    public GameObject hair2;
    public GameObject bodyCloth;
    public GameObject legsCloth;
    public GameObject glovesCloth;
    public GameObject bootsCloth;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Armor")
        {
            helmet.SetActive(true); body.SetActive(true); legs.SetActive(true); gloves.SetActive(true); boots.SetActive(true);
            beard.SetActive(false); beard2.SetActive(false); hair.SetActive(false); hair2.SetActive(false); bodyCloth.SetActive(false); legsCloth.SetActive(false); glovesCloth.SetActive(false); bootsCloth.SetActive(false);
        }
    }
}
