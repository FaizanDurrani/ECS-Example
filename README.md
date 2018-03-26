# RTS Example using Hybrid ECS
This is the start of an RTS made using ECS and Wrapped IComponentDatas, It is NOT using Pure ECS (Instantiation using EntityManager.CreateEntity()) instead its using normal Object.Instantiate() to create `Entities`.

### Why am I not using Pure ECS?
Mainly because, at the moment (Unity-2018.1b12) you cant use the current physics implementation with pure entities (Raycasting, Collisions etc)
