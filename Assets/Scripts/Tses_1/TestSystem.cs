using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;



public readonly partial struct MonsterAspect : IAspect {
    public readonly RefRW<MonsterTestData> monsterData;
    public readonly RefRW<LocalTransform> localTransform;
}


public struct MonsterTestData : IComponentData
{
    public float hp;
    public float speed;
}


//system�����ǽṹ��
[UpdateInGroup(typeof(SimulationSystemGroup))]//���·���
public partial struct MonsterTestSystem:ISystem
{
    /// <summary>
    /// �������������ں���
    /// </summary>
    /// <param name="state"></param>
    public void OnCreate(ref SystemState state) { 
        //Entity monster=state.EntityManager.CreateEntity();�յ�ʵ��
        //����һ������...�����ʵ��
        //Entity monster=state.EntityManager.CreateEntity(typeof(MonsterTestData),typeof(LocalTransform));
        //state.EntityManager.SetComponentData(monster, new MonsterTestData()
        //{
        //    hp=100
        //});

    }

    public void OnUpdate(ref SystemState state) {

        float3 float3 = new float3(0, 0, 1);
        //foreach((RefRW<MonsterTestData> monsterData, RefRW<LocalTransform> localTransform) i in SystemAPI.Query<RefRW<MonsterTestData>, RefRW<LocalTransform>>())
        //{
        //    i.localTransform.ValueRW.Position += float3 * SystemAPI.Time.DeltaTime;
        //    i.monsterData.ValueRW.hp-= SystemAPI.Time.DeltaTime;
        //}

        foreach (MonsterAspect i in SystemAPI.Query<MonsterAspect>())
        {
            i.localTransform.ValueRW.Position += float3 *i.monsterData.ValueRW.speed * SystemAPI.Time.DeltaTime;
            i.monsterData.ValueRW.hp -= SystemAPI.Time.DeltaTime;
        }
    }
}
