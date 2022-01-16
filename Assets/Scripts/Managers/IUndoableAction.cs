namespace Managers
{
    
    //Really unnecessary atm but oh well
    public interface IUndoableAction<out T>
    {
        public T Execute();
        public void Undo() ;
    }
}
