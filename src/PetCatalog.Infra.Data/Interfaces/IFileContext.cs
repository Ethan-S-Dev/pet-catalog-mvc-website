namespace PetCatalog.Infra.Data.Interfaces
{
    public interface IFileContext
    {
        bool Delete(string fileName);
        bool Save(string name, byte[] data);
        bool Update(string oldName, string newName, byte[] data);
    }
}
