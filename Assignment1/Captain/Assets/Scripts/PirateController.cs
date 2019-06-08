using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

public class PirateController : MonoBehaviour
{
    public IPirateCommand ActiveCommand;
    public GameObject ProductPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.ActiveCommand = ScriptableObject.CreateInstance<NoWorkPirateCommand>();
    }

    // Update is called once per frame
    void Update()
    {
        var working = this.ActiveCommand.Execute(this.gameObject, this.ProductPrefab);

        this.gameObject.GetComponent<Animator>().SetBool("Exhausted", !working);
    }

    //Has received motivation. A likely source is from on of the Captain's morale inducements.
    public void Motivate()
    {
        int actionNumber = Random.Range(1, 4);
        if (actionNumber == 1){
            this.ActiveCommand = Object.Instantiate(ScriptableObject.CreateInstance<FastWorkPirateCommand>());
        } else if (actionNumber == 2){
            this.ActiveCommand = Object.Instantiate(ScriptableObject.CreateInstance<NormalWorkPirateCommand>());
        } else if (actionNumber == 3){
            this.ActiveCommand = Object.Instantiate(ScriptableObject.CreateInstance<FastWorkPirateCommand>());
        }
    }
}
