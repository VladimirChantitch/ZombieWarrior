using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiController : MonoBehaviour
{   
    private UQueryBuilder<VisualElement> allSlots;
    private VisualElement slotsContainer;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        slotsContainer = root.Q<VisualElement>("PrincipalLayout");
        var redBall = root.Q<VisualElement>("object1");
       // DragAndDropManipulator manipulator = new DragAndDropManipulator(redBall, root);
        //redBall.AddManipulator(manipulator);
        foreach (VisualElement row in slotsContainer.Children()) { 
            foreach(VisualElement slot in row.Children())
            {
                //DragAndDropManipulator manipulator = new DragAndDropManipulator(slot, root);

                DragAndDropManipulator manipulator = new DragAndDropManipulator(slot.Q<VisualElement>("object"), root);
                //slot.AddManipulator(manipulator);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
