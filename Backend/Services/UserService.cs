public class UserService{
    private static List<User> users = new List<User>();
    private static List<PersonalInformation> pis = new List<PersonalInformation>();
    public void CreateUser(User user)
    {
        user.UserId=users.Count+1;
        users.Add(user);
        
    }

    public List<User> ListAllUsers()
    {
        return users;
    }
    public User ListUser(int id)
    {
        return users[id-1];
    }

    public void DeleteUser(int id)
    {
        if (id > users.Count)
        {
            throw new Exception("User not found");
        }
        users.RemoveAt(id-1);
    }

    public void CreatePersonalInfo(int id,PersonalInformation pi)
    {
        var user = users[id-1]; //repo later
        if (user == null) throw new Exception("User not found");
        pi.UserId = id;
        
    }
}