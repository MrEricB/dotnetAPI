namespace DotNetAPI.Models
{
    public partial class UserJobInfo
    {
        public int JobId {get; set;}
        public string JobTitle {get; set;} = "";
        public string Department {get; set;} = "";
        public decimal Salary {get; set;}

    }

}