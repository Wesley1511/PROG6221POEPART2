namespace POEPART2
{
    internal class User
    {
        string name = "User";

        public string GetUserName()
        {
            return name;
        }

        public void SetUserName(string newName)
        {
            name = newName;
        }
    }
}