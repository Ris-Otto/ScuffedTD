using Units;

namespace Helpers
{
    public interface IUndoableAction<out T>
    {
        public T Execute();
        public void Undo() ;
    }
}
