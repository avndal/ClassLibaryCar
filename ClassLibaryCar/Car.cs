public class Car
{

    public int Id { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }


    public override string ToString()
    {
        return $"{Id} {Model} {Year}";
    }

    public void ValidateModel()
    {
        if (Model == null)
        {
            throw new ArgumentNullException("Model was null");
        }
        if (Model.Length < 1)
        {
            throw new ArgumentOutOfRangeException("Title must be at least 1 character");
        }
    }

    public void ValidateYear()
    {
        if (Year == null)
        {
            throw new ArgumentNullException("Year was null");
        }
    }

    public void Validate()
    {
        ValidateModel();
        ValidateYear();
    }

}