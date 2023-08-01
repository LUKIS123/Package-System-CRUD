using Package_System_CRUD.BusinessLogic.Repositories;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Services
{
    public class ManufacturerService : IModelService<Manufacturer>
    {
        private readonly IModelRepository<Manufacturer> _repository;

        public ManufacturerService(IModelRepository<Manufacturer> manufacturerRepository)
        {
            _repository = manufacturerRepository;
        }

        public List<Manufacturer> GetPageList(int pageNumber, int numberOfElements)
        {
            return _repository.LoadPage(pageNumber, numberOfElements);
        }

        public Manufacturer? FindById(int id)
        {
            return _repository.FindById(id);
        }

        public Manufacturer? FindByName(string name)
        {
            return _repository.FindByName(name);
        }

        public void AddToDatabase(Manufacturer model)
        {
            _repository.SaveEntity(model);
        }

        public void RemoveFromDatabase(Manufacturer model)
        {
            _repository.DeleteEntity(model);
        }

        public void UpdateEntity(Manufacturer entity)
        {
            _repository.UpdateEntity(entity);
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }
    }
}