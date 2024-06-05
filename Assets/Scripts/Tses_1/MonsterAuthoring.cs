using Unity.Entities;
using UnityEngine;

public class MonsterAuthoring : MonoBehaviour
{
    public float hp;
    public float speed;
    public float createinterval;
    public GameObject bulletPrefab;
    public class MonsterBake : Baker<MonsterAuthoring>
    {
        public override void Bake(MonsterAuthoring authoring)
        {
            //��ǰ����決Ϊ��̬���͵�ʵ��
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<MonsterTestData>(entity,new MonsterTestData() { 
                hp = authoring.hp,
                createinterval = authoring.createinterval,
                bullteProrotype=GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic)
            });

            AddComponent<MoveData>(entity, new MoveData()
            {                
                speed = authoring.speed,
            });
        }
    }

}
