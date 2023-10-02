using UnityEngine;

[CreateAssetMenu(fileName = "Class Data", menuName = "Player Classes/Class Data")]
public class ClassData : ScriptableObject
{
    [SerializeField]
    private string className;
    public string ClassName { get { return className; } }
    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } }
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }
    [SerializeField]
    private float ability;
    public float Ablilty { get { return ability; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
}