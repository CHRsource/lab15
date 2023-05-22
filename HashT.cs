#pragma warning disable
public class HashT<K, V>    // хеш-таблицп с открытой адресацией на основе линейного зондирования
{
    private int capacity;
    private int size = 0;
    private HashN<K, V>[] arr;
    private HashN<K, V> dummy;

    public HashT(int n)
    {
        capacity = n;
        arr = new HashN<K, V>[capacity];
        dummy = new HashN<K, V>(default(K), default(V));
    }
    public HashT() : this(20) { }
    private int HashCode(K c)
    {
        int k = Convert.ToInt32(c); // ASCII-код буквы
        int hashed_value = (11 * k) % capacity;  // вычисляем хеш значение
        return hashed_value;
    }
    private int GetHashIndex(K key)
    {
        return Math.Abs(HashCode(key)) % capacity;
    }
    public void Insert(K key, V value)
    {
        HashN<K, V> newNode = new(key, value);
        int hashIndex = GetHashIndex(key);
        while (arr[hashIndex] != null && !arr[hashIndex].key.Equals(key) && !arr[hashIndex].key.Equals(default(K)))
        {
            hashIndex++;
            hashIndex %= capacity;
        }
        if (arr[hashIndex] == null || arr[hashIndex].key.Equals(default(K)))
            size++;
        arr[hashIndex] = newNode;
    }
    public bool Delete(K key)
    {
        int hashIndex = GetHashIndex(key);
        while (arr[hashIndex] != null)
        {
            if (arr[hashIndex].key.Equals(key))
            {
                arr[hashIndex] = dummy;
                size--;
                return true;
            }
            hashIndex++;
            hashIndex %= capacity;
        }
        return false;
    }

    public V Find(K key)
    {
        int hashIndex = GetHashIndex(key);
        int counter = 0;
        while (arr[hashIndex] != null)
        {
            if (counter++ > capacity)
                break;

            if (arr[hashIndex].key.Equals(key))
                return arr[hashIndex].value;

            hashIndex++;
            hashIndex %= capacity;
        }
        return default(V);
    }
    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < capacity; i++)
        {
            if (arr[i] != null && !arr[i].Equals(dummy))
                str += $"{i}: {arr[i].key} - {arr[i].value}\n";
            else str += $"{i}: (empty)\n";
        }
        return str;
    }
}
