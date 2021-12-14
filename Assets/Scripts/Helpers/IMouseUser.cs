using UnityEngine;

namespace Helpers
{
    public interface IMouseUser
    {
        Vector3 GetMousePos();

        Vector3 GetMousePos(float zValue);
    }
}