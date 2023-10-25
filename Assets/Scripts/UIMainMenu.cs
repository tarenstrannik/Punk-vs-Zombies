using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIMainMenu : UIDisplay
{
    // Start is called before the first frame update
    
    public override void OpenMenu(UnityEngine.Object obj)
    {
        base.OpenMenu(obj);
        
        if (prevMenu == null)
        {
            ButtonActionWithDelay(quitButton);
        }
    }
}
