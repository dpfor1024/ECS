using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float speed;
    public class BulletBake : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            //当前对象烘焙为动态类型的实体
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<MoveData>(entity, new MoveData()
            {                
                speed = authoring.speed,
            });
        }
    }

}
