
public interface ICarRepository
{
    Car Add(Car car);
    IEnumerable<Car> Get(int? yearAfter = null, string? modelIncludes = null, string? orderBy = null);
    Car? GetById(int id);
    Car? Remove(int id);
    Car? Update(Car car, int id);
}