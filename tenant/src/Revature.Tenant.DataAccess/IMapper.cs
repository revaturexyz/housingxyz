namespace Revature.Tenant.Lib.Interface
{
  /// <summary>
  /// IMapper interface defines methods for mapping db entities <-> logic models
  /// </summary>
  public interface IMapper
  {
    /// <summary>
    /// Map a Model Tenant from a Entity Tenant
    /// </summary>
    /// <param name="tenant">A Tenant Entity</param>
    /// <returns>A Tenant Model</returns>
    public Models.Tenant MapTenant(DataAccess.Entities.Tenant tenant);

    /// <summary>
    /// Map a Entity Tenant from a Model Tenant
    /// </summary>
    /// <param name="tenant">A Tenant Model</param>
    /// <returns>A Tenant Entity</returns>
    public DataAccess.Entities.Tenant MapTenant(Models.Tenant tenant);

    /// <summary>
    /// Map a Model Car from a Entity Car
    /// </summary>
    /// <param name="car">A Car Entity</param>
    /// <returns>A Car Model</returns>
    public Models.Car MapCar(DataAccess.Entities.Car car);

    /// <summary>
    /// Map a Entity Car from a Model Car
    /// </summary>
    /// <param name="car">A Car Model</param>
    /// <returns>A Car Entity</returns>
    public DataAccess.Entities.Car MapCar(Models.Car car);

    /// <summary>
    /// Map a Model Batch from a Entity Batch
    /// </summary>
    /// <param name="batch">A Batch Entity</param>
    /// <returns>A Batch Model</returns>
    public Models.Batch MapBatch(DataAccess.Entities.Batch batch);

    /// <summary>
    /// Map a Entity Batch from a Model Batch
    /// </summary>
    /// <param name="batch">A Batch Model</param>
    /// <returns>A Batch Entity</returns>
    public DataAccess.Entities.Batch MapBatch(Models.Batch batch);

  }
}
