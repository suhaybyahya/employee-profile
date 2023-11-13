namespace Employee_Profile.ViewModel
{
    public class RaiseSalaryViewModel
    {
        public string RaiseType { get; set; }
        public float Value { get; set; }
    }

    public enum RaiseType
    {
        Fixed,
        Percentage
    }
}
