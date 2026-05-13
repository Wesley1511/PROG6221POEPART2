namespace POEPART2
{
    internal class User // class for the user, simply countains a name and a getter and setter, may expand this later for more functionality but for now this is all I need
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