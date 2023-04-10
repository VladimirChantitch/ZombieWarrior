using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Slot : VisualElement 
{
    new class UxmlFactory : UxmlFactory<Slot , UxmlTraits> { }
    public Image icon;
 

    public void OnInit()
    {
        //DragAndDropManipulator manipulator = new DragAndDropManipulator(this);
    }
    //on init add manipulator
    public void clearSlot()
    {
        icon.visible = false;
    }

}
