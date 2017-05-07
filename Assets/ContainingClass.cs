using UnityEngine;

public class ContainingClass : MonoBehaviour {

    [Header("Unity default array")]
    [Tooltip("When starting from an empty array, new items added in the inspector will have their default values set to 0, null, etc.. Any subsequent items will take the values of the last item in the list.")]
    public SerialisableClass[] UnmonitoredArray;

    [Header("Defaults on all items added")]
    [Tooltip("Whenever adding new items to this array, they will be initialised to the default values given in the class.")]
    public SerialisableClass[] MonitoredArrayEveryNew;

    [Header("Defaults only when starting from empty array")]
    [Tooltip("When starting from an array of zero length, any new items will take the default values given in the class. When adding subsequent values to the array, these will default to the last item in the array (Unity default behaviour). Try changing the value of the last item in the array and then adding a new item. It will take the value you gave the last item.")]
    public SerialisableClass[] MonitoredArrayOnlyFromEmpty;

    private ArrayDefaultValuer<SerialisableClass> ADV1;
    private ArrayDefaultValuer<SerialisableClass> ADV2;

    void OnValidate() {
        ArrayDefaultValuer<SerialisableClass>.OnValidate(ref ADV1, MonitoredArrayEveryNew, ArrayDefaultValuer.INITIALISE_DEFAULTS_ON_EVERY_NEW_INSTANCE_ADDED);
        ArrayDefaultValuer<SerialisableClass>.OnValidate(ref ADV2, MonitoredArrayOnlyFromEmpty, ArrayDefaultValuer.INITIALISE_DEFAULTS_ONLY_WHEN_STARTING_FROM_EMPTY);
    }

}
