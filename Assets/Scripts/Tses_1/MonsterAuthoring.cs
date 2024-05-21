using Unity.Entities;
using UnityEngine;

public class MonsterAuthoring : MonoBehaviour
{
    public float hp;
    public float speed;
    public class MonsterBake : Baker<MonsterAuthoring>
    {
        public override void Bake(MonsterAuthoring authoring)
        {
            //当前对象烘焙为动态类型的实体
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<MonsterTestData>(entity,new MonsterTestData() { 
                hp = authoring.hp,
                speed = authoring.speed
            });
        }
    }

}
