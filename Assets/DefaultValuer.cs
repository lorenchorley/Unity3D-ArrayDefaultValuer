// Original code from http://answers.unity3d.com/questions/600150/initialization-of-classes-instatiated-in-an-array.html

using System.Collections.Generic;

public class DefaultValuer {
    public const int INITIALISE_DEFAULTS_ONLY_WHEN_STARTING_FROM_EMPTY = 0;
    public const int INITIALISE_DEFAULTS_ON_EVERY_NEW_INSTANCE_ADDED = 1;
}

public class ArrayDefaultValuer<V> : DefaultValuer where V : new() {

    public static void OnValidate(ref ArrayDefaultValuer<V> defaultValuerInstance, V[] array, int onlyFirstInstance) {
        if (defaultValuerInstance == null)
            defaultValuerInstance = new ArrayDefaultValuer<V>();
        defaultValuerInstance.OnValidate(array, onlyFirstInstance);
    }

    bool m_firstDeserialization = true;
    int m_arrayLength = 0;

    private void OnValidate(V[] array, int onlyFirstInstance) {
        if (m_firstDeserialization) {
            m_arrayLength = array.Length;
            m_firstDeserialization = false;
        } else {
            if (array.Length != m_arrayLength) {
                if (onlyFirstInstance == 0) {
                        
                    // Only for first instances when coming from an empty array
                    if (array.Length > 0 && m_arrayLength == 0) { 
                        for (int i = 0; i < array.Length; i++)
                            array[i] = new V();
                    }

                } else {

                    // Any new element will take default values and not inherit from previous element in array
                    if (array.Length > m_arrayLength) { 
                        for (int i = m_arrayLength; i < array.Length; i++)
                            array[i] = new V();
                    }

                }
                    
                m_arrayLength = array.Length;
            }
        }
    }

}

public class ListDefaultValuer<V> : DefaultValuer where V : new() {

    public static void OnValidate(ref ListDefaultValuer<V> defaultValuerInstance, List<V> list, int onlyFirstInstance) {
        if (defaultValuerInstance == null)
            defaultValuerInstance = new ListDefaultValuer<V>();
        defaultValuerInstance.OnValidate(list, onlyFirstInstance);
    }

    bool m_firstDeserialization = true;
    int m_listLength = 0;

    private void OnValidate(List<V> list, int onlyFirstInstance) {
        if (m_firstDeserialization) {
            m_listLength = list.Count;
            m_firstDeserialization = false;
        } else {
            if (list.Count != m_listLength) {
                if (onlyFirstInstance == 0) {

                    // Only for first instances when coming from an empty array
                    if (list.Count > 0 && m_listLength == 0) {
                        for (int i = 0; i < list.Count; i++)
                            list[i] = new V();
                    }

                } else {

                    // Any new element will take default values and not inherit from previous element in array
                    if (list.Count > m_listLength) {
                        for (int i = m_listLength; i < list.Count; i++)
                            list[i] = new V();
                    }

                }

                m_listLength = list.Count;
            }
        }
    }

}
