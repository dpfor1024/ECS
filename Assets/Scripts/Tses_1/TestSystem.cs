using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;



public readonly partial struct MonsterAspect : IAspect 
{
    public readonly RefRW<MonsterTestData> monsterData;
    public readonly RefRW<LocalTransform> localTransform;
}


public struct MonsterTestData : IComponentData
{
    public float hp;
    public Entity bullteProrotype;
    public float createTimer;
    public float createinterval;
}


//system必须是结构体
[UpdateInGroup(typeof(SimulationSystemGroup))]//更新分组
public partial struct MonsterTestSystem:ISystem
{
    /// <summary>
    /// 类似于生命周期函数
    /// </summary>
    /// <param name="state"></param>
    public void OnCreate(ref SystemState state) { 
        //Entity monster=state.EntityManager.CreateEntity();空的实体
        //创建一个具有...组件的实体
        //Entity monster=state.EntityManager.CreateEntity(typeof(MonsterTestData),typeof(LocalTransform));
        //state.EntityManager.SetComponentData(monster, new MonsterTestData()
        //{
        //    hp=100
        //});

    }

    public void OnUpdate(ref SystemState state) {
        //foreach((RefRW<MonsterTestData> monsterData, RefRW<LocalTransform> localTransform) i in SystemAPI.Query<RefRW<MonsterTestData>, RefRW<LocalTransform>>())
        //{
        //    i.localTransform.ValueRW.Position += float3 * SystemAPI.Time.DeltaTime;
        //    i.monsterData.ValueRW.hp-= SystemAPI.Time.DeltaTime;
        //}
        foreach (MonsterAspect i in SystemAPI.Query<MonsterAspect>())
        {           
            i.monsterData.ValueRW.hp -= SystemAPI.Time.DeltaTime;
            i.monsterData.ValueRW.createTimer -= SystemAPI.Time.DeltaTime;
            if(i.monsterData.ValueRW.createTimer <= 0)
            {
                i.monsterData.ValueRW.createTimer = i.monsterData.ValueRO.createinterval;
                Entity bullet=state.EntityManager.Instantiate(i.monsterData.ValueRO.bullteProrotype);
                state.EntityManager.SetComponentData(bullet, new LocalTransform()
                {
                    Position=i.localTransform.ValueRO.Position,
                    Scale=0.25f
                });
            }
        }
    }
}



public readonly partial struct MoveAspect : IAspect
{
    public readonly RefRW<MoveData> moveData;
    public readonly RefRW<LocalTransform> localTransform;
}

public struct MoveData : IComponentData
{
    public float speed;
}

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct MoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {

        float3 float3 = new float3(0, 0, 1);

        foreach (MoveAspect i in SystemAPI.Query<MoveAspect>())
        {
            i.localTransform.ValueRW.Position += float3 * i.moveData.ValueRW.speed * SystemAPI.Time.DeltaTime;
        }
    }
}