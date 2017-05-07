// Original code from http://answers.unity3d.com/questions/600150/initialization-of-classes-instatiated-in-an-array.html

public class ArrayDefaultValuer {
    public const int INITIALISE_DEFAULTS_ONLY_WHEN_STARTING_FROM_EMPTY = 0;
    public const int INITIALISE_DEFAULTS_ON_EVERY_NEW_INSTANCE_ADDED = 1;
}

public class ArrayDefaultValuer<V> : ArrayDefaultValuer where V : new() {

    public static void OnValidate<v>(ref ArrayDefaultValuer<v> defaultValuerInstance, v[] array, int onlyFirstInstance) where v : new() {
        if (defaultValuerInstance == null)
            defaultValuerInstance = new ArrayDefaultValuer<v>();
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
