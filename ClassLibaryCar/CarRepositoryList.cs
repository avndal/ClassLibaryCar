public class CarRepositoryList
{
    private int _nextId = 1;
    private readonly List<Car> _cars = new();

    public CarRepositoryList()
    {
        _cars.Add(new Car() { Id = _nextId++, Model = "BMW", Year = 2020 });
        _cars.Add(new Car() { Id = _nextId++, Model = "Audi", Year = 2019 });
    }

    public List<Car> GetAll()
    {
        return new List<Car>(_cars);
    }


    public Car GetById(int id)
    {
        return _cars.Find(c => c.Id == id);
    }

    public Car Add(Car car)
    {
        car.Validate();
        car.Id = _nextId++;
        _cars.Add(car);
        return car;

    }
    public Car Remove(int id)
    {
        Car? car = GetById(id);

        if (car.Id == null)
        {
            return null;
        }

        _cars.Remove(car);
        return car;
    }

    public Car Update(Car car, int id)
    {
        car.Validate();
        Car existingCar = GetById(id);

        if (existingCar == null)
        {
            return null;
        }

        existingCar.Model = car.Model;
        existingCar.Year = car.Year;

        return car;

    }

    public IEnumerable<Car> Get(int? yearAfter = null, string? modelIncludes = null, string? orderBy = null)
    {
        IEnumerable<Car> result = new List<Car>(_cars);

        // Filtering

        if (yearAfter != null)
        {
            result = result.Where(car => car.Year > yearAfter);
        }
        if (modelIncludes != null)
        {
            result = result.Where(car => car.Model.Contains(modelIncludes));
        }

        // Ordering aka. sorting

        if (orderBy != null)
        {
            orderBy = orderBy.ToLower();
            switch (orderBy)
            {
                case "title": // fall through to next case
                case "title_asc":
                    result = result.OrderBy(m => m.Model);
                    break;
                case "title_desc":
                    result = result.OrderByDescending(m => m.Model);
                    break;
                case "year":
                case "year_asc":
                    result = result.OrderBy(m => m.Year);
                    break;
                case "year_desc":
                    result = result.OrderByDescending(m => m.Year);
                    break;
                default:
                    break; // do nothing
                           //throw new ArgumentException("Unknown sort order: " + orderBy);
            }
        }
        return result;
    }
}


