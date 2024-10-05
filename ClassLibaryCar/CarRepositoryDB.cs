public class CarRepositoryDB : ICarRepository
{

    private readonly CarDbContext _context;

    public CarRepositoryDB(CarDbContext dbContext)
    {
        _context = dbContext;
    }


    public Car? GetById(int id)
    {
        return _context.Cars.FirstOrDefault(c => c.Id == id);
    }

    public Car Add(Car car)
    {
        car.Validate();
        car.Id = 0;
        _context.Add(car);
        _context.SaveChanges();
        return car;

    }
    public Car? Remove(int id)
    {
        Car? car = GetById(id);

        if (car.Id == null)
        {
            return null;
        }

        _context.Cars.Remove(car);
        _context.SaveChanges();
        return car;
    }

    public Car? Update(Car car, int id)
    {
        car.Validate();
        Car existingCar = GetById(id);

        if (existingCar == null)
        {
            return null;
        }

        existingCar.Model = car.Model;
        existingCar.Year = car.Year;

        _context.SaveChanges();
        return car;

    }

    public IEnumerable<Car> Get(int? yearAfter = null, string? modelIncludes = null, string? orderBy = null)
    {
        //List<Car> result = _context.Cars.ToList();
        IQueryable<Car> result = _context.Cars.ToList().AsQueryable();

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
