using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float speed;
    public class BulletBake : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            //��ǰ����決Ϊ��̬���͵�ʵ��
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<MoveData>(entity, new MoveData()
            {                
                speed = authoring.speed,
            });
        }
    }

}
