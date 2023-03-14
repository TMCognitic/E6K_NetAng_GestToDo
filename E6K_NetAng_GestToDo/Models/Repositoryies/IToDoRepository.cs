using E6K_NetAng_GestToDo.Models.Entities;

namespace E6K_NetAng_GestToDo.Models.Repositoryies
{
    public interface IToDoRepository
    {
        IEnumerable<ToDo> Get();
        ToDo? Get(int id);
        ToDo? Insert(ToDo entity);
        bool Update(ToDo entity);
        bool Delete(int id);
    }
}
