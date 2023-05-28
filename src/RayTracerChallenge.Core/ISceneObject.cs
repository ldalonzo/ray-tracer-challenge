namespace RayTracerChallenge.Core;

public interface ISceneObject
{
    Material Material { get; }

    Vector4 NormalAt(Vector4 worldPoint);
}
