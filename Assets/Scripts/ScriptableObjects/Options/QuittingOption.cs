using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOption", menuName = "Options/QuittingOption")]
public class QuittingOption : Option
{
    public override void OnClickOnThisOption()
    {
        Application.Quit();
    }
}
