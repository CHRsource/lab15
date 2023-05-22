#pragma warning disable
public class HashNode<K, V>
{
    public K key;
    public V value;
    public readonly int hashCode;
    public HashNode<K, V> next;

    public HashNode(K key, V value, int hashCode)
    {
        this.key = key;
        this.value = value;
        this.hashCode = hashCode;
    }
}
