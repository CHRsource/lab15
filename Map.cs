#pragma warning disable

public class Map<K, V>  // хеш-таблица с закрытой адресацией на основе цепочек
{
    private List<HashNode<K, V>> bucketArray;
    private int numBuckets;
    private int size;

    public Map(int n)
    {
        bucketArray = new List<HashNode<K, V>>();
        numBuckets = n;
        size = 0;

        for (int i = 0; i < numBuckets; i++)
            bucketArray.Add(null);
    }
    public Map() : this(10) { }
    public int Size { get { return size; } }
    public bool IsEmpty { get { return size == 0; } }
    //private int HashCode(K key) { return key.GetHashCode(); }
    private int HashCode(K c) {
        int k = Convert.ToInt32(c); // ASCII-код буквы
        int hashed_value = (11 * k) % numBuckets;  // вычисляем хеш значение
        return hashed_value;
    }
    public int GetBucketIndex(K key)
    {
        int hashCode = HashCode(key);
        int index = hashCode % numBuckets;
        index = index < 0 ? index * -1 : index;
        return index;
    }
    public V Remove(K key)
    {
        int bucketIndex = GetBucketIndex(key);
        int hashCode = HashCode(key);
        HashNode<K, V> head = bucketArray[bucketIndex];

        HashNode<K, V> prev = null;
        while (head != null)
        {
            if (head.key.Equals(key) && hashCode == head.hashCode)
                break;
            prev = head;
            head = head.next;
        }

        if (head == null)
            return default(V);
        size--;

        if (prev != null)
            prev.next = head.next;
        else
            bucketArray[bucketIndex] = head.next;

        return head.value;
    }
    public V Get(K key)
    {
        int bucketIndex = GetBucketIndex(key);
        int hashCode = HashCode(key);
        HashNode<K, V> head = bucketArray[bucketIndex];

        while (head != null)
        {
            if (head.key.Equals(key) && head.hashCode == hashCode)
                return head.value;
            head = head.next;
        }

        return default(V);
    }
    public void Add(K key, V value)
    {
        int bucketIndex = GetBucketIndex(key);
        int hashCode = HashCode(key);
        HashNode<K, V> head = bucketArray[bucketIndex];

        while (head != null)
        {
            if (head.key.Equals(key) && head.hashCode == hashCode)
            {
                head.value = value;
                return;
            }
            head = head.next;
        }

        size++;
        head = bucketArray[bucketIndex];
        HashNode<K, V> newNode = new HashNode<K, V>(key, value, hashCode);
        newNode.next = head;
        bucketArray[bucketIndex] = newNode;

        //if ((1.0 * size) / numBuckets > 0.7)
        //{
        //    List<HashNode<K, V>> temp = bucketArray;
        //    bucketArray = new List<HashNode<K, V>>();
        //    numBuckets = 2 * numBuckets;
        //    size = 0;
        //    for (int i = 0; i < numBuckets; i++)
        //        bucketArray.Add(null);

        //    foreach (HashNode<K, V> headNode in temp)
        //    {
        //        HashNode<K, V> currentNode = headNode;
        //        while (currentNode != null)
        //        {
        //            Add(currentNode.key, currentNode.value);
        //            currentNode = currentNode.next;
        //        }
        //    }
        //}
    }
    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < numBuckets; i++)
        {
            str += $"{i}: ";
            HashNode<K, V> node = bucketArray[i];
            while (node != null)
            {
                str += $"{node.value} ";
                node = node.next;
            }
            str += "\n";
        }
        return str;
    }
}
