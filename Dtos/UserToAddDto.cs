namespace DotNetAPI.Dtos
{
    public partial class UserToAddDto //automaper is takeing care of most of our work
    {
        public string FirstName {get; set;} = "";
        public string LastName {get; set;} = "";
        public string Email {get; set;} = "";
        public bool Active {get; set;}
        public int JobId {get; set;}

    }

}