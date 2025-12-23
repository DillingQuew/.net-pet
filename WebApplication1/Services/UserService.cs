using WebApplication1.Models;

namespace WebApplication1.Services;

public class UserService
{
    private List<User> _users;
    private int _lastId = 0;

    public UserService()
    {
        _users = new();
        GetDataFromSource();
    }

    private void GetDataFromSource()
    {
        _users.Add( new User() {Id = GetCurrentNextId(), Login = "Test1", Password = "Password1" });
        _users.Add( new User() {Id = GetCurrentNextId(), Login = "Test2", Password = "Password2" });
    }

    private int GetCurrentNextId() => ++_lastId;

    public List<User> GetAll() => _users;

    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public User Add(User user)
    {
        user.Id = GetCurrentNextId();
        _users.Add(user);

        return user;
    }

    public bool Update(User user)
    {
        var found_user = GetById(user.Id);
        if (found_user == null)
            return false;
        
        found_user.Login = user.Login;
        found_user.Password = user.Password;
        return true;
    }
    
    public bool Delete(int id)
    {
        var found_user = GetById(id);
        if (found_user == null)
            return false;

        _users.Remove(found_user);
        return true;
    }
    
}